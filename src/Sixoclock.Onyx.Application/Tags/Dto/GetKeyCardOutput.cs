using System;

namespace Sixoclock.Onyx.Tags.Dto
{
    public class GetKeyCardOutput
    {
        public int Id { get; set; }
        public string IdToken { get; set; }
        public string AuthorizationStatus { get; set; }
        public float KwhCharged { get; set; }
        public int Transactions { get; set; }
        public DateTime? Expiry { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
