using System;
using System.Collections.Generic;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class RPCMessage
    {
        public MessageType MessageId { get; set; }
        public string UniqueId { get; set; }
        public string PayLoad { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public string ConnectionId { get; set; }
        public bool IsProcessed { get; set; }
        public string Action { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
