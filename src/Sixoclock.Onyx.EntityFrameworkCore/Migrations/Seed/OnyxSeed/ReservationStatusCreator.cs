using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ReservationStatusCreator
    {
        public List<ReservationStatus> InitialReservationStatuses => GetInitialReservationStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ReservationStatus> GetInitialReservationStatuses()
        {
            return new List<ReservationStatus>
            {
                new ReservationStatus() { Value = "Accepted", Comment="Reservation has been made.", TenantId = _tenantId },
                new ReservationStatus() { Value = "Faulted", Comment="Reservation has not been made, because evse, connectors or specified connector are in a faulted state.", TenantId = _tenantId },
                new ReservationStatus() { Value = "Occupied", Comment="Reservation has not been made. The evse or the specified connector is occupied.", TenantId = _tenantId },
                new ReservationStatus() { Value = "Rejected", Comment="Reservation has not been made. Charge Point is not configured to accept reservations.", TenantId = _tenantId },
                new ReservationStatus() { Value = "Unavailable", Comment="Reservation has not been made, because connectors or specified connector are in an unavailable state.", TenantId = _tenantId },
            };
        }

        public ReservationStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateReservationStatuses();
        }

        private void CreateReservationStatuses()
        {
            foreach (var reservationStatus in InitialReservationStatuses)
            {
                AddReservationStatusIfNotExists(reservationStatus);
            }
        }

        private void AddReservationStatusIfNotExists(ReservationStatus reservationStatus)
        {
            if (_context.ReservationStatuses.Any(l => l.TenantId == _tenantId && l.Value == reservationStatus.Value))
            {
                return;
            }

            _context.ReservationStatuses.Add(reservationStatus);

            _context.SaveChanges();
        }
    }
}
