using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Editions.Dto;

namespace Sixoclock.Onyx.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}