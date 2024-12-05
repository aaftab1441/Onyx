using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ModelKeyValues.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ModelKeyValues
{
    public class ModelKeyValueAppService : OnyxAppServiceBase, IModelKeyValueAppService
    {
        private readonly IRepository<ModelKeyValue> _modelKeyValueRepository;
        public ModelKeyValueAppService(IRepository<ModelKeyValue> modelKeyValueRepository)
        {
            _modelKeyValueRepository = modelKeyValueRepository;
        }
        public async Task CreateOrUpdateModelKeyValue(CreateOrUpdateModelKeyValueInput input)
        {
            CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete);

            input.ModelKeyValue.TenantId = GetCurrentTenant().Id;
            var option = _modelKeyValueRepository.GetAll().Where(f => f.KeyValueId == input.ModelKeyValue.KeyValueId && f.ChargepointModelId == input.ChargepointModelId).FirstOrDefault();

            if (option == null)
            {
                input.ModelKeyValue.ChargepointModelId = input.ChargepointModelId;
                await _modelKeyValueRepository.InsertAsync(input.ModelKeyValue);
            }
            else
            {
                option.IsDeleted = false;
                option.DeleterUserId = null;
                option.DeletionTime = null;
                option.LastModificationTime = null;
                await _modelKeyValueRepository.UpdateAsync(option);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task UpdateModelKeyValue(UpdateModelKeyValueInput input)
        {
            var model = ObjectMapper.Map<ModelKeyValue>(input);
            await _modelKeyValueRepository.UpdateAsync(model);
        }
        public async Task<GetModelKeyValueForEditOutput> GetModelKeyValueForEdit(EntityDto<int> input)
        {
            //Editing an existing model key value
            var output = new GetModelKeyValueForEditOutput();
            if (input.Id == 0)
            {
                output.ModelKeyValue = new ModelKeyValueDto();
            }
            else
            {
                var modelKeyValue = await _modelKeyValueRepository.GetAsync(input.Id);

                output.ModelKeyValue = ObjectMapper.Map<ModelKeyValueDto>(modelKeyValue);
            }

            return output;
        }
        public async Task<PagedResultDto<ModelKeyValueDto>> GetModelKeyValue(GetModelKeyValueInput input)
        {
            var query = (from modelKeyValue in _modelKeyValueRepository.GetAll().Include(m => m.ChargepointModel)
                                 .Include(m => m.ChargepointModel.Vendor).Include(m => m.ChargepointModel.OCPPVersion)
                         select new ModelKeyValueDto
                         {
                             Id = modelKeyValue.Id,
                             ModelValue = modelKeyValue.ModelValue,
                             Comment = modelKeyValue.Comment,
                             RW = modelKeyValue.RW,
                             TenantId = modelKeyValue.TenantId,
                             ChargepointModelId = modelKeyValue.ChargepointModelId,
                             VendorName = modelKeyValue.ChargepointModel.Vendor.Name,
                             ModelName = modelKeyValue.ChargepointModel.ModelName,
                             VersionName = modelKeyValue.ChargepointModel.OCPPVersion.VersionName,
                             FeatureName = modelKeyValue.FeatureName,
                             Key = modelKeyValue.Key,
                             CreationTime = modelKeyValue.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelValue.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.ModelValue.IsNullOrWhiteSpace(), item => item.ModelValue == input.ModelValue)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ModelKeyValueDto>(resultCount, results.ToList());
        }
        public GetModelKeyValuesListByChargepointModelIdListOutput GetModelKeyValuesListByChargepointModelId(GetModelKeyValuesListByChargepointModelIdListInput input)
        {
            IEnumerable<ModelKeyValueByChargepointModelIdListDto> _modelKeyValuesList = from modelKeyValue in _modelKeyValueRepository.GetAll().Where(k => k.TenantId == input.TenantId && k.ChargepointModelId == input.ChargepointModelId)
                                                                                      select new ModelKeyValueByChargepointModelIdListDto
                                                                                      {
                                                                                          Id = modelKeyValue.Id,
                                                                                          ModelValue = modelKeyValue.ModelValue,
                                                                                          RW = modelKeyValue.RW,
                                                                                          Comment = modelKeyValue.Comment,
                                                                                          ChargepointModelId = modelKeyValue.ChargepointModelId,
                                                                                          KeyValueId = modelKeyValue.KeyValueId,
                                                                                          Key = modelKeyValue.Key,
                                                                                          FeatureName = modelKeyValue.FeatureName,
                                                                                          TenantId = modelKeyValue.TenantId
                                                                                      };
            return new GetModelKeyValuesListByChargepointModelIdListOutput { ModelKeyValues = _modelKeyValuesList.ToList() };
        }
        public async Task DeleteModelKeyValue(ModelKeyValueByChargepointModelIdListDto input)
        {
            var modelKeyValue = await _modelKeyValueRepository.GetAsync(input.Id);
            await _modelKeyValueRepository.DeleteAsync(modelKeyValue);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
