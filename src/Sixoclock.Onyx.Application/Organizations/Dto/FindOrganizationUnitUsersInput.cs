using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
