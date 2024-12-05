using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class TagTransationTypeCreator
    {
        public List<TagTransactionType> InitialTagTransationTypes => GetInitialTagTransationTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<TagTransactionType> GetInitialTagTransationTypes()
        {
            return new List<TagTransactionType>
            {
                new TagTransactionType() { Value = "Start", Comment="Indicates that a new Transaction has started", TenantId = _tenantId },
                new TagTransactionType() { Value = "Stop", Comment="Indicates that a Transaction has stopped", TenantId = _tenantId },
            };
        }

        public TagTransationTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateTagTransationTypes();
        }

        private void CreateTagTransationTypes()
        {
            foreach (var tagTransationType in InitialTagTransationTypes)
            {
                AddTagTransationTypeIfNotExists(tagTransationType);
            }
        }

        private void AddTagTransationTypeIfNotExists(TagTransactionType tagTransationType)
        {
            if (_context.TagTransactionType.Any(l => l.TenantId == _tenantId && l.Value == tagTransationType.Value))
            {
                return;
            }

            _context.TagTransactionType.Add(tagTransationType);

            _context.SaveChanges();
        }
    }
}
