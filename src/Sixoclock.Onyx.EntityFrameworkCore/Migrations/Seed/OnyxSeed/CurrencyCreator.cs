using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class CurrencyCreator
    {
        public List<Currency> InitialCurrency => GetInitialCurrencys();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Currency> GetInitialCurrencys()
        {
            return new List<Currency>
            {
                new Currency() { Value = "EUR",Comment="Euro", TenantId = _tenantId },
                new Currency() { Value = "GBP",Comment="British Pound", TenantId = _tenantId },
                new Currency() { Value = "SEK",Comment="Swedish Krona", TenantId = _tenantId },
                new Currency() { Value = "DKK",Comment="Danish Krone", TenantId = _tenantId },
                new Currency() { Value = "NOK",Comment="Norwegian Krone", TenantId = _tenantId },
                new Currency() { Value = "USD",Comment="US Dollar", TenantId = _tenantId }
            };
        }

        public CurrencyCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateCurrencies();
        }

        private void CreateCurrencies()
        {
            foreach (var currency in InitialCurrency)
            {
                AddCurrencyIfNotExists(currency);
            }
        }

        private void AddCurrencyIfNotExists(Currency currency)
        {
            if (_context.Currencies.Any(l => l.TenantId == _tenantId && l.Value == currency.Value))
            {
                return;
            }

            _context.Currencies.Add(currency);

            _context.SaveChanges();
        }
    }
}
