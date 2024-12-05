using Abp.AutoMapper;

namespace Sixoclock.Onyx.ComOptions.Dto
{
    [AutoMapTo(typeof(ComOption))]
    public class CreateOrUpdateComOptionInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }
    }
}
