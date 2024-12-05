using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class CancelReservationStatusCreator
    {
        public List<CancelReservationStatus> InitialCancelReservationStatuses => GetInitialCancelReservationStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<CancelReservationStatus> GetInitialCancelReservationStatuses()
        {
            return new List<CancelReservationStatus>
            {
                new CancelReservationStatus() { Value = "Accepted", Comment = "Reservation for the identifier has been cancelled." , TenantId = _tenantId },
                new CancelReservationStatus() { Value = "Rejected", Comment = "Reservation could not be cancelled, because there is no reservation active for the identifier." , TenantId = _tenantId }
            };
        }

        public CancelReservationStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateCancelReservationStatuses();
        }

        private void CreateCancelReservationStatuses()
        {
            foreach (var cancelReservationStatus in InitialCancelReservationStatuses)
            {
                AddCancelReservationStatusIfNotExists(cancelReservationStatus);
            }
        }

        private void AddCancelReservationStatusIfNotExists(CancelReservationStatus cancelReservationStatus)
        {
            if (_context.CancelReservationStatuses.Any(l => l.TenantId == _tenantId && l.Value == cancelReservationStatus.Value))
            {
                return;
            }

            _context.CancelReservationStatuses.Add(cancelReservationStatus);

            _context.SaveChanges();
        }
    }
}
