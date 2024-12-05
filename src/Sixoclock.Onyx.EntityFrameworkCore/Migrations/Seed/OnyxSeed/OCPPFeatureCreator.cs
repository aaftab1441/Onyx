using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sixoclock.Onyx.MultiTenancy.OCPPTenantSeedData;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class OCPPFeatureCreator
    {
        public List<OCPPFeature> InitialOCPPFeatures => GetInitialOCPPFeatures();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<OCPPFeature> GetInitialOCPPFeatures()
        {
            List<OCPPFeature> Features = OCPPSeedData.GetInitialOCPPFeatures(_tenantId);

            for (int i = 0; i < 1; i++)
                Features[i].OCPPVersionId = _context.OCPPVersions.SingleOrDefault(l => l.TenantId == _tenantId && l.VersionName == "OCPP15").Id;
            for (int i = 1; i < 7; i++)
                Features[i].OCPPVersionId = _context.OCPPVersions.SingleOrDefault(l => l.TenantId == _tenantId && l.VersionName == "OCPP16").Id;
            for (int i = 7; i < 13; i++)
                Features[i].OCPPVersionId = _context.OCPPVersions.SingleOrDefault(l => l.TenantId == _tenantId && l.VersionName == "OCPP20").Id;
            
            return Features;
        }

        public OCPPFeatureCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOCPPFeatures();
        }

        private void CreateOCPPFeatures()
        {
            foreach (var oCPPFeature in InitialOCPPFeatures)
            {
                AddOCPPFeatureIfNotExists(oCPPFeature);
            }
        }

        private void AddOCPPFeatureIfNotExists(OCPPFeature oCPPFeature)
        {
            if (_context.OCPPFeatures.Any(l => l.TenantId == _tenantId && l.FeatureName == oCPPFeature.FeatureName && l.OCPPVersionId == oCPPFeature.OCPPVersionId))
            {
                return;
            }

            _context.OCPPFeatures.Add(oCPPFeature);

            _context.SaveChanges();
        }
    }
}
