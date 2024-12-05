using System.Collections.Generic;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class MeterValuesRequest:BaseDTO<AuthorizeRequest>
    {
        private int _connectorId;
        private int? _transactionId;
        private List<MeterValueItem> _meterValue = new List<MeterValueItem>();
        [Newtonsoft.Json.JsonProperty("connectorId", Required = Newtonsoft.Json.Required.Always)]
        public int ConnectorId
        {
            get { return _connectorId; }
            set
            {
                if (_connectorId != value)
                {
                    _connectorId = value;  
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
        public List<MeterValueItem> MeterValue
        {
            get { return _meterValue; }
            set
            {
                if (_meterValue != value)
                {
                    _meterValue = value;
                   
                }
            }
        }
    }
}
