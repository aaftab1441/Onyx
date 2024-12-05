using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class TransactionStatusCreator
    {
        public List<TransactionStatus> InitialTransactionStatuses => GetInitialTransactionStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<TransactionStatus> GetInitialTransactionStatuses()
        {
            return new List<TransactionStatus>
            {
                new TransactionStatus() { Value = "Idle", Comment="Waiting for EV to charge", TenantId = _tenantId },
                new TransactionStatus() { Value = "Charging", Comment="Providing energy to EV", TenantId = _tenantId },
                new TransactionStatus() { Value = "Faulted", Comment="Error state", TenantId = _tenantId },
                new TransactionStatus() { Value = "Completed", Comment="Transaction Completed", TenantId = _tenantId },
            };
        }

        public TransactionStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateTransactionStatuses();
        }

        private void CreateTransactionStatuses()
        {
            foreach (var transactionStatus in InitialTransactionStatuses)
            {
                AddTransactionStatusIfNotExists(transactionStatus);
            }
        }

        private void AddTransactionStatusIfNotExists(TransactionStatus transactionStatus)
        {
            if (_context.TransactionStatuses.Any(l => l.TenantId == _tenantId && l.Value == transactionStatus.Value))
            {
                return;
            }

            _context.TransactionStatuses.Add(transactionStatus);

            _context.SaveChanges();
        }
    }
}
