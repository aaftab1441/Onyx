namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class CallErrorResponse
    {
        public OCPPErrorCode ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public object ErrorDetails { get; set; }
    }
}
