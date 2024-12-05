using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}