using Abp.Domain.Repositories;
using Sixoclock.Onyx.Countries.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Countries
{
    public class CountryAppService : OnyxAppServiceBase, ICountryAppService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryAppService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public GetCountriesListOutput GetCountriesList()
        {
            IEnumerable<CountryListDto> _countrysList = from country in _countryRepository.GetAll()
                                                        select new CountryListDto
                                                        {
                                                            Id = country.Id,
                                                            Name = country.Value
                                                        };
            return new GetCountriesListOutput { Countries = _countrysList.ToList() };
        }
    }
}
