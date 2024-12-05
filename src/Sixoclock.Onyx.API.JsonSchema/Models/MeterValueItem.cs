using System.Collections.Generic;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class MeterValueItem
    {
        private System.DateTime _timestamp;
        private List<SampledValueItem> _sampledValue = new List<SampledValueItem>();

        [Newtonsoft.Json.JsonProperty("timestamp", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("sampledValue", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public List<SampledValueItem> SampledValue
        {
            get { return _sampledValue; }
            set
            {
                if (_sampledValue != value)
                {
                    _sampledValue = value;
                    
                }
            }
        }
    }
}
