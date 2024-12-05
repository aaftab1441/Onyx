using Abp.Domain.Repositories;
using Sixoclock.Onyx.Currencies.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Currencies
{
    public class CurrencyAppService : OnyxAppServiceBase, ICurrencyAppService
    {
        private readonly IRepository<Currency> _currencyRepository;
        public CurrencyAppService(IRepository<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public GetCurrenciesListOutput GetCurrenciesList()
        {
            IEnumerable<CurrencyListDto> _currenciesList = from currency in _currencyRepository.GetAll()
                                                                          select new CurrencyListDto
                                                                          {
                                                                              Id = currency.Id,
                                                                              Name = currency.Value
                                                                          };
            return new GetCurrenciesListOutput { Currencies = _currenciesList.ToList() };
        }
    }
}
