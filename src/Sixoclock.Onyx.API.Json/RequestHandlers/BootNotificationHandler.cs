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
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.RequestHandlers
{
    [OCPPAction(Name = "BootNotification", Type = RequestHandlerType.CPTOCS)]
    public class BootNotificationHandler : IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<ChargepointKeyValue> _connectorKeyValueRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IRepository<ChargepointModel> _cPModelRepository;
        private readonly IRepository<Vendor> _cPVendorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BootNotificationHandler(OnyxWebSocketManager webSocketManager,
            IRepository<Chargepoint> chargepointRepository,
            ILogger logger,
            IRepository<ChargepointKeyValue> connectorKeyValueRepository,
            IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository,
            IRepository<ChargepointModel> cPModelRepository1,
            IRepository<Vendor> cPVendorRepository1, IUnitOfWorkManager unitOfWorkManager)
        {
            _webSocketManager = webSocketManager;
            _chargepointRepository = chargepointRepository;
            _logger = logger;
            _connectorKeyValueRepository = connectorKeyValueRepository;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _cPModelRepository = cPModelRepository1;
            _cPVendorRepository = cPVendorRepository1;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public async Task HandleRequestAsync(RPCMessage message)
        {
            using (var unitofWork = _unitOfWorkManager.Begin())
            {
                var socket = _webSocketManager.GetSocketById(message.ConnectionId);
                var response = new List<object>();
                Chargepoint cp = null;
                try
                {
                    _unitOfWorkManager.Current.SetTenantId(socket.TenantId);
                    var messageOb = JsonConvert.DeserializeObject<BootNotificationRequest>(message.PayLoad);

                    cp = _chargepointRepository.FirstOrDefault(x => x.Identity == socket.Identity);
                    int ocppPendingStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Pending))
                        .Id;
                    await _ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent
                    {
                        TenantId = socket.TenantId,
                        OCPPStatusId = ocppPendingStatusId,
                        ChargepointId = cp.Id,
                        Request = message.PayLoad,
                        UniqueId = message.UniqueId
                    });
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    var messageEvent =
                        await _ocppMessageEventRepository.FirstOrDefaultAsync(x => x.UniqueId == message.UniqueId);
                        var cpModel = _cPModelRepository.FirstOrDefault(x => x.Id == cp.ChargepointModelId);
                        var cpVendor = _cPVendorRepository.FirstOrDefault(x => x.Id == cpModel.VendorId);
                        var keyvalue = await _connectorKeyValueRepository.FirstOrDefaultAsync(x =>
                            x.Chargepoint.Id == cp.Id && x.Key == KeyValueConstants.HeartbeatInterval);
                        int heartbeatInterval = keyvalue != null ? Int32.Parse(keyvalue.ChargepointValue) : 30;
                        if (cpVendor.Name == messageOb.ChargePointVendor &&
                            cpModel.ModelName == messageOb.ChargePointModel)
                        {
                            var bootNotificationREsponse = new BootNotificationResponse()
                            {
                                Status = BootNotificationResponseStatus.Accepted,
                                CurrentTime = "2017-01-22T10:39:59.630094",
                                Interval = heartbeatInterval
                            };
                            response.Add(3);
                            response.Add(message.UniqueId);
                            response.Add(bootNotificationREsponse);
                            int ocppSuccessStatusId =
                                (await _ocppStatusRepository.FirstOrDefaultAsync(x =>
                                    x.Status == OCPPStatusConstants.Success))
                                .Id;
                            messageEvent.Response = JsonConvert.SerializeObject(bootNotificationREsponse);
                            messageEvent.OCPPStatusId = ocppSuccessStatusId;
                            await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                           // cp.ResetStatusId = idleStatus.Id;
                            await _chargepointRepository.UpdateAsync(cp);
                        }
                        else
                        {
                            var bootnotificationResponse = new BootNotificationResponse()
                            {
                                Status = BootNotificationResponseStatus.Pending,
                                CurrentTime = "2018-01-22T10:39:59.630094",
                                Interval = heartbeatInterval
                            };
                            response.Add(3);
                            response.Add(message.UniqueId);
                            response.Add(bootnotificationResponse);
                            int ocppSuccessStatusId =
                                (await _ocppStatusRepository.FirstOrDefaultAsync(x =>
                                    x.Status == OCPPStatusConstants.Success))
                                .Id;
                            messageEvent.Response = JsonConvert.SerializeObject(response);
                            messageEvent.OCPPStatusId = ocppSuccessStatusId;
                            await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                        }
             

                    var responseJson = JsonConvert.SerializeObject(response);
                    _logger.DebugFormat("Response:\n {0}", responseJson);
                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseJson)),
                        WebSocketMessageType.Binary, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    var ocppStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Failed))
                        .Id;
                    var errorResponse = new CallErrorResponse()
                    {
                        ErrorCode = OCPPErrorCode.InternalError,
                        ErrorDescription = ex.Message,
                        ErrorDetails = ex.StackTrace
                    };
                    response.Clear();
                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(errorResponse);
                    if (cp != null)
                    {
                        var messageEvent =
                            await _ocppMessageEventRepository.FirstOrDefaultAsync(x => x.UniqueId == message.UniqueId);
                        if (messageEvent != null)
                        {
                            messageEvent.OCPPStatusId = ocppStatusId;
                            messageEvent.Response = JsonConvert.SerializeObject(errorResponse);
                            await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                        }

                        else
                            await _ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent()
                            {
                                TenantId = socket.TenantId,
                                OCPPStatusId = ocppStatusId,
                                ChargepointId = cp.Id,
                                Response = JsonConvert.SerializeObject(errorResponse)
                            });
                    }
                    var responseJson = JsonConvert.SerializeObject(response);
                    _logger.DebugFormat("Response:\n {0}", responseJson);
                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseJson)),
                        WebSocketMessageType.Binary, true, CancellationToken.None);
                }
                await unitofWork.CompleteAsync();
            }
        }
    }
}