using System.Net.WebSockets;

namespace Sixoclock.Onyx.API.Json.WebSockets
{
    public class OnyxWebSocket
    {
        public string Identity { get; set; }
        public string OcppProtocol { get; set; }
        public WebSocket Connection { get; set; }
        public int TenantId { get; set; }
        public OnyxWebSocket(int tenantId,string identity, string protocol, WebSocket conn)
        {
            TenantId = tenantId;
            Identity = identity;
            OcppProtocol = protocol;
            Connection = conn;
        }

    }
}
