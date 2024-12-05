using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class AvailabilityStatusCreator
    {
        public List<AvailabilityStatus> InitialAvailabilityStatus => GetInitialAvailabilityStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<AvailabilityStatus> GetInitialAvailabilityStatuses()
        {
            return new List<AvailabilityStatus>
            {
                new AvailabilityStatus() { Value = "Accepted", TenantId = _tenantId },
                new AvailabilityStatus() { Value = "Rejected", TenantId = _tenantId },
                new AvailabilityStatus() { Value = "Scheduled", TenantId = _tenantId },
                new AvailabilityStatus() { Value = "Initiated", TenantId = _tenantId }
            };
        }

        public AvailabilityStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateAvailabilityStatuses();
        }

        private void CreateAvailabilityStatuses()
        {
            foreach (var availabilityStatus in InitialAvailabilityStatus)
            {
                AddAvailabilityStatusIfNotExists(availabilityStatus);
            }
        }

        private void AddAvailabilityStatusIfNotExists(AvailabilityStatus availabilityStatus)
        {
            if (_context.AvailabilityStatuses.Any(l => l.TenantId == _tenantId && l.Value == availabilityStatus.Value))
            {
                return;
            }

            _context.AvailabilityStatuses.Add(availabilityStatus);

            _context.SaveChanges();
        }
    }
}
