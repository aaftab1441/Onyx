using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargepointFeatures.Dto;
using Sixoclock.Onyx.ChargepointKeyValues;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;
using Sixoclock.Onyx.KeyValues;
using Sixoclock.Onyx.KeyValues.Dto;
using Sixoclock.Onyx.ModelFeatures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ChargepointFeatures
{
    public class ChargepointFeatureAppService : OnyxAppServiceBase, IChargepointFeatureAppService
    {
        private readonly IRepository<ChargepointFeature> _chargepointFeatureRepository;
        private readonly IModelFeatureAppService _modelFeatureAppService;
        private readonly IKeyValueAppService _keyValueAppService;
        private readonly IChargepointKeyValueAppService _chargepointKeyValuesAppService;

        public ChargepointFeatureAppService(IRepository<ChargepointFeature> chargepointFeatureRepository, IModelFeatureAppService modelFeatureAppService,
            IChargepointKeyValueAppService chargepointKeyValuesAppService,IKeyValueAppService keyValueAppService)
        {
            _chargepointFeatureRepository = chargepointFeatureRepository;
            _modelFeatureAppService = modelFeatureAppService;
            _chargepointKeyValuesAppService = chargepointKeyValuesAppService;
            _keyValueAppService = keyValueAppService;
        }

        public GetChargepointFeaturesListOutput GetChargepointFeaturesList(int chargepointId)
        {
            IEnumerable<ChargepointFeatureListDto> _chargepointFeaturesList = from chargepointFeature in _chargepointFeatureRepository.GetAll().Include(f => f.Chargepoint.OCPPVersion.OCPPFeatures).Where(f => f.ChargepointId == chargepointId)
                                                                          select new ChargepointFeatureListDto
                                                                          {
                                                                              Id = chargepointFeature.Id,
                                                                              ChargepointId = chargepointFeature.ChargepointId,
                                                                              FeatureName = chargepointFeature.Chargepoint.OCPPVersion.OCPPFeatures.FirstOrDefault().FeatureName,
                                                                              OCPPFeatureId = chargepointFeature.OCPPFeatureId
                                                                          };
            return new GetChargepointFeaturesListOutput { ChargepointFeatures = _chargepointFeaturesList.ToList() };
        }

        public async Task CreateOrUpdateChargepointFeature(CreateOrUpdateChargepointFeatureInput chargepointFeature)
        {
            CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete);

            chargepointFeature.ChargepointFeature.TenantId = GetCurrentTenant().Id;
            var option = _chargepointFeatureRepository.GetAll().Where(f => f.ChargepointId == chargepointFeature.ChargepointFeature.ChargepointId && f.OCPPFeatureId == chargepointFeature.ChargepointFeature.OCPPFeatureId).FirstOrDefault();
            if (option == null)
            {
                chargepointFeature.ChargepointFeature.ChargepointId = chargepointFeature.ChargepointFeature.ChargepointId;
                await _chargepointFeatureRepository.InsertAsync(chargepointFeature.ChargepointFeature);
            }
            else
            {
                option.IsDeleted = false;
                option.DeleterUserId = null;
                option.DeletionTime = null;
                option.LastModificationTime = null;
                await _chargepointFeatureRepository.UpdateAsync(option);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task CopyModelFeaturesToChargepointFeatures(int chargepointModelId,int chargepointId)
        {
            var modelFeatures = _modelFeatureAppService.GetModelFeaturesList(chargepointModelId);
            CreateOrUpdateChargepointFeatureInput chargepointFeature;

            foreach (var modelFeature in modelFeatures.ModelFeatures)
            {
                chargepointFeature = new CreateOrUpdateChargepointFeatureInput();
                chargepointFeature.ChargepointFeature = new ChargepointFeature();
                chargepointFeature.ChargepointFeature.ChargepointId = chargepointId;
                chargepointFeature.ChargepointFeature.OCPPFeatureId = modelFeature.OCPPFeatureId;

                await CreateOrUpdateChargepointFeature(chargepointFeature);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

        }
        public async Task CreateChargepointFeaturesAndKeys(List<ChargepointFeature> chargepointFeature, int chargepointId)
        {
            CreateOrUpdateChargepointFeatureInput feature;
            var tenantId = GetCurrentTenant().Id;
            foreach (var ocppFeature in chargepointFeature)
            {
                feature = new CreateOrUpdateChargepointFeatureInput();
                feature.ChargepointFeature = ocppFeature;
                feature.ChargepointFeature.ChargepointId = chargepointId;

                await CreateOrUpdateChargepointFeature(feature);

                GetKeyValuesListByOCPPFeatureIdListInput keyValueInput = new GetKeyValuesListByOCPPFeatureIdListInput();
                keyValueInput.TenantId = tenantId;
                keyValueInput.OCPPFeatureId = ocppFeature.OCPPFeatureId;
                var keyValues = _keyValueAppService.GetKeyValuesListByOCPPFeatureId(keyValueInput);

                CreateOrUpdateChargepointKeyValueInput chargepointKeyValue;
                foreach (KeyValueByOCPPFeatureIdListDto keyValue in keyValues.KeyValues)
                {
                    chargepointKeyValue = new CreateOrUpdateChargepointKeyValueInput();
                    chargepointKeyValue.ChargepointKeyValue = new ChargepointKeyValue();
                    chargepointKeyValue.ChargepointKeyValue.KeyValueId = keyValue.Id;
                    chargepointKeyValue.ChargepointKeyValue.Key = keyValue.Key;
                    chargepointKeyValue.ChargepointKeyValue.FeatureName = keyValue.FeatureName;
                    chargepointKeyValue.ChargepointId = chargepointId;
                    chargepointKeyValue.ChargepointKeyValue.Comment = keyValue.Comment;
                    chargepointKeyValue.ChargepointKeyValue.ChargepointValue = keyValue.DefaultValue;
                    chargepointKeyValue.ChargepointKeyValue.RW = keyValue.RW;
                    chargepointKeyValue.ChargepointKeyValue.TenantId = tenantId;

                    await _chargepointKeyValuesAppService.CreateOrUpdateChargepointKeyValue(chargepointKeyValue);
                }
            }
        }
        public async Task DeleteChargepointFeaturesByChargepoint(EntityDto<int> input, int tenantId)
        {
            var features = _chargepointFeatureRepository.GetAll().Where(f => f.ChargepointId == input.Id);
            foreach(var feature in features)
            {
                await _chargepointFeatureRepository.DeleteAsync(feature);

                GetChargepointKeyValuesListByChargepointInput chargepointKeyValueInput = new GetChargepointKeyValuesListByChargepointInput();
                chargepointKeyValueInput.TenantId = tenantId;
                chargepointKeyValueInput.ChargepointId = input.Id;
                var keyValues = _chargepointKeyValuesAppService.GetChargepointKeyValuesListByChargepointAndTenant(chargepointKeyValueInput);

                foreach (var chargepointKeyValue in keyValues.Result.Items)
                {
                    await _chargepointKeyValuesAppService.DeleteChargepointKeyValue(chargepointKeyValue);
                }
            }
        }

        public async Task DeleteChargepointFeature(ChargepointFeatureListDto input)
        {
            var feature = await _chargepointFeatureRepository.GetAsync(input.Id);
            await _chargepointFeatureRepository.DeleteAsync(feature);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
