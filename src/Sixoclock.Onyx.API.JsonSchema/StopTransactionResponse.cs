using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class StopTransactionResponse:BaseDTO<StopTransactionResponse>
    {
        private IdTagInfo _idTagInfo;

        [Newtonsoft.Json.JsonProperty("idTagInfo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IdTagInfo IdTagInfo
        {
            get { return _idTagInfo; }
            set
            {
                if (_idTagInfo != value)
                {
                    _idTagInfo = value;
                   
                }
            }
        }
    }
}
