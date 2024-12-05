using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class RemoteStartStopStatusCreator
    {
        public List<RemoteStartStopStatus> InitialRemoteStartStopStatusesStatuses => GetInitialRemoteStartStopStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;
        private List<RemoteStartStopStatus> GetInitialRemoteStartStopStatuses()
        {
            return new List<RemoteStartStopStatus>
            {
                new RemoteStartStopStatus() { Value = "Accepted", TenantId = _tenantId },
                new RemoteStartStopStatus() { Value = "Rejected", TenantId = _tenantId },
                new RemoteStartStopStatus() { Value = "Initiated", TenantId = _tenantId }
            };

        }
        public RemoteStartStopStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRemoteStartStopStatuses();
        }

        private void CreateRemoteStartStopStatuses()
        {
            foreach (var Status in InitialRemoteStartStopStatusesStatuses)
            {
                AddRemoteStartStopStatusIfNotExists(Status);
            }
        }

        private void AddRemoteStartStopStatusIfNotExists(RemoteStartStopStatus Status)
        {
            if (_context.RemoteStartStopStatuses.Any(l => l.TenantId == _tenantId && l.Value == Status.Value))
            {
                return;
            }

            _context.RemoteStartStopStatuses.Add(Status);

            _context.SaveChanges();
        }
    }
}
