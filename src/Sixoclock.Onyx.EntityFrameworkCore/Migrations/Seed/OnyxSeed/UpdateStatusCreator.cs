using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class UpdateStatusCreator
    {
        public List<UpdateStatus> InitialUpdateStatuses => GetInitialUpdateStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<UpdateStatus> GetInitialUpdateStatuses()
        {
            return new List<UpdateStatus>
            {
                new UpdateStatus() { Value = "Accepted", Comment="Local Authorization List successfully updated.", TenantId = _tenantId },
                new UpdateStatus() { Value = "Failed", Comment="Failed to update the Local Authorization List.", TenantId = _tenantId },
                new UpdateStatus() { Value = "NotSupported", Comment="Update of Local Authorization List is not supported by Charge Point.", TenantId = _tenantId },
                new UpdateStatus() { Value = "VersionMismatch", Comment="Version number in the request for a differential update is less or equal then version number of current list.", TenantId = _tenantId },
            };
        }

        public UpdateStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateUpdateStatuses();
        }

        private void CreateUpdateStatuses()
        {
            foreach (var updateStatus in InitialUpdateStatuses)
            {
                AddUpdateStatusIfNotExists(updateStatus);
            }
        }

        private void AddUpdateStatusIfNotExists(UpdateStatus updateStatus)
        {
            if (_context.UpdateStatuses.Any(l => l.TenantId == _tenantId && l.Value == updateStatus.Value))
            {
                return;
            }

            _context.UpdateStatuses.Add(updateStatus);

            _context.SaveChanges();
        }
    }
}
