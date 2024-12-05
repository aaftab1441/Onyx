using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public partial class BootNotificationResponse : BaseDTO<AuthorizeRequest>
    {
        private BootNotificationResponseStatus _status;
        private System.String _currentTime;
        private int _interval;

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public BootNotificationResponseStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("currentTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.String CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("interval", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Interval
        {
            get { return _interval; }
            set
            {
                if (_interval != value)
                {
                    _interval = value;

                }
            }
        }

    }
}
