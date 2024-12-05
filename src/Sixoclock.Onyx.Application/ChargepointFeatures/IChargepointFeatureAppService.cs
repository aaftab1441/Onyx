using System.Threading.Tasks;
using Sixoclock.Onyx.ChargepointFeatures.Dto;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargepointFeatures
{
    public interface IChargepointFeatureAppService : IApplicationService
    {
        Task CreateOrUpdateChargepointFeature(CreateOrUpdateChargepointFeatureInput ocppFeature);
        Task DeleteChargepointFeature(ChargepointFeatureListDto input);
        GetChargepointFeaturesListOutput GetChargepointFeaturesList(int chargepointId);
        Task DeleteChargepointFeaturesByChargepoint(EntityDto<int> input, int tenantId);
        Task CopyModelFeaturesToChargepointFeatures(int ChargepointModelId, int chargepointId);
        Task CreateChargepointFeaturesAndKeys(List<ChargepointFeature> chargepointFeature, int chargepointId);
    }
}