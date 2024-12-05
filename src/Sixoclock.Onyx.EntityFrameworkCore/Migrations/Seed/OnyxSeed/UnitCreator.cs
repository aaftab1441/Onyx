using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class UnitCreator
    {
        public List<Unit> InitialUnits => GetInitialUnits();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Unit> GetInitialUnits()
        {
            return new List<Unit>
            {
                new Unit() { UnitName = "Wh", Comment="Watt-hours (energy). Default.", TenantId = _tenantId },
                new Unit() { UnitName = "kWh", Comment="kiloWatt-hours (energy).", TenantId = _tenantId },
                new Unit() { UnitName = "varh", Comment="Var-hours (reactive energy).", TenantId = _tenantId },
                new Unit() { UnitName = "kvarh", Comment="kilovar-hours (reactive energy).", TenantId = _tenantId },
                new Unit() { UnitName = "W", Comment="Watts (power).", TenantId = _tenantId },
                new Unit() { UnitName = "kW", Comment="kilowatts (power).", TenantId = _tenantId },
                new Unit() { UnitName = "VA", Comment="VoltAmpere (apparent power).", TenantId = _tenantId },
                new Unit() { UnitName = "kVA", Comment="kiloVolt Ampere (apparent power).", TenantId = _tenantId },
                new Unit() { UnitName = "var", Comment="Vars (reactive power).", TenantId = _tenantId },
                new Unit() { UnitName = "kvar", Comment="kilovars (reactive power).", TenantId = _tenantId },
                new Unit() { UnitName = "A", Comment="Amperes (current).", TenantId = _tenantId },
                new Unit() { UnitName = "V", Comment="Voltage (r.m.s. AC).", TenantId = _tenantId },
                new Unit() { UnitName = "Celcius", Comment="Degrees (temperature).", TenantId = _tenantId },
                new Unit() { UnitName = "Fahrenheit", Comment="Degrees (temperature).", TenantId = _tenantId },
                new Unit() { UnitName = "K", Comment="Degrees Kelvin (temperature).", TenantId = _tenantId },
                new Unit() { UnitName = "ASU", Comment="Arbitrary Strength Unit (Signal Strength)", TenantId = _tenantId },
                new Unit() { UnitName = "dB", Comment="Decibel (for example Signal Strength)", TenantId = _tenantId },
                new Unit() { UnitName = "dBm", Comment="Power relative to 1mW (10log(P/1mW)).", TenantId = _tenantId },
                new Unit() { UnitName = "Deg", Comment="Degrees (angle/rotation)", TenantId = _tenantId },
                new Unit() { UnitName = "Hz", Comment="Hertz (frequency)", TenantId = _tenantId },
                new Unit() { UnitName = "kPa", Comment="kiloPascal (Pressure)", TenantId = _tenantId },
                new Unit() { UnitName = "lx", Comment="Lux (Light Intensity)", TenantId = _tenantId },
                new Unit() { UnitName = "ms2", Comment="ms2 (Acceleration)", TenantId = _tenantId },
                new Unit() { UnitName = "N", Comment="Newtons (Force)", TenantId = _tenantId },
                new Unit() { UnitName = "Percent", Comment="Percentage", TenantId = _tenantId },
                new Unit() { UnitName = "RH", Comment="Relative Humidity%", TenantId = _tenantId },
                new Unit() { UnitName = "RPM", Comment="Revolutions per Minute", TenantId = _tenantId },
                new Unit() { UnitName = "s", Comment="Seconds (Time)", TenantId = _tenantId },
            };
        }

        public UnitCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateUnits();
        }

        private void CreateUnits()
        {
            foreach (var unit in InitialUnits)
            {
                AddUnitIfNotExists(unit);
            }
        }

        private void AddUnitIfNotExists(Unit unit)
        {
            if (_context.Units.Any(l => l.TenantId == _tenantId && l.UnitName == unit.UnitName))
            {
                return;
            }

            _context.Units.Add(unit);

            _context.SaveChanges();
        }
    }
}
