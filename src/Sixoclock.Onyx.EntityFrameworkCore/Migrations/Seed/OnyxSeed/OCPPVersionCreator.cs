using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class OCPPVersionCreator
    {
        public List<OCPPVersion> InitialOCPPVersions => GetInitialOCPPVersions();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<OCPPVersion> GetInitialOCPPVersions()
        {
            return new List<OCPPVersion>
            {
                 new OCPPVersion() { VersionName = "None", Comment = "Used for Chargepoints in Topology that are not connected to Spine" , TenantId = _tenantId },
                 new OCPPVersion() { VersionName = "Custom", Comment = "Used for optional features not related to any OCPP Version" , TenantId = _tenantId },
                 new OCPPVersion() { VersionName = "OCPP14", Comment = "OCPP version 1.4" , TenantId = _tenantId },
                 new OCPPVersion() { VersionName = "OCPP15", Comment = "OCPP version 1.5" , TenantId = _tenantId },
                 new OCPPVersion() { VersionName = "OCPP16", Comment = "OCPP version 1.6" , TenantId = _tenantId },
                 new OCPPVersion() { VersionName = "OCPP20", Comment = "OCPP version 2.0" , TenantId = _tenantId }

            };
        }

        public OCPPVersionCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOCPPVersions();
        }

        private void CreateOCPPVersions()
        {
            foreach (var oCPPVersion in InitialOCPPVersions)
            {
                AddOCPPVersionIfNotExists(oCPPVersion);
            }
        }

        private void AddOCPPVersionIfNotExists(OCPPVersion oCPPVersion)
        {
            if (_context.OCPPVersions.Any(l => l.TenantId == _tenantId && l.VersionName == oCPPVersion.VersionName))
            {
                return;
            }

            _context.OCPPVersions.Add(oCPPVersion);

            _context.SaveChanges();
        }
    }
}
