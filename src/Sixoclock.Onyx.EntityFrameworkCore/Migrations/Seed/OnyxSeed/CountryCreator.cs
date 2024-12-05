using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class CountryCreator
    {
        public List<Country> InitialCountry => GetInitialCountries();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Country> GetInitialCountries()
        {
            return new List<Country>
            {
                new Country() { Value = "Germany", Comment="Germany", TenantId = _tenantId },
                new Country() { Value = "France", Comment="France", TenantId = _tenantId },
                new Country() { Value = "Switzerland", Comment="Switzerland", TenantId = _tenantId },
                new Country() { Value = "Austria", Comment="Austria", TenantId = _tenantId },
                new Country() { Value = "Sweden", Comment="Sweden", TenantId = _tenantId },
                new Country() { Value = "Denmark", Comment="Denmark", TenantId = _tenantId },
                new Country() { Value = "Norway", Comment="Norway", TenantId = _tenantId }
            };
        }

        public CountryCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateCountries();
        }

        private void CreateCountries()
        {
            foreach (var country in InitialCountry)
            {
                AddCountryIfNotExists(country);
            }
        }

        private void AddCountryIfNotExists(Country country)
        {
            if (_context.Countries.Any(l => l.TenantId == _tenantId && l.Value == country.Value))
            {
                return;
            }

            _context.Countries.Add(country);

            _context.SaveChanges();
        }
    }
}
