using Abp.AutoMapper;

namespace Sixoclock.Onyx.AdminStatuses.Dto
{
    [AutoMapTo(typeof(AdminStatus))]
    public class CreateOrUpdateAdminStatusInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
