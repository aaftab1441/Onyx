using Abp.AutoMapper;

namespace Sixoclock.Onyx.Groups.Dto
{
    [AutoMapTo(typeof(Group))]
    public class CreateOrUpdateGroupInput
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Comment { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public int InstallId { get; set; }
        public int? CountryId { get; set; }
        public int TenantId { get; set; }
    }
}
