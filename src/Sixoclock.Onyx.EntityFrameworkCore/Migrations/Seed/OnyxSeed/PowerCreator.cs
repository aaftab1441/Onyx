using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class PowerCreator
    {
        public List<Power> InitialPowers => GetInitialPowers();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Power> GetInitialPowers()
        {
            return new List<Power>
            {
                new Power() { PowerName = "1-Phase", Comment="One phase", TenantId = _tenantId },
                new Power() { PowerName = "3-Phase", Comment="Three phase", TenantId = _tenantId },
                new Power() { PowerName = "DC", Comment="Direct current", TenantId = _tenantId }
            };
        }

        public PowerCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreatePoweres();
        }

        private void CreatePoweres()
        {
            foreach (var power in InitialPowers)
            {
                AddPowerIfNotExists(power);
            }
        }

        private void AddPowerIfNotExists(Power power)
        {
            if (_context.Powers.Any(l => l.TenantId == _tenantId && l.PowerName == power.PowerName))
            {
                return;
            }

            _context.Powers.Add(power);

            _context.SaveChanges();
        }
    }
}
