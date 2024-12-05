using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;
using Sixoclock.Onyx.ModelKeyValues;
using Sixoclock.Onyx.ModelKeyValues.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ChargepointKeyValues
{
    public class ChargepointKeyValueAppService : OnyxAppServiceBase, IChargepointKeyValueAppService
    {
        private readonly IRepository<ChargepointKeyValue> _chargepointKeyValueRepository;
        private readonly IModelKeyValueAppService _modelKeyValueAppService;

        public ChargepointKeyValueAppService(IRepository<ChargepointKeyValue> chargepointKeyValueRepository, IModelKeyValueAppService modelKeyValueAppService)
        {
            _chargepointKeyValueRepository = chargepointKeyValueRepository;
            _modelKeyValueAppService = modelKeyValueAppService;
        }

        public async Task CreateOrUpdateChargepointKeyValue(CreateOrUpdateChargepointKeyValueInput input)
        {
            CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete);

            input.ChargepointKeyValue.TenantId = GetCurrentTenant().Id;
            var option = _chargepointKeyValueRepository.GetAll().Where(f => f.ChargepointId == input.ChargepointId && f.KeyValueId == input.ChargepointKeyValue.KeyValueId).FirstOrDefault();

            if (option == null)
            {
                input.ChargepointKeyValue.ChargepointId = input.ChargepointId;
                await _chargepointKeyValueRepository.InsertAsync(input.ChargepointKeyValue);
            }
            else
            {
                option.IsDeleted = false;
                option.DeleterUserId = null;
                option.DeletionTime = null;
                option.LastModificationTime = null;
                await _chargepointKeyValueRepository.UpdateAsync(option);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task UpdateChargepointKeyValue(UpdateChargepointKeyValueInput input)
        {
            var chargepoint = ObjectMapper.Map<ChargepointKeyValue>(input);
            
            await _chargepointKeyValueRepository.UpdateAsync(chargepoint);
        }
        public async Task<GetChargepointKeyValueForEditOutput> GetChargepointKeyValueForEdit(EntityDto<int> input)
        {
            //Editing an existing Chargepoint key value
            var output = new GetChargepointKeyValueForEditOutput();
            if (input.Id == 0)
            {
                output.ChargepointKeyValue = new ChargepointKeyValueDto();
            }
            else
            {
                var chargepointKeyValue = await _chargepointKeyValueRepository.GetAsync(input.Id);

                output.ChargepointKeyValue = ObjectMapper.Map<ChargepointKeyValueDto>(chargepointKeyValue);
            }

            return output;
        }

        public async Task<PagedResultDto<ChargepointKeyValueDto>> GetChargepointKeyValue(GetChargepointKeyValueInput input)
        {
            var query = (from chargepointKeyValue in _chargepointKeyValueRepository.GetAll().Include(m => m.Chargepoint)
                         .Include(m => m.Chargepoint.ChargepointModel.Vendor)
                         select new ChargepointKeyValueDto
                         {
                             Id = chargepointKeyValue.Id,
                             ChargepointValue = chargepointKeyValue.ChargepointValue,
                             Comment = chargepointKeyValue.Comment,
                             RW = chargepointKeyValue.RW,
                             TenantId = chargepointKeyValue.TenantId,
                             ChargepointId = chargepointKeyValue.ChargepointId,
                             CreationTime = chargepointKeyValue.CreationTime,
                             VendorName = chargepointKeyValue.Chargepoint.ChargepointModel.Vendor.Name,
                             ModelName = chargepointKeyValue.Chargepoint.ChargepointModel.ModelName,
                             Identity = chargepointKeyValue.Chargepoint.Identity,
                             VersionName = chargepointKeyValue.Chargepoint.ChargepointModel.OCPPVersion.VersionName,
                             FeatureName = chargepointKeyValue.FeatureName,
                             Key = chargepointKeyValue.Key
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.VendorName.Contains(input.Filter) || item.ModelName.Contains(input.Filter) || item.ChargepointValue.Contains(input.Filter))
                .WhereIf(!input.ChargepointValue.IsNullOrWhiteSpace(), item => item.ChargepointValue == input.ChargepointValue)
                .WhereIf(!input.ModelName.IsNullOrWhiteSpace(), item => item.ModelName == input.ModelName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargepointKeyValueDto>(resultCount, results.ToList());
        }

        public async Task CopyModelKeyValuesToChargepointKeyValues(int chargepointModelId, int chargepointId)
        {
            int tenantId = GetCurrentTenant().Id;

            CreateOrUpdateChargepointKeyValueInput chargepointKeyValue;
            GetModelKeyValuesListByChargepointModelIdListInput modelKeyValueInput = new GetModelKeyValuesListByChargepointModelIdListInput();
            modelKeyValueInput.TenantId = tenantId;
            modelKeyValueInput.ChargepointModelId = chargepointModelId;
            var modelKeyValues = _modelKeyValueAppService.GetModelKeyValuesListByChargepointModelId(modelKeyValueInput);

            foreach (var modelKeyValue in modelKeyValues.ModelKeyValues)
            {
                chargepointKeyValue = new CreateOrUpdateChargepointKeyValueInput();
                chargepointKeyValue.ChargepointId = chargepointId;
                chargepointKeyValue.ChargepointKeyValue = new ChargepointKeyValue();
                chargepointKeyValue.ChargepointKeyValue.RW = modelKeyValue.RW;
                chargepointKeyValue.ChargepointKeyValue.ChargepointValue = modelKeyValue.ModelValue;
                chargepointKeyValue.ChargepointKeyValue.KeyValueId = modelKeyValue.KeyValueId;
                chargepointKeyValue.ChargepointKeyValue.Key = modelKeyValue.Key;
                chargepointKeyValue.ChargepointKeyValue.FeatureName = modelKeyValue.FeatureName;
                chargepointKeyValue.ChargepointKeyValue.Comment = modelKeyValue.Comment;

                await CreateOrUpdateChargepointKeyValue(chargepointKeyValue);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteChargepointKeyValue(EntityDto<int> input)
        {
            var modelKeyValue = await _chargepointKeyValueRepository.GetAsync(input.Id);
            await _chargepointKeyValueRepository.DeleteAsync(modelKeyValue);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteChargepointKeyValuesByChargepoint(EntityDto<int> input)
        {
            var keyValues = _chargepointKeyValueRepository.GetAll().Where(f => f.ChargepointId == input.Id);
            foreach (var keyvalue in keyValues)
            {
                await _chargepointKeyValueRepository.DeleteAsync(keyvalue);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<ChargepointKeyValueDto>> GetChargepointKeyValuesListByChargepointAndTenant(GetChargepointKeyValuesListByChargepointInput input)
        {
            var query = (from chargepointKeyValue in _chargepointKeyValueRepository.GetAll().Where(k => k.TenantId == input.TenantId && k.ChargepointId == input.ChargepointId)
                         select new ChargepointKeyValueDto
                         {
                             Id = chargepointKeyValue.Id,
                             ChargepointValue = chargepointKeyValue.ChargepointValue,
                             RW = chargepointKeyValue.RW,
                             Comment = chargepointKeyValue.Comment,
                             WildValue = chargepointKeyValue.WildValue,
                             ChargepointId = chargepointKeyValue.ChargepointId,
                             ModelKeyValueId = chargepointKeyValue.KeyValueId,
                             Key = chargepointKeyValue.Key,
                             FeatureName = chargepointKeyValue.FeatureName,
                             TenantId = chargepointKeyValue.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargepointKeyValueDto>(resultCount, results.ToList());
        }
        public async Task<PagedResultDto<ChargepointKeyValueDto>> GetChargepointKeyValuesListByChargepoint(GetChargepointKeyValuesListByChargepointInput input)
        {
            var query = (from chargepointKeyValue in _chargepointKeyValueRepository.GetAll().Where(k => k.ChargepointId == input.ChargepointId)
                         select new ChargepointKeyValueDto
                         {
                             Id = chargepointKeyValue.Id,
                             ChargepointValue = chargepointKeyValue.ChargepointValue,
                             RW = chargepointKeyValue.RW,
                             Comment = chargepointKeyValue.Comment,
                             
                             ChargepointId = chargepointKeyValue.ChargepointId,
                             ModelKeyValueId = chargepointKeyValue.KeyValueId,
                             Key = chargepointKeyValue.Key,
                             FeatureName = chargepointKeyValue.FeatureName,
                             TenantId = chargepointKeyValue.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargepointKeyValueDto>(resultCount, results.ToList());
        }
    }
}
