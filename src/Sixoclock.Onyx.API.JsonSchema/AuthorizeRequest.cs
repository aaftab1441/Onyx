using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
  
    public partial class AuthorizeRequest : BaseDTO<AuthorizeRequest>
    {
        private string _idTag;

        [Newtonsoft.Json.JsonProperty("idTag", Required = Newtonsoft.Json.Required.Always)]
        public string IdTag
        {
            get { return _idTag; }
            set
            {
                if (_idTag != value)
                {
                    _idTag = value;                  
                }
            }
        }
    }
}
