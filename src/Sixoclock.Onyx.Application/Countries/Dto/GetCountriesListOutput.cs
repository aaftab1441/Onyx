using System.Collections.Generic;

namespace Sixoclock.Onyx.Countries.Dto
{
    public class GetCountriesListOutput
    {
        public IEnumerable<CountryListDto> Countries { get; set; }
    }
}
