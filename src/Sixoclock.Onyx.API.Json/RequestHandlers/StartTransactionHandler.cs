﻿using System;
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
    [OCPPAction(Name = "StartTransaction", Type = RequestHandlerType.CPTOCS)]
    public class StartTransactionHandler:IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Tag> _idTagRepository;
        private readonly IRepository<EVSE> _evseRepository;
        private readonly IRepository<TagTransactionType> _tagTransactionTypeRepository;
        private readonly IRepository<ParentTag> _parentTagRepository;
        private readonly IRepository<AuthorizationStatus> _authStatusRepository;
        private readonly IRepository<TransactionStatus> _transactionStatusRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<MeterValue> _meterValueRepository;
        private readonly IRepository<MeterValueType> _meterValueTypeRepository;

        public StartTransactionHandler(OnyxWebSocketManager webSocketManager,
            IRepository<Chargepoint> chargepointRepository,
            ILogger logger,
            IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository,
            IRepository<Tag> idTagRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<EVSE> evseRepository,
            IRepository<TagTransactionType> tagTransactionTypeRepository,
            IRepository<ParentTag> parentTagRepository,
            IRepository<AuthorizationStatus> authStatusRepository,
            IRepository<TransactionStatus> transactionStatusRepository,
            IRepository<Transaction> transactionRepository, IRepository<MeterValue> meterValueRepository, IRepository<MeterValueType> meterValueTypeRepository)
        {
            _webSocketManager = webSocketManager;
            _chargepointRepository = chargepointRepository;
            _logger = logger;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _idTagRepository = idTagRepository;
            _evseRepository = evseRepository;
            _tagTransactionTypeRepository = tagTransactionTypeRepository;
            _parentTagRepository = parentTagRepository;
            _authStatusRepository = authStatusRepository;
            _transactionStatusRepository = transactionStatusRepository;
            _transactionRepository = transactionRepository;
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
                    var messageOb = JsonConvert.DeserializeObject<StartTransactionRequest>(message.PayLoad);
                    _unitOfWorkManager.Current.SetTenantId(socket.TenantId);
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
                    var idTag = await _idTagRepository.FirstOrDefaultAsync(x => x.IdToken == messageOb.IdTag);
                    StartTransactionResponse startTransactionResponse ;
                    if (idTag != null)
                    {
                        var tagTranscationType = await _tagTransactionTypeRepository.FirstOrDefaultAsync(x =>
                            x.Value == EnumExtensions.ToEnumString(TagTransactionTypeEnum
                                .Start));
                        var parentTag = await _parentTagRepository.FirstOrDefaultAsync(x => x.Id == idTag.ParentTagId);
                        var authStatus =
                            await _authStatusRepository.FirstOrDefaultAsync(x => x.Id == idTag.AuthorizationStatusId);
                        var startmeterValueType =
                            await _meterValueTypeRepository.FirstOrDefaultAsync(x => x.Type == "Start");
                        EVSE evse ;
                        using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                        {
                            evse = await _evseRepository.FirstOrDefaultAsync(x =>
                                x.ChargepointId == cp.Id && x.EVSE_id == messageOb.ConnectorId);
                        }
                        if (evse == null)
                        {
                            _logger.Debug($"Invalid ConnectorId: {messageOb.ConnectorId}");
                            throw new Exception($"Invalid ConnectorId: {messageOb.ConnectorId}");
                        }
                        var transactionStatus = await
                            _transactionStatusRepository.FirstOrDefaultAsync(x =>
                                x.Value == EnumExtensions.ToEnumString(
                                    TransactionStatusEnum.Charging));

                        if (authStatus.Value == EnumExtensions.ToEnumString(StatusEnum.Accepted))
                        {
                            var transaction = new Transaction()
                            {
                                TenantId = socket.TenantId,
                                Comment = string.Empty,
                                CreationTime = DateTime.UtcNow,
                                IsDeleted = false,
                                TransactionStatusId = transactionStatus.Id,
                                EVSEId = evse.Id,
                                TransactionStartTime = DateTime.UtcNow

                            };
                            transaction.TagTransactions = new List<TagTransaction>();
                            transaction.TagTransactions.Add(new TagTransaction()
                            {
                                TagId = idTag.Id,
                                TenantId = socket.TenantId,
                                TagTransactioTime = DateTime.UtcNow,
                                CreationTime = DateTime.UtcNow,
                                IsDeleted = false,
                                TagTransactionTypeId = tagTranscationType.Id

                            });
                            var transactionId = await _transactionRepository.InsertAndGetIdAsync(transaction);
                            await _unitOfWorkManager.Current.SaveChangesAsync();
                            startTransactionResponse = new StartTransactionResponse()
                            {
                                IdTagInfo = new IdTagInfo()
                                {
                                    ExpiryDate = "2018-01-22T10:39:59.630094",
                                    ParentIdTag = parentTag?.Value,
                                    Status = EnumExtensions.ToEnum<StatusEnum>(authStatus.Value)
                                },
                                TransactionId = transactionId
                            };
                            var meterValue = new MeterValue
                            {

                                MeterTime = messageOb.Timestamp,
                                TransactionId = transactionId,
                                Value = messageOb.MeterStart,
                                CreationTime = DateTime.UtcNow,
                                MeterValueTypeId = startmeterValueType.Id

                            };
                            await _meterValueRepository.InsertAsync(meterValue);

                        }

                        else
                        {
                            _logger.Debug($"Tag status not accepted TagId: {messageOb.IdTag}, TagStatus: {authStatus.Value}");
                            startTransactionResponse = new StartTransactionResponse()
                            {
                                IdTagInfo = new IdTagInfo()
                                {
                                    Status = StatusEnum.Invalid
                                }
                            };
                        }
                    }
                    else
                    {
                        _logger.Debug($"Invalid Tag id {messageOb.IdTag}");
                        startTransactionResponse = new StartTransactionResponse()
                        {
                            IdTagInfo = new IdTagInfo()
                            {

                                Status = StatusEnum.Invalid
                            }
                        };

                    }






                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(startTransactionResponse);
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
                            await _ocppMessageEventRepository.InsertAsync(new OCPPMessageEvent
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