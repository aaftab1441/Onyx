using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class ChangeAvailabilityResponse : BaseDTO<ChangeAvailabilityResponse>
    {
        private ChangeAvailabilityStatusEnum _status;
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ChangeAvailabilityStatusEnum Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                  
                }
            }
        }
    }
}
