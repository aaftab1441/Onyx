using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.Json.RequestHandlers;
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema.Internal;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.RequestSenders
{
    [OCPPAction(Name = "ChangeAvailability", Type = RequestHandlerType.CSTOCP)]
    public class ChangeAvailabilitySender:IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<AvailabilityEvent> _availabilityEventRepository;

        public ChangeAvailabilitySender(OnyxWebSocketManager webSocketManager, ILogger logger, IRepository<OCPPMessageEvent> ocppMessageEventRepository, IRepository<Chargepoint> chargepointRepository, IRepository<OCPPStatus> ocppStatusRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<AvailabilityEvent> availabilityEventRepository)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _chargepointRepository = chargepointRepository;
            _ocppStatusRepository = ocppStatusRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _availabilityEventRepository = availabilityEventRepository;
        }
        public async Task HandleRequestAsync(RPCMessage message)
        {
            using (var unitofWork = _unitOfWorkManager.Begin())
            {
                var socket = _webSocketManager.GetSocketById(message.ConnectionId);
                Chargepoint cp = null;
                try
                {
                    var messageOb = JsonConvert.DeserializeObject(message.PayLoad);
                    _unitOfWorkManager.Current.SetTenantId(socket.TenantId);
                    var ocppStatus =
                        await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Success);

                    cp = _chargepointRepository.FirstOrDefault(x => x.Identity == socket.Identity);
                    var data = new List<object> { (int)message.MessageId, message.UniqueId, message.Action, messageOb };
                    _logger.DebugFormat("Request:\n {0}", JsonConvert.SerializeObject(data));

                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))),
                        WebSocketMessageType.Binary, true, CancellationToken.None);
                    int messageEventId = await this._ocppMessageEventRepository.InsertAndGetIdAsync(new OCPPMessageEvent()
                    {
                        TenantId = socket.TenantId,
                        OCPPStatusId = ocppStatus.Id,
                        ChargepointId = cp.Id,
                        Request = message.PayLoad,
                        UniqueId = message.UniqueId
                    });
                    if (message.Metadata != null && message.Metadata.ContainsKey(APIMetadata.EventId))
                    {
                        int availabilityEventId = Int32.Parse(message.Metadata[APIMetadata.EventId]);
                        var availabilityEvent = await _availabilityEventRepository.FirstOrDefaultAsync(availabilityEventId);
                        availabilityEvent.OcppMessageEventId = messageEventId;
                        await _availabilityEventRepository.UpdateAsync(availabilityEvent);
                    }
                }
                catch (Exception ex)
                {
                    var response = new List<object>();
                    var ocppStatus =
                        await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Failed);
                    var errorResponse = new CallErrorResponse()
                    {
                        ErrorCode = OCPPErrorCode.InternalError,
                        ErrorDescription = ex.Message,
                        ErrorDetails = ex.StackTrace
                    };
                    response.Clear();
                    response.Add((int)message.MessageId);
                    response.Add(message.UniqueId);
                    response.Add(message.Action);
                    response.Add(errorResponse);
                    if (cp != null)
                        await this._ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent()
                        {
                            TenantId = socket.TenantId,
                            OCPPStatus = ocppStatus,
                            ChargepointId = cp.Id,
                            Request = JsonConvert.SerializeObject(errorResponse),
                            UniqueId = message.UniqueId
                        });

                }
                await unitofWork.CompleteAsync();
            }
        }
    }
}
