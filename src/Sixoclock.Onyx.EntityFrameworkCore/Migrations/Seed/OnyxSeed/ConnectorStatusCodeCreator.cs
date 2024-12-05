using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ConnectorStatusCodeCreator
    {
        public List<ConnectorStatusCode> InitialConnectorStatusStatusCodes => GetInitialConnectorStatusCodes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ConnectorStatusCode> GetInitialConnectorStatusCodes()
        {
            return new List<ConnectorStatusCode>
            {
                new ConnectorStatusCode() { Status = "Available", Comment="When a Connector becomes available for a new user (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Preparing", Comment="When a Connector becomes no longer available for a new user but no charging session is active. Typically a Connector is occupied when a user presents a tag, inserts a cable or a vehicle occupies the parking bay (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Charging", Comment="When the contactor of a Connector closes, allowing the vehicle to charge (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "SuspendedEVSE", Comment="When the contactor of a Connector opens upon request of the EVSE, e.g. due to a smart chargingn restriction or as the result of StartTransaction.conf indicating that charging is not allowed (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "SuspendedEV", Comment="When the EVSE is ready to deliver energy but contactor is open, e.g. the EV is not ready.", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Finishing", Comment="When a charging session has stopped at a Connector, but the Connector is not yet available for a new user, e.g. the cable has not been removed or the vehicle has not left the parking bay (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Reserved", Comment="When a Connector becomes reserved as a result of a Reserve Now command (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Occupied", Comment="When an EVSE becomes occupied, so it is not available for a new EV driver. (Operative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Unavailable", Comment="When an EVSE becomes unavailable as the result of a Change Availability command or an event upon which the Charge Point transitions to unavailable at its discretion. Upon receipt of ChangeAvailability.req message command, the status MAY change immediately or the change MAY be scheduled. When scheduled, StatusNotification.req SHALL be send when the availability change becomes effective (Inoperative)", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "Faulted", Comment="When a Charge Point or evse has reported an error and is not available for energy delivery. (Inoperative).", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "ExternalChargingLimitSet", Comment="Indicates that an external charging limit is set.", TenantId = _tenantId },
                new ConnectorStatusCode() { Status = "ExternalChargingLimitReleased", Comment="Indicates that an external charging limit is released / reset.", TenantId = _tenantId }
            };
        }

        public ConnectorStatusCodeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateConnectorStatusCodes();
        }

        private void CreateConnectorStatusCodes()
        {
            foreach (var connectorStatusCode in InitialConnectorStatusStatusCodes)
            {
                AddConnectorStatusCodeIfNotExists(connectorStatusCode);
            }
        }

        private void AddConnectorStatusCodeIfNotExists(ConnectorStatusCode connectorStatusCode)
        {
            if (_context.ConnectorStatusCodes.Any(l => l.TenantId == _tenantId && l.Status == connectorStatusCode.Status))
            {
                return;
            }

            _context.ConnectorStatusCodes.Add(connectorStatusCode);

            _context.SaveChanges();
        }
    }
}
