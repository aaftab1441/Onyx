using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Sixoclock.Onyx.API.Json.MessageBus;
using Sixoclock.Onyx.MultiTenancy;

namespace Sixoclock.Onyx.API.Json.WebSockets
{
    public class OnyxWebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly OnyxWebSocketManager _socketManager;
        private readonly IWebSocketOcppFramework _ocppFramework;
        private readonly ILogger _logger;
        private readonly IRepository<Chargepoint> _chargePointRepo;
        private readonly TenantManager _tenantManager;
        private readonly OnyxMessageBusManager _messageBusManager;
        public OnyxWebSocketMiddleware(RequestDelegate next,
                                            OnyxWebSocketManager socketManager, IWebSocketOcppFramework oCPPF, ILogger logger, IRepository<Chargepoint> chargePointRepo, TenantManager tenantManager, OnyxMessageBusManager messageBusManager)
        {
            _next = next;
            _socketManager = socketManager;
            _ocppFramework = oCPPF;
            _logger = logger;
            _chargePointRepo = chargePointRepo;
            _tenantManager = tenantManager;
            _messageBusManager = messageBusManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }
         
            //handshake
            #region handshake

       
            string tenancyName = context.Request.Path.Value.TrimStart('/').Split('/').FirstOrDefault();
            string identity = context.Request.Path.Value.TrimStart('/').Split('/').LastOrDefault();
            Tenant tenant = (await _tenantManager.FindByTenancyNameAsync(tenancyName));
            if (string.IsNullOrEmpty(tenancyName))
            {
                _logger.DebugFormat("New websocket handshake request recieved but TenancyName did not matched: {0}");
                context.Response.StatusCode = 400;
                return;
            }
            var supportedProtocols = context.Request.Headers.ContainsKey("Sec-WebSocket-Protocol") ? context.Request.Headers["Sec-WebSocket-Protocol"].FirstOrDefault() : string.Empty;

                if (string.IsNullOrEmpty(identity) || string.IsNullOrEmpty(supportedProtocols))
                {
                    _logger.DebugFormat("New websocket handshake request recieved but missing either identity or supported Protocols");
                    context.Response.StatusCode = 400;
                    return;
                }
                _logger.DebugFormat("New websocket handshake: Identity {0} , SupportedProtocols: {1}", identity, supportedProtocols);
                var cp = await _chargePointRepo.FirstOrDefaultAsync(x => x.Identity == identity);
            if (cp == null)
            {
                _logger.DebugFormat("New websocket handshake request recieved but identity did not matched: {0}", identity);
                context.Response.StatusCode = 400;
                return;
            }


                //if (cp.OCPPVersion.VersionName.Equals(supportedProtocols.Trim().Replace(".", ""), StringComparison.CurrentCultureIgnoreCase))
                //{
                //    _logger.DebugFormat("New websocket handshake request recieved but supported protocol did not matched: {0}", supportedProtocols);
                //    context.Response.Headers.Add(new KeyValuePair<string, StringValues>("Sec-WebSocket-Protocol", ""));
                //    return;
                //}
                context.Response.Headers.Add(new KeyValuePair<string, StringValues>("Sec-WebSocket-Protocol", supportedProtocols));

                #endregion
                var innersocket = await context.WebSockets.AcceptWebSocketAsync();           
            var socket = new OnyxWebSocket(tenant.Id, identity, supportedProtocols, innersocket);
            var id = _socketManager.AddSocket(socket);
            try
            {
                await Receive(socket.Connection, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _socketManager.RemoveSocket(id);
                        return;
                    }
                    var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    _logger?.DebugFormat("Request at WebSocketMiddleWare: \n {0}", message);
                    await this._ocppFramework.ProcessMessageAsync(message, this._socketManager.GetId(socket));
                });
            }
            catch (Exception e)
            {
               _logger.Debug("Exception on socket: "+e.Message.ToString());
                await _socketManager.RemoveSocket(id);

            }
           

           
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
