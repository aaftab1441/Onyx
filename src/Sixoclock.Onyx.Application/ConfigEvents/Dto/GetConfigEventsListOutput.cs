using System.Collections.Generic;

namespace Sixoclock.Onyx.ConfigEvents.Dto
{
    public class GetConfigEventsListOutput
    {
        public IEnumerable<ConfigEventListDto> ConfigEvents { get; set; }
    }
}
