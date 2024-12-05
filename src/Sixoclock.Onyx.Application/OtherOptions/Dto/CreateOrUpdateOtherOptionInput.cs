using Abp.AutoMapper;

namespace Sixoclock.Onyx.OtherOptions.Dto
{
    [AutoMapTo(typeof(OtherOption))]
    public class CreateOrUpdateOtherOptionInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
