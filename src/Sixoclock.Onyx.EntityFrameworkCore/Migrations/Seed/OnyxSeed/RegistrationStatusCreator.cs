using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class RegistrationStatusCreator
    {
        public List<RegistrationStatus> InitialRegistrationStatuses => GetInitialRegistrationStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<RegistrationStatus> GetInitialRegistrationStatuses()
        {
            return new List<RegistrationStatus>
            {
                new RegistrationStatus() { Value = "Accepted", Comment="Charge point is accepted by Central System.", TenantId = _tenantId },
                new RegistrationStatus() { Value = "Pending", Comment="Central System is not yet ready to accept the Charge Point. Central System may send messages to retrieve information or prepare the Charge Point.", TenantId = _tenantId },
                new RegistrationStatus() { Value = "Rejected", Comment="Charge point is not accepted by Central System. This may happen when the Charge Point id is not known by Central System.", TenantId = _tenantId },
            };
        }

        public RegistrationStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRegistrationStatuses();
        }

        private void CreateRegistrationStatuses()
        {
            foreach (var registrationStatus in InitialRegistrationStatuses)
            {
                AddRegistrationStatusIfNotExists(registrationStatus);
            }
        }

        private void AddRegistrationStatusIfNotExists(RegistrationStatus registrationStatus)
        {
            if (_context.RegistrationStatuses.Any(l => l.TenantId == _tenantId && l.Value == registrationStatus.Value))
            {
                return;
            }

            _context.RegistrationStatuses.Add(registrationStatus);

            _context.SaveChanges();
        }
    }
}
