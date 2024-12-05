using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class ResetRequest
    {
        private ResetTypeEnum _type;

        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ResetTypeEnum Type
        {
            get => _type;
            set
            {
                if (_type != value){
                    _type = value;
                  
                }
            }
        }
    }
}
