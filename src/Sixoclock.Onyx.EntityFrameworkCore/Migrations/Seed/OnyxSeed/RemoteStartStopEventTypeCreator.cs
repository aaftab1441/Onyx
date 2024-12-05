using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class RemoteStartStopEventTypeCreator
    {
        public List<RemoteStartStopEventType> InitialRemoteStartStopEventTypes => GetInitialRemoteStartStopEventTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<RemoteStartStopEventType> GetInitialRemoteStartStopEventTypes()
        {
            return new List<RemoteStartStopEventType>
            {
                new RemoteStartStopEventType() { EventType = "Start", Comment = "Start Remote Transaction", TenantId = _tenantId },
                new RemoteStartStopEventType() { EventType = "Stop", Comment = "Stop Remote Transaction", TenantId = _tenantId }
            };
        }

        public RemoteStartStopEventTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRemoteStartStopEventTypes();
        }

        private void CreateRemoteStartStopEventTypes()
        {
            foreach (var remoteStartStopEventType in InitialRemoteStartStopEventTypes)
            {
                AddRemoteStartStopEventTypeIfNotExists(remoteStartStopEventType);
            }
        }

        private void AddRemoteStartStopEventTypeIfNotExists(RemoteStartStopEventType remoteStartStopEventType)
        {
            if (_context.RemoteStartStopEventTypes.Any(l => l.TenantId == _tenantId && l.EventType == remoteStartStopEventType.EventType))
            {
                return;
            }

            _context.RemoteStartStopEventTypes.Add(remoteStartStopEventType);

            _context.SaveChanges();
        }
    }
}
