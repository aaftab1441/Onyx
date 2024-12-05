using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sixoclock.Onyx.API.Json.Utility;
using Sixoclock.Onyx.API.Json.WebSockets;
using Sixoclock.Onyx.API.JsonSchema;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.RequestHandlers
{
    [OCPPAction(Name = "MeterValues", Type = RequestHandlerType.CPTOCS)]
    public class MeterValueRequestHandler : IRequestHandler
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
        private readonly IRepository<MeterValueType> _meterValueTypeRepository;

        public MeterValueRequestHandler(OnyxWebSocketManager webSocketManager, ILogger logger,
            IRepository<Chargepoint> chargepointRepository, IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository, IUnitOfWorkManager unitOfWorkManager,
            IRepository<Transaction> transactionRepository, IRepository<Phase> phaseRepository,
            IRepository<Context> contextRepository, IRepository<Format> formatRepository,
            IRepository<Location> locationRepository, IRepository<Measurand> measurandRepository,
            IRepository<Unit> unitRepository, IRepository<MeterValue> meterValueRepository,
             IRepository<MeterValueType> meterValueTypeRepository)
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
            _meterValueTypeRepository = meterValueTypeRepository;
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
                    var messageOb = JsonConvert.DeserializeObject<MeterValuesRequest>(message.PayLoad);
                    int ocppPendingStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Pending))
                        .Id;

                    cp = await _chargepointRepository.FirstOrDefaultAsync(x => x.Identity == socket.Identity);
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
                    var intermediatemeterValueType = await _meterValueTypeRepository.FirstOrDefaultAsync(x => x.Type == "Intermediate");
                   if(intermediatemeterValueType==null)
                        throw new Exception("IntermediateMeterValueType is not defined");
                    MeterValuesResponse transactionResponse;
                    if (transaction != null)
                    {
                        foreach (var meterValueItem in messageOb.MeterValue)
                        {
                            var timestamp = meterValueItem.Timestamp;
                            foreach (var sampledValueItem in meterValueItem.SampledValue)
                            {
                                var measurand = sampledValueItem.Measurand.HasValue
                                    ? await _measurandRepository.FirstOrDefaultAsync(x =>
                                        x.MeasurandType == EnumExtensions.ToEnumString(sampledValueItem.Measurand.Value))
                                    : null;
                                var context = sampledValueItem.Context.HasValue
                                    ? await _contextRepository.FirstOrDefaultAsync(x =>
                                        x.ContextName == EnumExtensions.ToEnumString(sampledValueItem.Context.Value))
                                    : null;
                                var format = sampledValueItem.Format.HasValue
                                    ? await _formatRepository.FirstOrDefaultAsync(x =>
                                        x.FormatType == EnumExtensions.ToEnumString(sampledValueItem.Format.Value))
                                    : null;
                                var location = sampledValueItem.Location.HasValue
                                    ? await _locationRepository.FirstOrDefaultAsync(x =>
                                        x.LocationName == EnumExtensions.ToEnumString(sampledValueItem.Location.Value))
                                    : null;
                                var phase = sampledValueItem.Phase.HasValue
                                    ? await _phaseRepository.FirstOrDefaultAsync(x =>
                                        x.PhaseName == EnumExtensions.ToEnumString(sampledValueItem.Phase.Value))
                                    : null;
                                var unit = sampledValueItem.Unit.HasValue
                                    ? await _unitRepository.FirstOrDefaultAsync(x =>
                                        x.UnitName == EnumExtensions.ToEnumString(sampledValueItem.Unit.Value))
                                    : null;
                                var meterValue = new MeterValue
                                {
                                    ContextId = context?.Id,
                                    FormatId = format?.Id,
                                    LocationId = location?.Id,
                                    MeasurandId = measurand?.Id,
                                    MeterTime = timestamp,
                                    PhaseId = phase?.Id,
                                    TransactionId = transaction.Id,
                                    Value = float.TryParse(sampledValueItem.Value, out var val) ? val : 0,
                                    CreationTime = DateTime.UtcNow,
                                    UnitId = unit?.Id,
                                    MeterValueTypeId = intermediatemeterValueType.Id
                                };
                                await _meterValueRepository.InsertAsync(meterValue);

                                if (sampledValueItem.Measurand.HasValue && sampledValueItem.Measurand ==
                                    MeasurandEnum.Energy_Active_Import_Register)
                                {
                                    var allMeterValues = await _meterValueRepository.GetAll().Include(x=>x.Measurand).Where(x =>
                                            x.TransactionId == transaction.Id && x.Measurand.MeasurandType ==
                                            EnumExtensions.ToEnumString(MeasurandEnum.Energy_Active_Import_Register))
                                        .OrderBy(x => x.Id).ToListAsync();
                                    MeterValue firstMeterValue=null;
                                    if (allMeterValues.Count > 0)
                                    {
                                         firstMeterValue =
                                            await _meterValueRepository.FirstOrDefaultAsync(x =>
                                                x.Id == allMeterValues.Min(u => u.Id));
                                    }
                                    var newWhDelivered = sampledValueItem.Unit.HasValue
                                        ? sampledValueItem.Unit == UnitEnum.Wh
                                            ? meterValue.Value
                                            : meterValue.Value * 1000
                                        : 0;
                                    float whDelivered;
                                    if (firstMeterValue != null)
                                        whDelivered = newWhDelivered - firstMeterValue.Value;
                                    else
                                        whDelivered = 0;
                                    transaction.KwhDelivered = whDelivered;
                                   
                                }
                                var duration = DateTime.UtcNow - transaction.TransactionStartTime;
                                transaction.Duration = DateTime.Now.Date.AddSeconds(duration.Value.TotalSeconds);
                                await _transactionRepository.UpdateAsync(transaction);
                            }
                        }

                        transactionResponse = new MeterValuesResponse();
                    }
                    else
                    {
                        transactionResponse = new MeterValuesResponse();
                    }
                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(transactionResponse);

                    int ocppSuccessStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x =>
                            x.Status == OCPPStatusConstants.Success))
                        .Id;
                    messageEvent.Response = JsonConvert.SerializeObject(response);
                    messageEvent.OCPPStatusId = ocppSuccessStatusId;
                    await _ocppMessageEventRepository.UpdateAsync(messageEvent);
                    var responseJson = JsonConvert.SerializeObject(response);
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
