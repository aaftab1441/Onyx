using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.OCPPFeatures.Dto;

namespace Sixoclock.Onyx.OCPPFeatures
{
    public interface IOCPPFeatureAppService : IApplicationService
    {
        GetOCPPFeaturesListOutput GetOCPPFeaturesList();
        GetOCPPFeaturesListOutput GetOCPPFeaturesByOCPPVersionList(EntityDto<int> input);
    }
}