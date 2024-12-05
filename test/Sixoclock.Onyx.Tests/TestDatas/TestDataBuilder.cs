using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
