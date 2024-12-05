namespace Sixoclock.Onyx.API.JsonSchema.Models
{
   public  class ChargingSchedulePeriod
    {
        private int _startPeriod;
        private double _limit;
        private int? _numberPhases;

        [Newtonsoft.Json.JsonProperty("startPeriod", Required = Newtonsoft.Json.Required.Always)]
        public int StartPeriod
        {
            get { return _startPeriod; }
            set
            {
                if (_startPeriod != value)
                {
                    _startPeriod = value;
                
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("limit", Required = Newtonsoft.Json.Required.Always)]
        public double Limit
        {
            get { return _limit; }
            set
            {
                if (_limit != value)
                {
                    _limit = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("numberPhases", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? NumberPhases
        {
            get { return _numberPhases; }
            set
            {
                if (_numberPhases != value)
                {
                    _numberPhases = value;
                   
                }
            }
        }
    }
}
