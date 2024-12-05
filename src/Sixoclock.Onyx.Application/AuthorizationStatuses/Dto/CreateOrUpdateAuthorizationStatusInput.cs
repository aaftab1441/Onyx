using Abp.AutoMapper;

namespace Sixoclock.Onyx.AuthorizationStatuses.Dto
{
    [AutoMapTo(typeof(AuthorizationStatus))]
    public class CreateOrUpdateAuthorizationStatusInput
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
