using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class CreateOrUpdateChargepointServiceInputDto
    {
        public int ChargepointId { get; set; }
        public int ServiceId { get; set; }
    }
}
