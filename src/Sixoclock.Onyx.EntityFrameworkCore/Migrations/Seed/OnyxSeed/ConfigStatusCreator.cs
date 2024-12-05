using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ConfigStatusCreator
    {
        public List<ConfigStatus> InitialConfigStatuses => GetInitialConfigStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ConfigStatus> GetInitialConfigStatuses()
        {
            return new List<ConfigStatus>
            {
                new ConfigStatus() { Value = "Accepted", TenantId = _tenantId },
                new ConfigStatus() { Value = "Rejected", TenantId = _tenantId },
                new ConfigStatus() { Value = "RebootRequired", TenantId = _tenantId },
                new ConfigStatus() { Value = "NotSupported", TenantId = _tenantId },
                new ConfigStatus() { Value = "Initiated", TenantId = _tenantId }
            };
        }

        public ConfigStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateConfigStatuses();
        }

        private void CreateConfigStatuses()
        {
            foreach (var configStatus in InitialConfigStatuses)
            {
                AddConfigStatusIfNotExists(configStatus);
            }
        }

        private void AddConfigStatusIfNotExists(ConfigStatus configStatus)
        {
            if (_context.ConfigStatuses.Any(l => l.TenantId == _tenantId && l.Value == configStatus.Value))
            {
                return;
            }

            _context.ConfigStatuses.Add(configStatus);

            _context.SaveChanges();
        }
    }
}
