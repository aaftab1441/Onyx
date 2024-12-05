using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Configs.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Configs
{
    public class ConfigAppService : OnyxAppServiceBase, IConfigAppService
    {
        private readonly IRepository<KeyValue> _configRepository;
        private readonly IRepository<OCPPFeature> _featureRepository;
        public ConfigAppService(IRepository<KeyValue> configRepository,IRepository<OCPPFeature> featureRepository)
        {
            _configRepository = configRepository;
            _featureRepository = featureRepository;
        }
        public async Task CreateOrUpdateConfig(CreateOrUpdateConfigInput input)
        {
            var config = ObjectMapper.Map<KeyValue>(input);

            if (input.Id == 0)
            {
                await _configRepository.InsertAsync(config);
            }
            else
            {
                await _configRepository.UpdateAsync(config);
            }
        }
        public async Task<GetConfigForEditOutput> GetConfigForEdit(EntityDto<int> input)
        {
            //Editing an existing config
            var output = new GetConfigForEditOutput();
            if (input.Id == 0)
            {
                output.Config = new ConfigDto();
            }
            else
            {
                var config = await _configRepository.GetAsync(input.Id);

                output.Config = ObjectMapper.Map<ConfigDto>(config);

                var result = _featureRepository.GetAll().Include(f => f.OCPPVersion).Where(f => f.Id == config.OCPPFeatureId).FirstOrDefault();
                output.Config.FeatureName = result.FeatureName;
                output.Config.VersionName = result.OCPPVersion.VersionName;
            }

            return output;
        }
        public async Task<PagedResultDto<ConfigDto>> GetConfig(GetConfigInput input)
        {
            var query = (from config in _configRepository.GetAll().Include(c => c.OCPPFeature).Include(c => c.OCPPFeature.OCPPVersion)
                         select new ConfigDto
                         {
                             Id = config.Id,
                             Key = config.Key,
                             DefaultValue = config.DefaultValue,
                             RW = config.RW,
                             Comment = config.Comment,
                             FeatureName = config.OCPPFeature.FeatureName,
                             VersionName = config.OCPPFeature.OCPPVersion.VersionName,
                             OCPPFeatureId = config.OCPPFeatureId,
                             OCPPVersionId = config.OCPPFeature.OCPPVersionId,
                             CreationTime = config.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.OCPPFeatureId == input.FeatureId || item.OCPPVersionId == input.VersionId)
                .WhereIf(input.VersionId != 0, item => item.OCPPVersionId == input.VersionId)
                .WhereIf(input.FeatureId != 0, item => item.OCPPFeatureId == input.FeatureId)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ConfigDto>(resultCount, results.ToList());
        }
        public async Task DeleteConfig(EntityDto<int> input)
        {
            var config = await _configRepository.GetAsync(input.Id);
            await _configRepository.DeleteAsync(config);
        }
    }
}
