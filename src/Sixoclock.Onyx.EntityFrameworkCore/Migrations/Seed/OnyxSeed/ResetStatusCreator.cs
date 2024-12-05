using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ResetStatusCreator
    {
        public List<ResetStatus> InitialResetStatuses => GetInitialResetStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ResetStatus> GetInitialResetStatuses()
        {
            return new List<ResetStatus>
            {
                new ResetStatus() { ResetStatusValue = "Accepted", TenantId = _tenantId },
                new ResetStatus() { ResetStatusValue = "Rejected", TenantId = _tenantId },
                new ResetStatus() { ResetStatusValue = "Initiated", TenantId = _tenantId }
            };
        }

        public ResetStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateResetStatuses();
        }

        private void CreateResetStatuses()
        {
            foreach (var resetStatus in InitialResetStatuses)
            {
                AddResetStatusIfNotExists(resetStatus);
            }
        }

        private void AddResetStatusIfNotExists(ResetStatus resetStatus)
        {
            if (_context.ResetStatuses.Any(l => l.TenantId == _tenantId && l.ResetStatusValue == resetStatus.ResetStatusValue))
            {
                return;
            }

            _context.ResetStatuses.Add(resetStatus);

            _context.SaveChanges();
        }
    }
}
