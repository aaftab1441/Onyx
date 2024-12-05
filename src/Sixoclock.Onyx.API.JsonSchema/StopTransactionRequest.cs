using System.Collections.Generic;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class StopTransactionRequest : BaseDTO<StatusNotificationResponse>
    {
        private string _idTag;
        private int _meterStop;
        private System.DateTime _timestamp;
        private int _transactionId;
        private StopTransactionReasonEnum? _reason;
        private List<MeterValueItem> _transactionData;
        [Newtonsoft.Json.JsonProperty("idTag", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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

        [Newtonsoft.Json.JsonProperty("meterStop", Required = Newtonsoft.Json.Required.Always)]
        public int MeterStop
        {
            get { return _meterStop; }
            set
            {
                if (_meterStop != value)
                {
                    _meterStop = value;
                 
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

        [Newtonsoft.Json.JsonProperty("transactionId", Required = Newtonsoft.Json.Required.Always)]
        public int TransactionId
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

        [Newtonsoft.Json.JsonProperty("reason", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StopTransactionReasonEnum? Reason
        {
            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    
                }
            }
        }
        [Newtonsoft.Json.JsonProperty("transactionData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<MeterValueItem> TransactionData
        {
            get { return _transactionData; }
            set
            {
                if (_transactionData != value)
                {
                    _transactionData = value;
                   
                }
            }
        }


    }
}
