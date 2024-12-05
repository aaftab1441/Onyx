using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class CreateOrUpdateCustomerServiceInputDto
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
    }
}
