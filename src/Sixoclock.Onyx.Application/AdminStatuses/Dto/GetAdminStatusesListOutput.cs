using System.Collections.Generic;

namespace Sixoclock.Onyx.AdminStatuses.Dto
{
    public class GetAdminStatusesListOutput
    {
        public IEnumerable<AdminStatusListDto> AdminStatuses { get; set; }
    }
}
