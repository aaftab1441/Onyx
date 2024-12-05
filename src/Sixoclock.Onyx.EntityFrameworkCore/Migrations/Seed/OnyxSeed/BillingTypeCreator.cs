using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class BillingTypeCreator
    {
        public List<BillingType> InitialBillingType => GetInitialBillingTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<BillingType> GetInitialBillingTypes()
        {
            return new List<BillingType>
            {
                new BillingType() { Type = "Spine",Comment="Billing made by Spine", TenantId = _tenantId },
                new BillingType() { Type = "Tenant",Comment="Billing made by Tenant", TenantId = _tenantId }
            };
        }

        public BillingTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateBillingTypees();
        }

        private void CreateBillingTypees()
        {
            foreach (var billingType in InitialBillingType)
            {
                AddBillingTypeIfNotExists(billingType);
            }
        }

        private void AddBillingTypeIfNotExists(BillingType billingType)
        {
            if (_context.BillingTypes.Any(l => l.TenantId == _tenantId && l.Type == billingType.Type))
            {
                return;
            }

            _context.BillingTypes.Add(billingType);

            _context.SaveChanges();
        }
    }
}
