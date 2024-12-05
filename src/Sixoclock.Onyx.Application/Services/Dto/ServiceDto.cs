using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Services.Dto
{
    [AutoMapFrom(typeof(Service))]
    [AutoMapTo(typeof(Service))]
    public class ServiceDto:FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public int PricingParamsCount { get; set; }

    }
}
