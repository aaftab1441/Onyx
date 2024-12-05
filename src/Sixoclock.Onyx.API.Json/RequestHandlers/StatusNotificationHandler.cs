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
using Sixoclock.Onyx.API.Json.Utility;
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.RequestHandlers
{
    [OCPPAction(Name = "StatusNotification", Type = RequestHandlerType.CPTOCS)]
    public class StatusNotificationHandler : IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<ErrorCode> _errorCodeRepository;
        private readonly IRepository<ConnectorStatusCode> _evseStatusCodeRepository;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<VendorErrorCode> _vendorErrorCodeRepository;
        private readonly IRepository<EVSEStatus> _evseStatusRepository;
        private readonly IRepository<EVSE> _evseRepository;

        public StatusNotificationHandler(OnyxWebSocketManager webSocketManager,
            IRepository<Chargepoint> chargepointRepository, ILogger logger,
            IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<ErrorCode> errorCodeRepository,
            IRepository<ConnectorStatusCode> evseStatusCodeRepository,
            IRepository<Vendor> vendorRepository,
            IRepository<VendorErrorCode> vendorErrorCodeRepository,
             IRepository<EVSEStatus> evseStatusRepository, IRepository<EVSE> evseRepository)
        {
            _webSocketManager = webSocketManager;
            _chargepointRepository = chargepointRepository;
            _logger = logger;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _errorCodeRepository = errorCodeRepository;
            _evseStatusCodeRepository = evseStatusCodeRepository;
            _vendorRepository = vendorRepository;
            _vendorErrorCodeRepository = vendorErrorCodeRepository;
            _evseStatusRepository = evseStatusRepository;
            _evseRepository = evseRepository;
        }
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
                    var messageOb = JsonConvert.DeserializeObject<StatusNotificationRequest>(message.PayLoad);
                    messageOb.VendorErrorCode = string.IsNullOrEmpty(messageOb.VendorErrorCode) ? "0" : messageOb.VendorErrorCode;
                    cp = await _chargepointRepository.FirstOrDefaultAsync(x => x.Identity == socket.Identity);
                    int ocppPendingStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Pending))
                        .Id;

                    await _ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent()
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
                    EVSE evse;
                    using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                    {
                         evse = await _evseRepository.FirstOrDefaultAsync(x =>
                            x.ChargepointId == cp.Id && x.EVSE_id == messageOb.ConnectorId);
                    }
                    int? vendorErrorCodeId = null;
                    if (!string.IsNullOrEmpty(messageOb.VendorId))
                    {
                        var vendor = await _vendorRepository.FirstOrDefaultAsync(x => x.Name == messageOb.VendorId);
                         vendorErrorCodeId = (await _vendorErrorCodeRepository.FirstOrDefaultAsync(x =>
                            x.VendorId == vendor.Id && x.ErrorCode == Int32.Parse(messageOb.VendorErrorCode))).Id;
                    }
                   
                    var errorCode = await _errorCodeRepository.FirstOrDefaultAsync(x => x.Value == EnumExtensions.ToEnumString(messageOb.ErrorCode));
                    var evseStatusCode = await _evseStatusCodeRepository.FirstOrDefaultAsync(x =>
                        x.Status == EnumExtensions.ToEnumString(messageOb.Status));
                    var evseStatus = await _evseStatusRepository.FirstOrDefaultAsync(x => x.EVSE.Id == evse.Id);
                    if (evseStatus != null)
                    {
                        evseStatus.ErrorCodeId = errorCode.Id;
                        evseStatus.EVSEStatusCodeId = evseStatusCode.Id;
                        evseStatus.VendorErrorCodeId = vendorErrorCodeId;
                        evseStatus.Time = DateTime.UtcNow;
                        await _evseStatusRepository.UpdateAsync(evseStatus);
                    }
                    else
                    {
                        evseStatus = new EVSEStatus
                        {
                            
                            ErrorCodeId = errorCode.Id,
                            EVSEStatusCodeId = evseStatusCode.Id,
                            VendorErrorCodeId = vendorErrorCodeId,
                            TenantId = socket.TenantId,
                            Time = DateTime.UtcNow,
                            CreationTime = DateTime.UtcNow
                        };
                        int evseStatusId=await _evseStatusRepository.InsertAndGetIdAsync(evseStatus);
                        await _unitOfWorkManager.Current.SaveChangesAsync();
                        evse.EVSEStatusId = evseStatusId;
                        await _evseRepository.UpdateAsync(evse);

                    }
                   
                    

                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(new StatusNotificationResponse());
                    var responseJson = JsonConvert.SerializeObject(response);
                    int ocppSuccessStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x =>
                            x.Status == OCPPStatusConstants.Success))
                        .Id;
                    messageEvent.Response = responseJson;
                    messageEvent.OCPPStatusId = ocppSuccessStatusId;
                    await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                    _logger.Debug("response: " + responseJson);
                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseJson)), WebSocketMessageType.Binary, true, CancellationToken.None);
                   
                }
                catch (Exception ex)
                {
                    int ocppStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Failed)).Id;
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
