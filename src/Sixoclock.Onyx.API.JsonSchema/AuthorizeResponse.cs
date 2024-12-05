using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public partial class AuthorizeResponse :BaseDTO<AuthorizeRequest>
    {
        private IdTagInfo _idTagInfo = new IdTagInfo();

        [Newtonsoft.Json.JsonProperty("idTagInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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
