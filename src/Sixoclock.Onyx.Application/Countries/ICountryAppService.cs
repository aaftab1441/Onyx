using Abp.Application.Services;
using Sixoclock.Onyx.Countries.Dto;

namespace Sixoclock.Onyx.Countries
{
    public interface ICountryAppService : IApplicationService
    {
        GetCountriesListOutput GetCountriesList();
    }
}