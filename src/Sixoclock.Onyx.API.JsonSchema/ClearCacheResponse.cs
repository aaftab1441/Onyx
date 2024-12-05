using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.API.JsonSchema.Base;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.JsonSchema
{
    public class ClearCacheResponse:BaseDTO<ClearCacheResponse>
    {
        private ResponseStatusEnum _status;

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ResponseStatusEnum Status
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
