using Abp.Application.Services;
using Sixoclock.Onyx.ModelFeatures.Dto;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ModelFeatures
{
    public interface IModelFeatureAppService : IApplicationService
    {
        GetModelFeaturesListOutput GetModelFeaturesList(int chargepointModelConnectorId);
        Task DeleteModelFeature(ModelFeatureListDto input);
        Task CreateOrUpdateModelFeature(CreateOrUpdateModelFeatureInput ocppFeature);
    }
}