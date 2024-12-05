using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class RemoteStartTransactionRequest: BaseDTO<RemoteStartTransactionRequest>
    {
        private int? _connectorId;
        private string _idTag;
        private ChargingProfile _chargingProfile;
        [Newtonsoft.Json.JsonProperty("connectorId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ConnectorId
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

        [Newtonsoft.Json.JsonProperty("chargingProfile", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ChargingProfile ChargingProfile
        {
            get { return _chargingProfile; }
            set
            {
                if (_chargingProfile != value)
                {
                    _chargingProfile = value;
                   
                }
            }
        }

    }
}
