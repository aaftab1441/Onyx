using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class BillingStatusCreator
    {
        public List<BillingStatus> InitialBillingStatus => GetInitialBillingStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<BillingStatus> GetInitialBillingStatuses()
        {
            return new List<BillingStatus>
            {
                new BillingStatus() { Value = "To Pay",Comment="Bill to be paid", TenantId = _tenantId },
                new BillingStatus() { Value = "Paid",Comment="Bill is paid", TenantId = _tenantId },
                new BillingStatus() { Value = "Due",Comment="Bill is due for Payment", TenantId = _tenantId }
            };
        }

        public BillingStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateBillingStatuses();
        }

        private void CreateBillingStatuses()
        {
            foreach (var billingStatus in InitialBillingStatus)
            {
                AddBillingStatusIfNotExists(billingStatus);
            }
        }

        private void AddBillingStatusIfNotExists(BillingStatus billingStatus)
        {
            if (_context.BillingStatuses.Any(l => l.TenantId == _tenantId && l.Value == billingStatus.Value))
            {
                return;
            }

            _context.BillingStatuses.Add(billingStatus);

            _context.SaveChanges();
        }
    }
}
