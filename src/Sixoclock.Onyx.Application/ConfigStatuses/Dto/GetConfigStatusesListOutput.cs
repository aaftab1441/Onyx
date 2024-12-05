using System.Collections.Generic;

namespace Sixoclock.Onyx.ConfigStatuses.Dto
{
    public class GetConfigStatusesListOutput
    {
        public IEnumerable<ConfigStatusListDto> ConfigStatuses { get; set; }
    }
}
