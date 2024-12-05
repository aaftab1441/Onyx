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
    [OCPPAction(Name = "StopTransaction", Type = RequestHandlerType.CPTOCS)]
    public class StopTransactionRequestHandler : IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Phase> _phaseRepository;
        private readonly IRepository<Context> _contextRepository;
        private readonly IRepository<Format> _formatRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Measurand> _measurandRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IRepository<MeterValue> _meterValueRepository;
        private readonly IRepository<Tag> _idTagRepository;
        private readonly IRepository<ParentTag> _parentTagRepository;
        private readonly IRepository<AuthorizationStatus> _authStatusRepository;
        private readonly IRepository<Reason> _reasonRepository;
        private readonly IRepository<MeterValueType> _meterValueTypeRepository;
        private readonly IRepository<TransactionStatus> _transactionStatusRepository;

        public StopTransactionRequestHandler(OnyxWebSocketManager webSocketManager, ILogger logger,
            IRepository<Chargepoint> chargepointRepository, IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository, IUnitOfWorkManager unitOfWorkManager,
            IRepository<Transaction> transactionRepository, IRepository<Phase> phaseRepository,
            IRepository<Context> contextRepository, IRepository<Format> formatRepository,
            IRepository<Location> locationRepository, IRepository<Measurand> measurandRepository,
            IRepository<Unit> unitRepository, IRepository<MeterValue> meterValueRepository,
            IRepository<Tag> idTagRepository, IRepository<ParentTag> parentTagRepository,
            IRepository<AuthorizationStatus> authStatusRepository, IRepository<Reason> reasonRepository, IRepository<MeterValueType> meterValueTypeRepository, IRepository<TransactionStatus> transactionStatusRepository)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
            _chargepointRepository = chargepointRepository;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _transactionRepository = transactionRepository;
            _phaseRepository = phaseRepository;
            _contextRepository = contextRepository;
            _formatRepository = formatRepository;
            _locationRepository = locationRepository;
            _measurandRepository = measurandRepository;
            _unitRepository = unitRepository;
            _meterValueRepository = meterValueRepository;
            _idTagRepository = idTagRepository;
            _parentTagRepository = parentTagRepository;
            _authStatusRepository = authStatusRepository;
            _reasonRepository = reasonRepository;
            _meterValueTypeRepository = meterValueTypeRepository;
            _transactionStatusRepository = transactionStatusRepository;
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
                    var messageOb = JsonConvert.DeserializeObject<StopTransactionRequest>(message.PayLoad);
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
                   
                    var transaction =
                        await _transactionRepository.FirstOrDefaultAsync(x => x.Id == messageOb.TransactionId);
                    var idTag = await _idTagRepository.FirstOrDefaultAsync(x => x.IdToken == messageOb.IdTag);
                    var authStatus =
                        await _authStatusRepository.FirstOrDefaultAsync(x => x.Id == idTag.AuthorizationStatusId);
                    var parentTag = await _parentTagRepository.FirstOrDefaultAsync(x => x.Id == idTag.ParentTagId);
                    var reason = await _reasonRepository.FirstOrDefaultAsync(x => x.ReasonName == "StoppedByEV");
                    var stopmeterValueType= await _meterValueTypeRepository.FirstOrDefaultAsync(x => x.Type == "Stop");
                    var intermediatemeterValueType = await _meterValueTypeRepository.FirstOrDefaultAsync(x => x.Type == "Intermediate");
                    if (intermediatemeterValueType == null)
                        throw new Exception("IntermediateMeterValueType is not defined");
                    StopTransactionResponse transactionResponse ;
                    if (transaction != null)
                    {
                        if (messageOb.TransactionData != null)
                        {
                            foreach (var meterValueItem in messageOb.TransactionData)
                            {
                                var timestamp = meterValueItem.Timestamp;
                                foreach (var sampledValueItem in meterValueItem.SampledValue)
                                {
                                    var measurand = sampledValueItem.Measurand.HasValue
                                        ? _measurandRepository.FirstOrDefaultAsync(x =>
                                            x.MeasurandType == EnumExtensions.ToEnumString(sampledValueItem.Measurand.Value))
                                        : null;
                                    var context = sampledValueItem.Context.HasValue
                                        ? _contextRepository.FirstOrDefaultAsync(x =>
                                            x.ContextName == EnumExtensions.ToEnumString(sampledValueItem.Context.Value))
                                        : null;
                                    var format = sampledValueItem.Format.HasValue
                                        ? _formatRepository.FirstOrDefaultAsync(x =>
                                            x.FormatType == EnumExtensions.ToEnumString(sampledValueItem.Format.Value))
                                        : null;
                                    var location = sampledValueItem.Location.HasValue
                                        ? _locationRepository.FirstOrDefaultAsync(x =>
                                            x.LocationName == EnumExtensions.ToEnumString(sampledValueItem.Location.Value))
                                        : null;
                                    var phase = sampledValueItem.Phase.HasValue
                                        ? _phaseRepository.FirstOrDefaultAsync(x =>
                                            x.PhaseName == EnumExtensions.ToEnumString(sampledValueItem.Phase.Value))
                                        : null;
                                    var unit = sampledValueItem.Unit.HasValue
                                        ? _unitRepository.FirstOrDefaultAsync(x =>
                                            x.UnitName == EnumExtensions.ToEnumString(sampledValueItem.Unit.Value))
                                        : null;
                                    var meterValue = new MeterValue
                                    {
                                        ContextId = context?.Id,
                                        FormatId = format?.Id,
                                        LocationId = location?.Id,
                                        MeterTime = timestamp,
                                        PhaseId = phase?.Id,
                                        MeasurandId = measurand?.Id,
                                        TransactionId = transaction.Id,
                                        Value = float.TryParse(sampledValueItem.Value, out var val) ? val : 0,
                                        CreationTime = DateTime.UtcNow,
                                        UnitId = unit?.Id,
                                        MeterValueTypeId = intermediatemeterValueType.Id
                                    };
                                    await _meterValueRepository.InsertAsync(meterValue);
                                }
                            }
                        }
                        var stopmeterValue = new MeterValue
                        {
                            
                            MeterTime = messageOb.Timestamp,
                            TransactionId = transaction.Id,
                            Value = messageOb.MeterStop,
                            CreationTime = DateTime.UtcNow,
                            MeterValueTypeId =stopmeterValueType.Id
                           
                        };
                        await _meterValueRepository.InsertAsync(stopmeterValue);
                        var transactionStatus = await
                            _transactionStatusRepository.FirstOrDefaultAsync(x =>
                                x.Value == EnumExtensions.ToEnumString(TransactionStatusEnum.Completed));
                        transaction.TransactionStopTime = DateTime.UtcNow;
                        transaction.ReasonId = reason.Id;
                        transaction.TransactionStatusId = transactionStatus.Id;
                        await _transactionRepository.UpdateAsync(transaction);
                        transactionResponse = new StopTransactionResponse()
                        {
                            IdTagInfo = new IdTagInfo()
                            {
                                ParentIdTag = idTag.ParentTag != null ? parentTag.Value : null,
                                Status = EnumExtensions.ToEnum<StatusEnum>(authStatus.Value),
                                ExpiryDate = "2017-10-02T19:53:32.486Z"
                            }
                        };
                    }
                    else
                    {
                        transactionResponse = new StopTransactionResponse()
                        {
                            IdTagInfo = new IdTagInfo()
                            {
                                Status = StatusEnum.Invalid,
                                ExpiryDate = "2017-10-02T19:53:32.486Z"
                            }
                        };
                    }
                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(transactionResponse);
                    var responseJson = JsonConvert.SerializeObject(response);
                    int ocppSuccessStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x =>
                            x.Status == OCPPStatusConstants.Success))
                        .Id;
                    messageEvent.Response = responseJson;
                    messageEvent.OCPPStatusId = ocppSuccessStatusId;
                    await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                    _logger.Debug("response: " + responseJson);
                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseJson)),
                        WebSocketMessageType.Binary, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    int ocppStatusId =
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