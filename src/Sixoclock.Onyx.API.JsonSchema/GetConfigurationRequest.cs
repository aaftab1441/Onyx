using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class GetConfigurationRequest:BaseDTO<GetConfigurationRequest>
    {
        private System.Collections.ObjectModel.ObservableCollection<string> _key;

        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.ObjectModel.ObservableCollection<string> Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                }
            }
        }
    }
}
