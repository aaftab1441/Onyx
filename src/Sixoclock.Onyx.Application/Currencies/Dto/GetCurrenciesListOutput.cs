using System.Collections.Generic;

namespace Sixoclock.Onyx.Currencies.Dto
{
    public class GetCurrenciesListOutput
    {
        public IEnumerable<CurrencyListDto> Currencies { get; set; }
    }
}
