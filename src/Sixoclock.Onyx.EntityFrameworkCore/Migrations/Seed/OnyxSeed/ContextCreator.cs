using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ContextCreator
    {
        public List<Context> InitialContexts => GetInitialContexts();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Context> GetInitialContexts()
        {
            return new List<Context>
            {
                new Context() { ContextName = "Interruption.Begin", Comment = "Value taken at start of interruption.", TenantId = _tenantId },
                new Context() { ContextName = "Interruption.End", Comment = "Value taken when resuming after interruption.", TenantId = _tenantId },
                new Context() { ContextName = "Other", Comment = "Value for any other situations.", TenantId = _tenantId },
                new Context() { ContextName = "Sample.Clock", Comment = "Value taken at clock aligned interval.", TenantId = _tenantId },
                new Context() { ContextName = "Sample.Periodic", Comment = "Value taken as periodic sample relative to start time of transaction.", TenantId = _tenantId },
                new Context() { ContextName = "Transaction.Begin", Comment = "Value taken at end of transaction.", TenantId = _tenantId },
                new Context() { ContextName = "Transaction.End", Comment = "Value taken at start of transaction.", TenantId = _tenantId },
                new Context() { ContextName = "Trigger", Comment = "Value taken in response to a TriggerMessage.req", TenantId = _tenantId }
            };
        }

        public ContextCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateContexts();
        }

        private void CreateContexts()
        {
            foreach (var context in InitialContexts)
            {
                AddContextIfNotExists(context);
            }
        }

        private void AddContextIfNotExists(Context context)
        {
            if (_context.Contexts.Any(l => l.TenantId == _tenantId && l.ContextName == context.ContextName))
            {
                return;
            }

            _context.Contexts.Add(context);

            _context.SaveChanges();
        }
    }
}
