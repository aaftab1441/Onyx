using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public partial class BootNotificationRequest :BaseDTO<BootNotificationRequest>
    {
        private string _chargePointVendor;
        private string _chargePointModel;
        private string _chargePointSerialNumber;
        private string _chargeBoxSerialNumber;
        private string _firmwareVersion;
        private string _iccid;
        private string _imsi;
        private string _meterType;
        private string _meterSerialNumber;

        [Newtonsoft.Json.JsonProperty("chargePointVendor", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ChargePointVendor
        {
            get { return _chargePointVendor; }
            set
            {
                if (_chargePointVendor != value)
                {
                    _chargePointVendor = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargePointModel", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ChargePointModel
        {
            get { return _chargePointModel; }
            set
            {
                if (_chargePointModel != value)
                {
                    _chargePointModel = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargePointSerialNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ChargePointSerialNumber
        {
            get { return _chargePointSerialNumber; }
            set
            {
                if (_chargePointSerialNumber != value)
                {
                    _chargePointSerialNumber = value;
                
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("chargeBoxSerialNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ChargeBoxSerialNumber
        {
            get { return _chargeBoxSerialNumber; }
            set
            {
                if (_chargeBoxSerialNumber != value)
                {
                    _chargeBoxSerialNumber = value;
                 
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("firmwareVersion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                if (_firmwareVersion != value)
                {
                    _firmwareVersion = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("iccid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Iccid
        {
            get { return _iccid; }
            set
            {
                if (_iccid != value)
                {
                    _iccid = value;
                
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("imsi", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Imsi
        {
            get { return _imsi; }
            set
            {
                if (_imsi != value)
                {
                    _imsi = value;
                  
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("meterType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MeterType
        {
            get { return _meterType; }
            set
            {
                if (_meterType != value)
                {
                    _meterType = value;
                   
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("meterSerialNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MeterSerialNumber
        {
            get { return _meterSerialNumber; }
            set
            {
                if (_meterSerialNumber != value)
                {
                    _meterSerialNumber = value;
                   
                }
            }
        }

       
    }
}
