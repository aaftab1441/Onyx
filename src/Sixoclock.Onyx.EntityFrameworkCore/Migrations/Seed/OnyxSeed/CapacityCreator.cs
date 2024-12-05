using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class CapacityCreator
    {
        public List<Capacity> InitialCapacities => GetInitialCapacities();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Capacity> GetInitialCapacities()
        {
            List<Capacity> Capacities = new List<Capacity>
            {
                new Capacity() { Value = "3.7", PowerId=1, UnitId=6, Comment="TBD", TenantId = _tenantId},
                new Capacity() { Value = "11", PowerId=2, UnitId=6, Comment="TBD", TenantId = _tenantId},
                new Capacity() { Value = "22", PowerId=2, UnitId=6, Comment="TBD", TenantId = _tenantId},
                new Capacity() { Value = "44", PowerId=2, UnitId=6, Comment="TBD", TenantId = _tenantId},
                new Capacity() { Value = "50", PowerId=3, UnitId=6, Comment="TBD", TenantId = _tenantId}
            };
            foreach (var capacity in Capacities)
            {
                if (capacity.PowerId == 1)
                    capacity.PowerId = _context.Powers.Where(l => l.TenantId == _tenantId && l.PowerName == "1-Phase").SingleOrDefault().Id;
                else if (capacity.PowerId == 2)
                    capacity.PowerId = _context.Powers.Where(l => l.TenantId == _tenantId && l.PowerName == "3-Phase").SingleOrDefault().Id;
                else if (capacity.PowerId == 3)
                    capacity.PowerId = _context.Powers.Where(l => l.TenantId == _tenantId && l.PowerName == "DC").SingleOrDefault().Id;
            }
            foreach (var capacity in Capacities)
            {
                if (capacity.UnitId == 6)
                    capacity.UnitId = _context.Units.Where(l => l.TenantId == _tenantId && l.UnitName == "kW").SingleOrDefault().Id;
            }
            return Capacities;
        }

        public CapacityCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateCapacityes();
        }

        private void CreateCapacityes()
        {
            foreach (var capacity in InitialCapacities)
            {
                AddCapacityIfNotExists(capacity);
            }
        }

        private void AddCapacityIfNotExists(Capacity capacity)
        {
            if (_context.Capacities.Any(l => l.TenantId == _tenantId && l.UnitId == capacity.UnitId && l.PowerId == capacity.PowerId && l.Value == capacity.Value))
            {
                return;
            }

            _context.Capacities.Add(capacity);

            _context.SaveChanges();
        }
    }
}
