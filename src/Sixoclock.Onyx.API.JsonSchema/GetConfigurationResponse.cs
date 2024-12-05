using System.Collections.Generic;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class GetConfigurationResponse:BaseDTO<GetConfigurationResponse>
    {
        private List<ConfigurationKey> _configurationKey;
        private List<string> _unknownKey;

        [Newtonsoft.Json.JsonProperty("configurationKey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<ConfigurationKey> ConfigurationKey
        {
            get { return _configurationKey; }
            set
            {
                if (_configurationKey != value)
                {
                    _configurationKey = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("unknownKey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> UnknownKey
        {
            get { return _unknownKey; }
            set
            {
                if (_unknownKey != value)
                {
                    _unknownKey = value;
                   
                }
            }
        }
    }
}
