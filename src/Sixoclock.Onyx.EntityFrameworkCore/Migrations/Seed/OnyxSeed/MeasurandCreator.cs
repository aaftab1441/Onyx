using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class MeasurandCreator
    {
        public List<Measurand> InitialMeasurands => GetInitialMeasurands();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Measurand> GetInitialMeasurands()
        {
            return new List<Measurand>
            {
                new Measurand() { MeasurandType = "Current.Export", Comment="Instantaneous current flow from EV", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Current.Import", Comment="Instantaneous current flow to EV", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Current.Offered", Comment="Maximum current offered to EV", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Export.Register", Comment="Numerical value read from the \"active electrical energy\" (Wh or kWh) register of the (most authoritative) electrical meter measuring energy exported (to the grid).", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Import.Register", Comment="Numerical value read from the \"active electrical energy\" (Wh or kWh) register of the (most authoritative) electrical meter measuring energy imported (from the grid supply).", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Export.Register", Comment="Numerical value read from the \"reactive electrical energy\" (VARh or kVARh) register of the (most authoritative) electrical meter measuring energy exported (to the grid).", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Import.Register", Comment="Numerical value read from the \"reactive electrical energy\" (VARh or kVARh) register of the (most authoritative) electrical meter measuring energy imported (from the grid supply).", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Export.Interval", Comment="Absolute amount of \"active electrical energy\" (Wh or kWh) exported (to the grid) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Import.Interval", Comment="Absolute amount of \"active electrical energy\" (Wh or kWh) imported (from the grid supply) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Export.Interval", Comment="Absolute amount of \"reactive electrical energy\" (VARh or kVARh) exported (to the grid) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Import.Interval", Comment="Absolute amount of \"reactive electrical energy\" (VARh or kVARh) imported (from the grid supply) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Frequency", Comment="Instantaneous reading of powerline frequency", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Active.Export", Comment="Instantaneous active power exported by EV. (W or kW)", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Active.Import", Comment="Instantaneous active power imported by EV. (W or kW)", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Factor", Comment="Instantaneous power factor of total energy flow", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Offered", Comment="Maximum power offered to EV", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Reactive.Export", Comment="Instantaneous reactive power exported by EV. (var or kvar)", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Power.Reactive.Import", Comment="Instantaneous reactive power imported by EV. (var or kvar)", TenantId = _tenantId },
                new Measurand() { MeasurandType = "RPM", Comment="Fan speed in RPM", TenantId = _tenantId },
                new Measurand() { MeasurandType = "SoC", Comment="State of charge of charging vehicle in percentage", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Temperature", Comment="Temperature reading inside Charge Point.", TenantId = _tenantId },
                new Measurand() { MeasurandType = "Voltage", Comment="Instantaneous AC RMS supply voltage", TenantId = _tenantId },
            };
        }

        public MeasurandCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateMeasurands();
        }

        private void CreateMeasurands()
        {
            foreach (var measurand in InitialMeasurands)
            {
                AddMeasurandIfNotExists(measurand);
            }
        }

        private void AddMeasurandIfNotExists(Measurand measurand)
        {
            if (_context.Measurands.Any(l => l.TenantId == _tenantId && l.MeasurandType == measurand.MeasurandType))
            {
                return;
            }

            _context.Measurands.Add(measurand);

            _context.SaveChanges();
        }
    }
}
