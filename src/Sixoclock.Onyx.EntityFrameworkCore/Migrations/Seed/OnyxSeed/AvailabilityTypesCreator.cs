using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class AvailabilityTypesCreator
    {
        public List<AvailabilityType> InitialAvailabilityType => GetInitialAvailabilityTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<AvailabilityType> GetInitialAvailabilityTypes()
        {
            return new List<AvailabilityType>
            {
                new AvailabilityType() { Value = "Inoperative", Comment = "Charge point is not available for charging.", TenantId = _tenantId },
                new AvailabilityType() { Value = "Operative", Comment = "Charge point is available for charging.", TenantId = _tenantId }
            };
        }

        public AvailabilityTypesCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateAvailabilityTypes();
        }

        private void CreateAvailabilityTypes()
        {
            foreach (var availabilityType in InitialAvailabilityType)
            {
                AddAvailabilityTypeIfNotExists(availabilityType);
            }
        }

        private void AddAvailabilityTypeIfNotExists(AvailabilityType availabilityType)
        {
            if (_context.AvailabilityTypes.Any(l => l.TenantId == _tenantId && l.Value == availabilityType.Value))
            {
                return;
            }

            _context.AvailabilityTypes.Add(availabilityType);

            _context.SaveChanges();
        }
    }
}
