using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.Json.RequestHandlers;
using Sixoclock.Onyx.API.Json.Utility;
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema;
using Sixoclock.Onyx.API.JsonSchema.Internal;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.ResponseHandlers
{
    [OCPPAction(Name = "ChangeAvailability", Type = RequestHandlerType.CPTOCS)]
    public class ChangeAvailabilityResponseHandler:IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IRepository<AvailabilityStatus> _availabilityStatusRepository;
        private readonly IRepository<AvailabilityEvent> _availabilityEventRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ChangeAvailabilityResponseHandler(OnyxWebSocketManager webSocketManager, ILogger logger, IRepository<Chargepoint> chargepointRepository, IRepository<OCPPStatus> ocppStatusRepository, IRepository<OCPPMessageEvent> ocppMessageEventRepository, IRepository<AvailabilityStatus> availabilityStatusRepository, IRepository<AvailabilityEvent> availabilityEventRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
            _chargepointRepository = chargepointRepository;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _availabilityStatusRepository = availabilityStatusRepository;
            _availabilityEventRepository = availabilityEventRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task HandleRequestAsync(RPCMessage message)
        {
            _logger.DebugFormat(
                 message.MessageId == MessageType.CALL
                     ? "Handing (CP=>CS Message: {0})"
                     : "Handling (CP=>CS Message: {0})", message.Action);
            using (var unitofWork = _unitOfWorkManager.Begin())
            {
                var socket = _webSocketManager.GetSocketById(message.ConnectionId);
                Chargepoint cp = null;
                try
                {

                    _unitOfWorkManager.Current.SetTenantId(socket.TenantId);

                    cp = await _chargepointRepository.FirstOrDefaultAsync(x => x.Identity == socket.Identity);
                    int ocppSuccessStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Success))
                        .Id;

                    var messageEvent =
                        await _ocppMessageEventRepository.FirstOrDefaultAsync(x => x.UniqueId == message.UniqueId);
                    if (message.MessageId == MessageType.CALLRESULT)
                    {
                        var messageOb = JsonConvert.DeserializeObject<ChangeAvailabilityResponse>(message.PayLoad);
                        var availabilityStatusId = (await _availabilityStatusRepository.FirstOrDefaultAsync(x =>
                                x.Value ==
                                EnumExtensions.ToEnumString<ChangeAvailabilityStatusEnum>(messageOb.Status)))
                            .Id;
                        if (message.Metadata != null && message.Metadata.ContainsKey(APIMetadata.EventId))
                        {
                            int availabilityEventId = Int32.Parse(message.Metadata[APIMetadata.EventId]);
                            var availabilityEvent = await _availabilityEventRepository.FirstOrDefaultAsync(availabilityEventId);
                            availabilityEvent.AvailabilityStatusId = availabilityStatusId;
                            await _availabilityEventRepository.UpdateAsync(availabilityEvent);
                        }

                    }
                    messageEvent.OCPPStatusId = ocppSuccessStatusId;
                    messageEvent.Response = message.PayLoad;
                    await _ocppMessageEventRepository.UpdateAsync(messageEvent);



                }
                catch (Exception ex)
                {
                    int ocppStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Failed))
                        .Id;
                    if (cp != null)
                    {
                        var messageEvent =
                            await _ocppMessageEventRepository.FirstOrDefaultAsync(x => x.UniqueId == message.UniqueId);
                        if (messageEvent != null)
                        {
                            messageEvent.OCPPStatusId = ocppStatusId;
                            messageEvent.Response = ex.Message;
                            await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                        }

                        else
                            await this._ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent()
                            {
                                TenantId = socket.TenantId,
                                OCPPStatusId = ocppStatusId,
                                ChargepointId = cp.Id,
                                Response = ex.Message
                            });
                    }

                }
                await unitofWork.CompleteAsync();
            }
        }
    }
}
