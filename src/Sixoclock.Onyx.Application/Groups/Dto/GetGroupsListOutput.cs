using System.Collections.Generic;

namespace Sixoclock.Onyx.Groups.Dto
{
    public class GetGroupsListOutput
    {
        public IEnumerable<GroupListDto> Groups { get; set; }
    }
}
