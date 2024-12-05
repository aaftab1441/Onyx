using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class AuthorizationStatusCreator
    {
        public List<AuthorizationStatus> InitialAuthorizationStatus => GetInitialAuthorizationStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<AuthorizationStatus> GetInitialAuthorizationStatuses()
        {
            return new List<AuthorizationStatus>
            {
                new AuthorizationStatus() { Value = "Accepted", Comment = "Identifier is allowed for charging." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "Blocked", Comment = "Identifier has been blocked. Not allowed for charging." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "Expired", Comment = "Identifier has expired. Not allowed for charging." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "Invalid", Comment = "Identifier is unknown. Not allowed for charging." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "NoCredit", Comment = "Identifier is valid, but EV Driver doesn’t have enough credit to start charging. Not allowed for charging." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "NotAllowedTypeEVSE", Comment = "Identifier is valid, but not allowed to charge it this type of EVSE." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "NotAtThisLocation", Comment = "Identifier is valid, but not allowed to charge it this location." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "NotAtThisTime", Comment = "Identifier is valid, but not allowed to charge it this location at this time." , TenantId = _tenantId },
                new AuthorizationStatus() { Value = "ConcurrentTX", Comment = "Identifier is already involved in another transaction and multiple transactions are not allowed." , TenantId = _tenantId }
            };
        }

        public AuthorizationStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateAvailabilityTypes();
        }

        private void CreateAvailabilityTypes()
        {
            foreach (var authorizationStatus in InitialAuthorizationStatus)
            {
                AddAuthorizationStatusIfNotExists(authorizationStatus);
            }
        }

        private void AddAuthorizationStatusIfNotExists(AuthorizationStatus authorizationStatus)
        {
            if (_context.AuthorizationStatuses.Any(l => l.TenantId == _tenantId && l.Value == authorizationStatus.Value))
            {
                return;
            }

            _context.AuthorizationStatuses.Add(authorizationStatus);

            _context.SaveChanges();
        }
    }
}
