using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class UnlockStatusCreator
    {
        public List<UnlockStatus> InitialUnlockStatuses => GetInitialUnlockStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<UnlockStatus> GetInitialUnlockStatuses()
        {
            return new List<UnlockStatus>
            {
                new UnlockStatus() { Value = "Unlocked", Comment="Connector has successfully been unlocked.", TenantId = _tenantId },
                new UnlockStatus() { Value = "UnlockFailed", Comment="Failed to unlock the connector.", TenantId = _tenantId },
                new UnlockStatus() { Value = "NotSupported", Comment="Charge Point has no connector lock.", TenantId = _tenantId },
                new UnlockStatus() { Value = "Initiated", Comment="Unlock Status Initiated from UI.", TenantId = _tenantId }
            };
        }

        public UnlockStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateUnlockStatuses();
        }

        private void CreateUnlockStatuses()
        {
            foreach (var unlockStatus in InitialUnlockStatuses)
            {
                AddUnlockStatusIfNotExists(unlockStatus);
            }
        }

        private void AddUnlockStatusIfNotExists(UnlockStatus unlockStatus)
        {
            if (_context.UnlockStatuses.Any(l => l.TenantId == _tenantId && l.Value == unlockStatus.Value))
            {
                return;
            }

            _context.UnlockStatuses.Add(unlockStatus);

            _context.SaveChanges();
        }
    }
}
