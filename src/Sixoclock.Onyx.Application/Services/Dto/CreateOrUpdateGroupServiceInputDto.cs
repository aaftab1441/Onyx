using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class CreateOrUpdateGroupServiceInputDto
    {
        public int GroupId { get; set; }
        public int ServiceId { get; set; }
    }
}
