using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class OCPPTransportCreator
    {
        public List<OCPPTransport> InitialOCPPTransports => GetInitialOCPPTransports();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<OCPPTransport> GetInitialOCPPTransports()
        {
            return new List<OCPPTransport>
            {
                new OCPPTransport() { OCPPTransportName = "SOAP", Comment="Use SOAP for transport of OCPP PDU’s", TenantId = _tenantId },							
                new OCPPTransport() { OCPPTransportName = "JSON", Comment="Use JSON over WebSockets for transport of OCPP PDU’s", TenantId = _tenantId },
            };
        }

        public OCPPTransportCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOCPPTransports();
        }

        private void CreateOCPPTransports()
        {
            foreach (var oCPPTransport in InitialOCPPTransports)
            {
                AddOCPPTransportIfNotExists(oCPPTransport);
            }
        }

        private void AddOCPPTransportIfNotExists(OCPPTransport oCPPTransport)
        {
            if (_context.OCPPTransports.Any(l => l.TenantId == _tenantId && l.OCPPTransportName == oCPPTransport.OCPPTransportName))
            {
                return;
            }

            _context.OCPPTransports.Add(oCPPTransport);

            _context.SaveChanges();
        }
    }
}
