using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ClearCacheStatusCreator
    {
        public List<ClearCacheStatus> InitialClearCacheStatuses => GetInitialclearCacheStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ClearCacheStatus> GetInitialclearCacheStatuses()
        {
            return new List<ClearCacheStatus>
            {
                new ClearCacheStatus() { Value = "Accepted", TenantId = _tenantId },
                new ClearCacheStatus() { Value = "Rejected", TenantId = _tenantId },
                new ClearCacheStatus() { Value = "Initiated", TenantId = _tenantId }
            };
        }

        public ClearCacheStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateClearCacheStatuses();
        }

        private void CreateClearCacheStatuses()
        {
            foreach (var clearCacheStatus in InitialClearCacheStatuses)
            {
                AddClearCacheStatusIfNotExists(clearCacheStatus);
            }
        }

        private void AddClearCacheStatusIfNotExists(ClearCacheStatus clearCacheStatus)
        {
            if (_context.ClearCacheStatuses.Any(l => l.TenantId == _tenantId && l.Value == clearCacheStatus.Value))
            {
                return;
            }

            _context.ClearCacheStatuses.Add(clearCacheStatus);

            _context.SaveChanges();
        }
    }
}
