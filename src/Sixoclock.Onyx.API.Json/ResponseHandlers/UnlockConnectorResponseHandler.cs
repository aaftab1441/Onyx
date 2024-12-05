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
    [OCPPAction(Name = "UnlockConnector", Type = RequestHandlerType.CPTOCS)]
    public class UnlockConnectorResponseHandler:IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IRepository<UnlockStatus> _unlockStatusRepository;
        private readonly IRepository<UnlockEvent> _unlockEventRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnlockConnectorResponseHandler(IRepository<Chargepoint> chargepointRepository, ILogger logger, OnyxWebSocketManager webSocketManager, IRepository<OCPPStatus> ocppStatusRepository, IRepository<OCPPMessageEvent> ocppMessageEventRepository, IRepository<UnlockStatus> unlockStatusRepository, IRepository<UnlockEvent> unlockEventRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _chargepointRepository = chargepointRepository;
            _logger = logger;
            _webSocketManager = webSocketManager;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _unlockStatusRepository = unlockStatusRepository;
            _unlockEventRepository = unlockEventRepository;
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
                        var messageOb = JsonConvert.DeserializeObject<UnlockConnectorResponse>(message.PayLoad);
                        var unlockStatusId = (await _unlockStatusRepository.FirstOrDefaultAsync(x =>
                                x.Value ==
                                EnumExtensions.ToEnumString<UnlockConnectorStatusEnum>(messageOb.Status)))
                            .Id;
                        if (message.Metadata != null && message.Metadata.ContainsKey(APIMetadata.EventId))
                        {
                            int unlockEventId = Int32.Parse(message.Metadata[APIMetadata.EventId]);
                            var unlockEvent = await _unlockEventRepository.FirstOrDefaultAsync(unlockEventId);
                            unlockEvent.UnlockStatusId = unlockStatusId;
                            await _unlockEventRepository.UpdateAsync(unlockEvent);
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
