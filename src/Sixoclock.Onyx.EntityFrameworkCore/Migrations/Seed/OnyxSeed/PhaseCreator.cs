using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class PhaseCreator
    {
        public List<Phase> InitialPhases => GetInitialPhases();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Phase> GetInitialPhases()
        {
            return new List<Phase>
            {
                new Phase() { PhaseName = "L1", Comment="Measured on L1", TenantId = _tenantId },
                new Phase() { PhaseName = "L2", Comment="Measured on L2", TenantId = _tenantId },
                new Phase() { PhaseName = "L3", Comment="Measured on L3", TenantId = _tenantId },
                new Phase() { PhaseName = "N", Comment="Measured on Neutral", TenantId = _tenantId },
                new Phase() { PhaseName = "L1-N", Comment="Measured on L1 with respect to Neutral conductor", TenantId = _tenantId },
                new Phase() { PhaseName = "L2-N", Comment="Measured on L2 with respect to Neutral conductor", TenantId = _tenantId },
                new Phase() { PhaseName = "L3-N", Comment="Measured on L3 with respect to Neutral conductor", TenantId = _tenantId },
                new Phase() { PhaseName = "L1-L2", Comment="Measured between L1 and L2", TenantId = _tenantId },
                new Phase() { PhaseName = "L2-L3", Comment="Measured between L2 and L3", TenantId = _tenantId },
                new Phase() { PhaseName = "L3-L1", Comment="Measured between L3 and L1", TenantId = _tenantId },
            };
        }

        public PhaseCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreatePhases();
        }

        private void CreatePhases()
        {
            foreach (var phase in InitialPhases)
            {
                AddPhaseIfNotExists(phase);
            }
        }

        private void AddPhaseIfNotExists(Phase phase)
        {
            if (_context.Phases.Any(l => l.TenantId == _tenantId && l.PhaseName == phase.PhaseName))
            {
                return;
            }

            _context.Phases.Add(phase);

            _context.SaveChanges();
        }
    }
}
