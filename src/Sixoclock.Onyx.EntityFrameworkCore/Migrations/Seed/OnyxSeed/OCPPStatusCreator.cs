using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class OCPPStatusCreator
    {
        public List<OCPPStatus> InitialOCPPStatuses => GetInitialOCPPStatuses();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<OCPPStatus> GetInitialOCPPStatuses()
        {
            return new List<OCPPStatus>
            {
                new OCPPStatus() { Status = "Success", Comment = "OCPP call was a success" , TenantId = _tenantId },
                 new OCPPStatus() { Status = "Failed", Comment = "OCPP call failed for some reason" , TenantId = _tenantId },
                 new OCPPStatus(){ Status = "Pending", Comment = "Message was sent by Central System and waiting for response", TenantId = _tenantId}

            };
        }

        public OCPPStatusCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOCPPStatuses();
        }

        private void CreateOCPPStatuses()
        {
            foreach (var oCPPStatus in InitialOCPPStatuses)
            {
                AddOCPPStatusIfNotExists(oCPPStatus);
            }
        }

        private void AddOCPPStatusIfNotExists(OCPPStatus oCPPStatus)
        {
            if (_context.OCPPStatuses.Any(l => l.TenantId == _tenantId && l.Status == oCPPStatus.Status))
            {
                return;
            }

            _context.OCPPStatuses.Add(oCPPStatus);

            _context.SaveChanges();
        }
    }
}
