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
    [OCPPAction(Name = "Authorize", Type = RequestHandlerType.CPTOCS)]
    public class AuthorizeRequestHandler : IRequestHandler
    {
        private readonly OnyxWebSocketManager _webSocketManager;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPMessageEvent> _ocppMessageEventRepository;
        private readonly IRepository<Tag> _idTagRepository;
        private readonly IRepository<ParentTag> _parentTagRepository;
        private readonly IRepository<AuthorizationStatus> _authStatusRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuthorizeRequestHandler(OnyxWebSocketManager webSocketManager,
            ILogger logger,
            IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPMessageEvent> ocppMessageEventRepository, IRepository<Tag> idTagRepository,
            IRepository<Chargepoint> chargepointRepository1, IUnitOfWorkManager unitOfWorkManager, IRepository<ParentTag> parentTagRepository, IRepository<AuthorizationStatus> authStatusRepository)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppMessageEventRepository = ocppMessageEventRepository;
            _idTagRepository = idTagRepository;
            _chargepointRepository = chargepointRepository1;
            _unitOfWorkManager = unitOfWorkManager;
            _parentTagRepository = parentTagRepository;
            _authStatusRepository = authStatusRepository;
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
                    var messageOb = JsonConvert.DeserializeObject<AuthorizeRequest>(message.PayLoad);
                    int ocppPendingStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Pending))
                        .Id;

                    cp = await _chargepointRepository.FirstOrDefaultAsync(x => x.Identity == socket.Identity);
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
                    var idTag = await _idTagRepository.FirstOrDefaultAsync(x => x.IdToken == messageOb.IdTag);
                    AuthorizeResponse authorizeResponse;
                    if (idTag != null)
                    {
                        var authStatus =
                            await _authStatusRepository.FirstOrDefaultAsync(x => x.Id == idTag.AuthorizationStatusId);
                        var parentTag = await _parentTagRepository.FirstOrDefaultAsync(x => x.Id == idTag.ParentTagId);


                        authorizeResponse = new AuthorizeResponse
                        {
                            IdTagInfo = new IdTagInfo
                            {
                                ExpiryDate = "2018-01-22T10:39:59.630094",
                                ParentIdTag = idTag.ParentTag != null ? parentTag.Value : null,
                                Status = EnumExtensions.ToEnum<StatusEnum>(authStatus.Value)
                            }
                        };

                    }
                    else
                    {
                        authorizeResponse = new AuthorizeResponse()
                        {
                            IdTagInfo = new IdTagInfo()
                            {

                                Status = StatusEnum.Invalid
                            }
                        };
                    }
                    response.Add(3);
                    response.Add(message.UniqueId);
                    response.Add(authorizeResponse);
                    int ocppSuccessStatusId =
                        (await _ocppStatusRepository.FirstOrDefaultAsync(x => x.Status == OCPPStatusConstants.Success))
                        .Id;

                    var responseJson = JsonConvert.SerializeObject(response);
                    _logger.Debug("response: " + responseJson);
                    await socket.Connection.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseJson)),
                        WebSocketMessageType.Binary, true, CancellationToken.None);
                    messageEvent.Response = JsonConvert.SerializeObject(response);
                    messageEvent.OCPPStatusId = ocppSuccessStatusId;
                    await _ocppMessageEventRepository.UpdateAsync(messageEvent);
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
