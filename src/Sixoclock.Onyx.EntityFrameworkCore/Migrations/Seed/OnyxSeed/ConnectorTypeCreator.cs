using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class ConnectorTypeCreator
    {
        public List<ConnectorType> InitialConnectorTypes => GetInitialConnectorTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<ConnectorType> GetInitialConnectorTypes()
        {
            return new List<ConnectorType>
            {
                new ConnectorType() { ConnectorName = "cCCS1", Comment = "Combined Charging System 1 (captive cabled) a.k.a. Combo 1", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "cCCS2", Comment = "Combined Charging System 2 (captive cabled) a.k.a. Combo 2", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "cG105", Comment = "JARI G105-1993 (captive cabled) a.k.a. CHAdeMO", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "cTesla", Comment = "Tesla Connector (captive cabled)", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "cType1", Comment = "IEC62196-2 Type 1 connector (captive cabled) a.k.a. J1772", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "cType2", Comment = "IEC62196-2 Type 2 connector (captive cabled) a.k.a. Mennekes socket", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "s309-1P-16A", Comment = "16A 1 phase IEC60309 socket", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "s309-1P-32A", Comment = "32A 1 phase IEC60309 socket", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "s309-3P-16A", Comment = "16A 3 phase IEC60309 socket", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "s309-3P-32A", Comment = "32A 3 phase IEC60309 socket", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "sBS1361", Comment = "UK domestic socket a.k.a. 13Amp", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "sCEE-7-7", Comment = "CEE 7/7 16A socket. May represent 7/4 & 7/5 a.k.a Schuko", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "sType2", Comment = "IEC62196-2 Type 2 socket a.k.a. Mennekes connector", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "sType3", Comment = "IEC62196-2 Type 2 socket a.k.a. Scame", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "Other1PhMax16A", Comment = "Other single phase (domestic) sockets not mentioned above, rated at no more than 16A. CEE7/17, AS3112, NEMA 5-15, NEMA 5-20, JISC8303, TIS166, SI 32, CPCS-CCC, SEV1011, etc.", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "Other1PhOver16A", Comment = "Other single phase sockets not mentioned above (over 16A)", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "Other3Ph", Comment = "Other 3 phase sockets not mentioned above. NEMA14-30, NEMA14-50.", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "wInductive", Comment = "Wireless inductively coupled connection (generic)", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "wResonant", Comment = "Wireless resonant coupled connection (generic)", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "Undetermined", Comment = "Yet to be determined (e.g. before plugged in)", TenantId = _tenantId },
                new ConnectorType() { ConnectorName = "Unknown", Comment = "Unknown; not determinable", TenantId = _tenantId },
            };
        }

        public ConnectorTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateConnectorTypees();
        }

        private void CreateConnectorTypees()
        {
            foreach (var connectorType in InitialConnectorTypes)
            {
                AddConnectorTypeIfNotExists(connectorType);
            }
        }

        private void AddConnectorTypeIfNotExists(ConnectorType connectorType)
        {
            if (_context.ConnectorTypes.Any(l => l.TenantId == _tenantId && l.ConnectorName == connectorType.ConnectorName))
            {
                return;
            }

            _context.ConnectorTypes.Add(connectorType);

            _context.SaveChanges();
        }
    }
}
