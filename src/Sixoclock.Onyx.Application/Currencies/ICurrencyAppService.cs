using Abp.Application.Services;
using Sixoclock.Onyx.Currencies.Dto;

namespace Sixoclock.Onyx.Currencies
{
    public interface ICurrencyAppService : IApplicationService
    {
        GetCurrenciesListOutput GetCurrenciesList();
    }
}