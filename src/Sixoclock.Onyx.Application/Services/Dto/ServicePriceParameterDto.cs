using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Services.Dto
{
    [AutoMapFrom(typeof(ServicePriceParameter))]
    [AutoMapTo(typeof(ServicePriceParameter))]
    public class ServicePriceParameterDto:FullAuditedEntityDto
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int ServiceId { get; set; }
    }
}
