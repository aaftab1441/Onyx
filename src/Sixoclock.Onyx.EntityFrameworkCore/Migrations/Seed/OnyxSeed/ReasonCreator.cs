using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ReasonCreator
    {
        public List<Reason> InitialReasons => GetInitialReasons();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Reason> GetInitialReasons()
        {
            return new List<Reason>
            {
                new Reason() { ReasonName = "DeAuthorized", Comment="The transaction was stopped because of the authorization status in a StartTransaction.conf", TenantId = _tenantId },
                new Reason() { ReasonName = "EmergencyStop", Comment="Emergency stop button was used.", TenantId = _tenantId },
                new Reason() { ReasonName = "EnergyLimitReached", Comment="EV charging session reached a locally enforced maximum energy transfer limit", TenantId = _tenantId },
                new Reason() { ReasonName = "EVDisconnected", Comment="Disconnecting of cable, vehicle moved away from inductive charge unit.", TenantId = _tenantId },
                new Reason() { ReasonName = "GroundFault", Comment="A GroundFault har occurred", TenantId = _tenantId },
                new Reason() { ReasonName = "ImmediateReset", Comment="A Reset(Immediate) command was received.", TenantId = _tenantId },
                new Reason() { ReasonName = "LawEnforcement", Comment="The transaction was stopped using a token with a LawEnforcementGroupId.", TenantId = _tenantId },
                new Reason() { ReasonName = "Local", Comment="Stopped locally on request of the user at the Charge Point. This is a regular termination of a transaction. Examples: presenting an RFID tag, pressing a button to stop.", TenantId = _tenantId },
                new Reason() { ReasonName = "LocalOutOfCredit", Comment="A local credit limit enforced through the Charge Point has been exceeded.", TenantId = _tenantId },
                new Reason() { ReasonName = "Other", Comment="Any other reason.", TenantId = _tenantId },
                new Reason() { ReasonName = "OvercurrentFault", Comment="A larger than intended electric current has occurred", TenantId = _tenantId },
                new Reason() { ReasonName = "PowerLoss", Comment="Complete loss of power.", TenantId = _tenantId },
                new Reason() { ReasonName = "PowerQuality", Comment="Quality of power too low, e.g. voltage too low/high, phase imbalance, etc.", TenantId = _tenantId },
                new Reason() { ReasonName = "Reboot", Comment="A locally initiated reset/reboot occurred. (for instance watchdog kicked in)", TenantId = _tenantId },
                new Reason() { ReasonName = "Remote", Comment="Stopped remotely on request of the user. This is a regular termination of a transaction. Examples: termination using a smartphone app, exceeding a (non local) prepaid credit.", TenantId = _tenantId },
                new Reason() { ReasonName = "HardReset", Comment="A hard reset command was received.", TenantId = _tenantId },
                new Reason() { ReasonName = "SoftReset", Comment="A soft reset command was received.", TenantId = _tenantId },
                new Reason() { ReasonName = "UnlockCommand", Comment="Central System sent an Unlock Connector", TenantId = _tenantId },
                new Reason() { ReasonName = "SOCLimitReached", Comment="Electric vehicle has reported reaching a locally enforced maximum battery State of Charge (SOC)", TenantId = _tenantId },
                new Reason() { ReasonName = "StoppedByEV", Comment="The transaction was stopped by the EV", TenantId = _tenantId },
                new Reason() { ReasonName = "TimeLimitReached", Comment="EV charging session reached a locally enforced time limit", TenantId = _tenantId },
                new Reason() { ReasonName = "Timeout", Comment="EV not connected within timeout", TenantId = _tenantId },
            };
        }

        public ReasonCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateReasons();
        }

        private void CreateReasons()
        {
            foreach (var reason in InitialReasons)
            {
                AddReasonIfNotExists(reason);
            }
        }

        private void AddReasonIfNotExists(Reason reason)
        {
            if (_context.Reasons.Any(l => l.TenantId == _tenantId && l.ReasonName == reason.ReasonName))
            {
                return;
            }

            _context.Reasons.Add(reason);

            _context.SaveChanges();
        }
    }
}
