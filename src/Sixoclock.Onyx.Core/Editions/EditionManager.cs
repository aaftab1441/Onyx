using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace Sixoclock.Onyx.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Basic";
        public const string ProEditionName = "Pro";
        public const string IntegratorEditionName = "Integrator";

        public EditionManager(
            IRepository<Edition> editionRepository,
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore
            )
        {
        }

        public async Task<List<Edition>> GetAllAsync()
        {
            return await EditionRepository.GetAllListAsync();
        }
    }
}
