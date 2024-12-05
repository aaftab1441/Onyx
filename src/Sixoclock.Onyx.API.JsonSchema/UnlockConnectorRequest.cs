using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.API.JsonSchema.Base;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class UnlockConnectorRequest : BaseDTO<UnlockConnectorRequest>
    {
        private int _connectorId;

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
    }
}
