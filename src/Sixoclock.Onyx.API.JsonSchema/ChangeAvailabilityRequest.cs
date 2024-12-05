using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class ChangeAvailabilityRequest : BaseDTO<ChangeAvailabilityRequest>
    {
        private int _connectorId;
        private ChangeAvailabilityTypeEnum _type;

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

        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChangeAvailabilityTypeEnum Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                  
                }
            }
        }
    }
}
