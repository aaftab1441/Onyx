using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ModelFeatures.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ModelFeatures
{
    public class ModelFeatureAppService : OnyxAppServiceBase, IModelFeatureAppService
    {
        private readonly IRepository<ModelFeature> _modelFeatureRepository;
        public ModelFeatureAppService(IRepository<ModelFeature> modelFeatureRepository)
        {
            _modelFeatureRepository = modelFeatureRepository;
        }

        public GetModelFeaturesListOutput GetModelFeaturesList(int chargepointModelId)
        {
            IEnumerable<ModelFeatureListDto> _modelFeaturesList = from modelFeature in _modelFeatureRepository.GetAll().Include(f => f.ChargepointModel.OCPPVersion.OCPPFeatures).Where(f => f.ChargepointModelId == chargepointModelId)
                                                                  select new ModelFeatureListDto
                                                                  {
                                                                      Id = modelFeature.Id,
                                                                      ChargepointModelId = modelFeature.ChargepointModelId,
                                                                      FeatureName = modelFeature.ChargepointModel.OCPPVersion.OCPPFeatures.FirstOrDefault().FeatureName,
                                                                      OCPPFeatureId = modelFeature.OCPPFeatureId
                                                                  };
            return new GetModelFeaturesListOutput { ModelFeatures = _modelFeaturesList.ToList() };
        }
        public async Task CreateOrUpdateModelFeature(CreateOrUpdateModelFeatureInput ocppFeature)
        {
            CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete);

            ocppFeature.ModelFeature.TenantId = GetCurrentTenant().Id;
            var option = _modelFeatureRepository.GetAll().Where(f => f.OCPPFeatureId == ocppFeature.ModelFeature.OCPPFeatureId && f.ChargepointModelId == ocppFeature.ChargepointModelId).FirstOrDefault();
            if (option == null)
            {
                ocppFeature.ModelFeature.ChargepointModelId = ocppFeature.ChargepointModelId;
                await _modelFeatureRepository.InsertAsync(ocppFeature.ModelFeature);
            }
            else
            {
                option.IsDeleted = false;
                option.DeleterUserId = null;
                option.DeletionTime = null;
                option.LastModificationTime = null;
                await _modelFeatureRepository.UpdateAsync(option);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task DeleteModelFeature(ModelFeatureListDto input)
        {
            var feature = await _modelFeatureRepository.GetAsync(input.Id);
            await _modelFeatureRepository.DeleteAsync(feature);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
