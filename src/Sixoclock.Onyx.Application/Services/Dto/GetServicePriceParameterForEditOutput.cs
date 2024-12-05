using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Services.Dto
{
    public class GetServicePriceParameterForEditOutput
    {
        public ServicePriceParameterDto PriceParameter { get; set; }
        public List<ComboboxItemDto> PriceParametersNames { get; set; }
    }
}
