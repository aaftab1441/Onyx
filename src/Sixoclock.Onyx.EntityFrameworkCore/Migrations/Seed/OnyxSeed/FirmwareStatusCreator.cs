using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class FirmwareStatusCreator
    {
        public List<FirmwareStatus> InitialFirmwareStatuss => GetInitialFirmwareStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<FirmwareStatus> GetInitialFirmwareStatuses()
        {
            return new List<FirmwareStatus>
            {
                new FirmwareStatus() { Value = "Cancelled", Comment = "The firmware update has been cancelled as result of a CancelFirmwareUpdate request. (Final state)", TenantId = _tenantId },
                new FirmwareStatus() { Value = "Downloaded", Comment = "New firmware has been downloaded by Charge Point.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadingCertificate", Comment = "The Charge Point is downloading the Firmware Signing certificate", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadFailed", Comment = "Charge point failed to download firmware.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadFinished", Comment = "Downloading of new firmware has finished.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "Downloading", Comment = "Firmware is being downloaded.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadScheduled", Comment = "DownloadScheduled Downloading of new firmware has been scheduled.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadPaused", Comment = "DownloadPaused Downloading has been paused.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "DownloadResumed", Comment = "Download has resumed", TenantId = _tenantId },
                new FirmwareStatus() { Value = "Idle", Comment = "Charge Point is not performing firmware update related tasks. Status Idle SHALL only be used as in a FirmwareStatusNotification.req that was triggered by TriggerMessage.req", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InstallationFailed", Comment = "Installation of new firmware has failed.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "Installing", Comment = "Firmware is being installed.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "Installed", Comment = "New firmware has successfully been installed in charge point.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InstallRebooting", Comment = "Charge Point is about to reboot to activate new firmware. This status MAY be omitted if a reboot is an integral part of the installation and cannot be reported separately.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InstallScheduled", Comment = "Installation of the downloaded firmware is scheduled to take place on installDate given in UpdateFirmware request. This status MAY be omitted if installation takes place immediately.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InstallVerificationFailed", Comment = "Verification of the new Firmware (e.g. using a checksum or some other means) has failed and installation will not proceed. (Final failure state)", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InvalidSignature", Comment = "The firmware signature is not valid.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "InvalidCertificate", Comment = "The Firmware Signing certificate is invalid.", TenantId = _tenantId },
                new FirmwareStatus() { Value = "RevokedCertificate", Comment = "The Firmware Signing certificate has been revoked.", TenantId = _tenantId }
            };
        }

        public FirmwareStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateFirmwareStatuses();
        }

        private void CreateFirmwareStatuses()
        {
            foreach (var firmwareStatus in InitialFirmwareStatuss)
            {
                AddFirmwareStatusIfNotExists(firmwareStatus);
            }
        }

        private void AddFirmwareStatusIfNotExists(FirmwareStatus firmwareStatus)
        {
            if (_context.FirmwareStatuses.Any(l => l.TenantId == _tenantId && l.Value == firmwareStatus.Value))
            {
                return;
            }

            _context.FirmwareStatuses.Add(firmwareStatus);

            _context.SaveChanges();
        }
    }
}
