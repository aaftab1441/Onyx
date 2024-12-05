using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.Services.Dto
{
    public class GetServicePriceParametersForEditParamInput<T> : EntityDto<T>
    {
        public int ServiceId { get; set; }
    }
}
