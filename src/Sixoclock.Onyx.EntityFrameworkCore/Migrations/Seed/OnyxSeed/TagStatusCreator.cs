using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class TagStatusCreator
    {
        public List<TagStatus> InitialTagStatus => GetInitialTagStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<TagStatus> GetInitialTagStatuses()
        {
            return new List<TagStatus>
            {
                new TagStatus() { Status = "Active", Comment="Tag is active", TenantId = _tenantId},
                new TagStatus() { Status = "Inactive", Comment="Tag has been inactivated", TenantId = _tenantId},
                new TagStatus() { Status = "Expired", Comment="Tag has expired", TenantId = _tenantId},
            };
        }

        public TagStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateTagStatuses();
        }

        private void CreateTagStatuses()
        {
            foreach (var tagStatus in InitialTagStatus)
            {
                AddTagStatusIfNotExists(tagStatus);
            }
        }

        private void AddTagStatusIfNotExists(TagStatus tagStatus)
        {
            if (_context.TagStatuses.Any(l => l.TenantId == _tenantId && l.Status == tagStatus.Status))
            {
                return;
            }

            _context.TagStatuses.Add(tagStatus);

            _context.SaveChanges();
        }
    }
}
