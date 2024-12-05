namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class SampledValueItem
    {
        private string _value;
        private ContextEnum? _context;
        private FormatEnum? _format;
        private MeasurandEnum? _measurand;
        private PhaseEnum? _phase;
        private LocationEnum? _location;
        private UnitEnum? _unit;
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("context", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ContextEnum? Context
        {
            get { return _context; }
            set
            {
                if (_context != value)
                {
                    _context = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("format", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public FormatEnum? Format
        {
            get { return _format; }
            set
            {
                if (_format != value)
                {
                    _format = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("measurand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public MeasurandEnum? Measurand
        {
            get { return _measurand; }
            set
            {
                if (_measurand != value)
                {
                    _measurand = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("phase", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhaseEnum? Phase
        {
            get { return _phase; }
            set
            {
                if (_phase != value)
                {
                    _phase = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("location", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public LocationEnum? Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("unit", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UnitEnum? Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                 
                }
            }
        }
    }
}
