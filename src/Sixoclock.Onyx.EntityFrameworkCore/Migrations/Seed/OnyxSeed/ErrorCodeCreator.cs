using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ErrorCodeCreator
    {
        public List<ErrorCode> InitialErrorCodes => GetInitialErrorCodes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ErrorCode> GetInitialErrorCodes()
        {
            return new List<ErrorCode>
            {
                new ErrorCode() { Value = "ConnectorLockFailure", Comment="Failure to lock or unlock connector.", TenantId = _tenantId },
                new ErrorCode() { Value = "EVCommunicationError", Comment="Communication failure with the vehicle, might be Mode 3 or other communication protocol problem. This is not a real error in the sense that the Charge Point doesn’t need to go to the faulted state. Instead, it should go to the SuspendedEVSE state.", TenantId = _tenantId },
                new ErrorCode() { Value = "GroundFailure", Comment="Ground fault circuit interrupter has been activated.", TenantId = _tenantId },
                new ErrorCode() { Value = "HighTemperature", Comment="Temperature inside Charge Point is too high.", TenantId = _tenantId },
                new ErrorCode() { Value = "InternalError", Comment="Error in internal hard- or software component.", TenantId = _tenantId },
                new ErrorCode() { Value = "LocalListConflict", Comment="The authorization information received from the Central System is in conflict with the LocalAuthorizationList.", TenantId = _tenantId },
                new ErrorCode() { Value = "NoError", Comment="No error to report.", TenantId = _tenantId },
                new ErrorCode() { Value = "OtherError", Comment="Other type of error. More information in vendorErrorCode.", TenantId = _tenantId },
                new ErrorCode() { Value = "OverCurrentFailure", Comment="Over current protection device has tripped.", TenantId = _tenantId },
                new ErrorCode() { Value = "OverVoltage", Comment="Voltage has risen above an acceptable level.", TenantId = _tenantId },
                new ErrorCode() { Value = "PowerMeterFailure", Comment="Failure to read power meter.", TenantId = _tenantId },
                new ErrorCode() { Value = "PowerSwitchFailure", Comment="Failure to control power switch.", TenantId = _tenantId },
                new ErrorCode() { Value = "ReaderFailure", Comment="Failure with idTag reader.", TenantId = _tenantId },
                new ErrorCode() { Value = "ResetFailure", Comment="Unable to perform a reset.", TenantId = _tenantId },
                new ErrorCode() { Value = "UnderVoltage", Comment="Voltage has dropped below an acceptable level.", TenantId = _tenantId },
                new ErrorCode() { Value = "WeakSignal", Comment="Wireless communication device reports a weak signal.", TenantId = _tenantId }
            };
        }

        public ErrorCodeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateErrorCodees();
        }

        private void CreateErrorCodees()
        {
            foreach (var errorCode in InitialErrorCodes)
            {
                AddErrorCodeIfNotExists(errorCode);
            }
        }

        private void AddErrorCodeIfNotExists(ErrorCode errorCode)
        {
            if (_context.ErrorCodes.Any(l => l.TenantId == _tenantId && l.Value == errorCode.Value))
            {
                return;
            }

            _context.ErrorCodes.Add(errorCode);

            _context.SaveChanges();
        }
    }
}
