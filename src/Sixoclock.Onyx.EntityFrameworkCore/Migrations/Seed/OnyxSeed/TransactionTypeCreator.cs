using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class TransactionTypeCreator
    {
        public List<TransactionType> InitialTransactionTypes => GetInitialTransactionTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<TransactionType> GetInitialTransactionTypes()
        {
            return new List<TransactionType>
            {
                new TransactionType() { Type = "UserTag", Comment="Transaction started by user and a Tag", TenantId = _tenantId },
                new TransactionType() { Type = "RemoteTag", Comment="Transaction started by remote request from Onyx", TenantId = _tenantId },
                new TransactionType() { Type = "AlwaysOpen", Comment="Charge point is always open and doesn't require authentication. User is unknown.", TenantId = _tenantId }
            };
        }

        public TransactionTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateTransactionTypes();
        }

        private void CreateTransactionTypes()
        {
            foreach (var transactionType in InitialTransactionTypes)
            {
                AddTransactionTypeIfNotExists(transactionType);
            }
        }

        private void AddTransactionTypeIfNotExists(TransactionType transactionType)
        {
            if (_context.TransactionTypes.Any(l => l.TenantId == _tenantId && l.Type == transactionType.Type))
            {
                return;
            }

            _context.TransactionTypes.Add(transactionType);

            _context.SaveChanges();
        }
    }
}
