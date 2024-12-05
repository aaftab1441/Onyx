using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class GetServiceListOutput
    {
        public IEnumerable<ServiceDto> Services { get; set; }
    }
}
