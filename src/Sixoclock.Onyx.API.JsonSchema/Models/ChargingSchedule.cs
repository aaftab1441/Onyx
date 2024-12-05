using System.Collections.Generic;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class ChargingSchedule
    {
        private int? _duration;
        private System.DateTime? _startSchedule;
        private ChargingRateUnit _chargingRateUnit;
        private double? _minChargingRate;
        private List<ChargingSchedulePeriod> _chargingSchedulePeriod=new List<ChargingSchedulePeriod>();
        [Newtonsoft.Json.JsonProperty("duration", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("startSchedule", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTime? StartSchedule
        {
            get { return _startSchedule; }
            set
            {
                if (_startSchedule != value)
                {
                    _startSchedule = value;
                    
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargingRateUnit", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChargingRateUnit ChargingRateUnit
        {
            get { return _chargingRateUnit; }
            set
            {
                if (_chargingRateUnit != value)
                {
                    _chargingRateUnit = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargingSchedulePeriod", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public List<ChargingSchedulePeriod> ChargingSchedulePeriod
        {
            get { return _chargingSchedulePeriod; }
            set
            {
                if (_chargingSchedulePeriod != value)
                {
                    _chargingSchedulePeriod = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("minChargingRate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? MinChargingRate
        {
            get { return _minChargingRate; }
            set
            {
                if (_minChargingRate != value)
                {
                    _minChargingRate = value;
                   
                }
            }
        }
    }
}
