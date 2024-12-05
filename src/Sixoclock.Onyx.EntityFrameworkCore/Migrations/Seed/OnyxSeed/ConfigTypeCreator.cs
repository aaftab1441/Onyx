using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ConfigTypesCreator
    {
        public List<ConfigType> InitialConfigType => GetInitialConfigTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ConfigType> GetInitialConfigTypes()
        {
            return new List<ConfigType>
            {
                new ConfigType() { Type = "Single", Comment = "Fetch one config keyvalues.", TenantId = _tenantId },
                new ConfigType() { Type = "All", Comment = "Fetch all config keyvalues.", TenantId = _tenantId }
            };
        }

        public ConfigTypesCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateConfigTypes();
        }

        private void CreateConfigTypes()
        {
            foreach (var configType in InitialConfigType)
            {
                AddConfigTypeIfNotExists(configType);
            }
        }

        private void AddConfigTypeIfNotExists(ConfigType configType)
        {
            if (_context.ConfigTypes.Any(l => l.TenantId == _tenantId && l.Type == configType.Type))
            {
                return;
            }

            _context.ConfigTypes.Add(configType);

            _context.SaveChanges();
        }
    }
}
