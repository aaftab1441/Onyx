using Abp.AutoMapper;

namespace Sixoclock.Onyx.ModelEVSEs.Dto
{
    [AutoMapTo(typeof(ModelEVSE))]
    public class CreateOrUpdateModelEVSEInput
    {
        public int Id { get; set;}
        public int MeterTypeId { get; set; }
        public int ChargepointModelId { get; set; }
        public int EVSEId { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
