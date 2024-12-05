using System.Collections.Generic;

namespace Sixoclock.Onyx.Markets.Dto
{
    public class GetMarketsListOutput
    {
        public IEnumerable<MarketListDto> Markets { get; set; }
    }
}
