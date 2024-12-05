using System.Collections.Generic;

namespace Sixoclock.Onyx.ResetStatuses.Dto
{
    public class GetResetStatusesListOutput
    {
        public IEnumerable<ResetStatusListDto> ResetStatuses { get; set; }
    }
}
