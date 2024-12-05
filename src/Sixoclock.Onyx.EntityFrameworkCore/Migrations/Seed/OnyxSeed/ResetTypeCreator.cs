using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ResetTypeCreator
    {
        public List<ResetType> InitialResetTypes => GetInitialResetTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ResetType> GetInitialResetTypes()
        {
            return new List<ResetType>
            {
                new ResetType() { Type = "Hard", Comment = "Hard reboot", TenantId = _tenantId },
                new ResetType() { Type = "Soft", Comment = "Soft reset", TenantId = _tenantId }
            };
        }

        public ResetTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateResetTypes();
        }

        private void CreateResetTypes()
        {
            foreach (var resetType in InitialResetTypes)
            {
                AddResetTypeIfNotExists(resetType);
            }
        }

        private void AddResetTypeIfNotExists(ResetType resetType)
        {
            if (_context.ResetTypes.Any(l => l.TenantId == _tenantId && l.Type == resetType.Type))
            {
                return;
            }

            _context.ResetTypes.Add(resetType);

            _context.SaveChanges();
        }
    }
}
