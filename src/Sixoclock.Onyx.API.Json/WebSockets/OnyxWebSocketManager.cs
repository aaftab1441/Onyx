using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.API.Json.WebSockets
{
    public class OnyxWebSocketManager
    {
        private static readonly ConcurrentDictionary<string, OnyxWebSocket> Sockets = new ConcurrentDictionary<string, OnyxWebSocket>();

        public OnyxWebSocket GetSocketById(string id)
        {
            return Sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, OnyxWebSocket> GetAll()
        {
            return Sockets;
        }

        public string GetId(OnyxWebSocket socket)
        {
            return Sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public string AddSocket(OnyxWebSocket socket)
        {
            var id = CreateConnectionId();
            Sockets.TryAdd(id, socket);

            return id;
        }

        public string GetIdByTenantAndIdentity(string identity, int tenantId)
        {
            return Sockets.FirstOrDefault(x => x.Value.Identity.ToLower() == identity.ToLower() && x.Value.TenantId == tenantId).Key;
        }
        public async Task RemoveSocket(string id)
        {
            Sockets.TryRemove(id, out var socket);

            await socket.Connection.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in Sockets)
            {
                if (pair.Value.Connection.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        private async Task SendMessageAsync(OnyxWebSocket socket, string message)
        {
            if (socket.Connection.State != WebSocketState.Open)
                return;

            await socket.Connection.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }
    }
}
