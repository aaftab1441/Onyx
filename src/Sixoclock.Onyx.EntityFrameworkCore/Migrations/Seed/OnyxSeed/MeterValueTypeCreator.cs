using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class MeterValueTypeCreator
    {
        public List<MeterValueType> InitialMeterValueTypes => GetInitialMeterValueTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<MeterValueType> GetInitialMeterValueTypes()
        {
            return new List<MeterValueType>
            {
                new MeterValueType() { Type = "Start", Comment="Meter value when charge transaction is started", TenantId = _tenantId },
                new MeterValueType() { Type = "Stop", Comment="Meter value when charge transaction is stopped", TenantId = _tenantId },
                new MeterValueType() { Type = "Intermediate", Comment="Meter value while in transaction", TenantId = _tenantId }
            };
        }

        public MeterValueTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateMeterValueTypes();
        }

        private void CreateMeterValueTypes()
        {
            foreach (var meterValueType in InitialMeterValueTypes)
            {
                AddMeterValueTypeIfNotExists(meterValueType);
            }
        }

        private void AddMeterValueTypeIfNotExists(MeterValueType meterValueType)
        {
            if (_context.MeterValueTypes.Any(l => l.TenantId == _tenantId && l.Type == meterValueType.Type))
            {
                return;
            }

            _context.MeterValueTypes.Add(meterValueType);

            _context.SaveChanges();
        }
    }
}
