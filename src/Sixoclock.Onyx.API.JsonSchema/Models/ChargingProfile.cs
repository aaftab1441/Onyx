namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public class ChargingProfile
    {
        private int _chargingProfileId;
        private int? _transactionId;
        private int _stackLevel;
        private ChargingProfilePurposeEnum _chargingProfilePurpose;
        private ChargingProfileKindEnum _chargingProfileKind;
        private ChargingProfileRecurrencyKind? _recurrencyKind;
        private System.DateTime? _validFrom;
        private System.DateTime? _validTo;
        private ChargingSchedule _chargingSchedule = new ChargingSchedule();
        [Newtonsoft.Json.JsonProperty("chargingProfileId", Required = Newtonsoft.Json.Required.Always)]
        public int ChargingProfileId
        {
            get { return _chargingProfileId; }
            set
            {
                if (_chargingProfileId != value)
                {
                    _chargingProfileId = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("transactionId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? TransactionId
        {
            get { return _transactionId; }
            set
            {
                if (_transactionId != value)
                {
                    _transactionId = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("stackLevel", Required = Newtonsoft.Json.Required.Always)]
        public int StackLevel
        {
            get { return _stackLevel; }
            set
            {
                if (_stackLevel != value)
                {
                    _stackLevel = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargingProfilePurpose", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChargingProfilePurposeEnum ChargingProfilePurpose
        {
            get { return _chargingProfilePurpose; }
            set
            {
                if (_chargingProfilePurpose != value)
                {
                    _chargingProfilePurpose = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargingProfileKind", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChargingProfileKindEnum ChargingProfileKind
        {
            get { return _chargingProfileKind; }
            set
            {
                if (_chargingProfileKind != value)
                {
                    _chargingProfileKind = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("recurrencyKind", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChargingProfileRecurrencyKind? RecurrencyKind
        {
            get { return _recurrencyKind; }
            set
            {
                if (_recurrencyKind != value)
                {
                    _recurrencyKind = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("validFrom", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTime? ValidFrom
        {
            get { return _validFrom; }
            set
            {
                if (_validFrom != value)
                {
                    _validFrom = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("validTo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTime? ValidTo
        {
            get { return _validTo; }
            set
            {
                if (_validTo != value)
                {
                    _validTo = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargingSchedule", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public ChargingSchedule ChargingSchedule
        {
            get { return _chargingSchedule; }
            set
            {
                if (_chargingSchedule != value)
                {
                    _chargingSchedule = value;
                
                }
            }
        }
    }
}
