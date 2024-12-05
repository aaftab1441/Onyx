using System.Collections.Generic;

namespace Sixoclock.Onyx.AuthorizationStatuses.Dto
{
    public class GetAuthorizationStatussListOutput
    {
        public IEnumerable<AuthorizationStatusListDto> AuthorizationStatuses { get; set; }
    }
}
