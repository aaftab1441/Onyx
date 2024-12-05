using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class GetServicePriceParameterListOutput
    {
        public IEnumerable<ServicePriceParameterDto> PriceParameters { get; set; }
    }
}
