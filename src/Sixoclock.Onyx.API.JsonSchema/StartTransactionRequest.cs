using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public partial class StartTransactionRequest : BaseDTO<StartTransactionRequest>
    {
        private int _connectorId;
        private string _idTag;
        private int _meterStart;
        private int? _reservationId;
        private System.DateTime _timestamp;

        [Newtonsoft.Json.JsonProperty("connectorId", Required = Newtonsoft.Json.Required.Always)]
        public int ConnectorId
        {
            get => _connectorId;
            set
            {
                if (_connectorId != value)
                {
                    _connectorId = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("idTag", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
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

        [Newtonsoft.Json.JsonProperty("meterStart", Required = Newtonsoft.Json.Required.Always)]
        public int MeterStart
        {
            get { return _meterStart; }
            set
            {
                if (_meterStart != value)
                {
                    _meterStart = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("reservationId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ReservationId
        {
            get { return _reservationId; }
            set
            {
                if (_reservationId != value)
                {
                    _reservationId = value;
                  
                }
            }
        }

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
    }
}
