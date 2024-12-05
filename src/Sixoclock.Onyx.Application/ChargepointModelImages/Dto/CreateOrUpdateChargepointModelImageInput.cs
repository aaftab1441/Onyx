using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Sixoclock.Onyx.ChargepointModelImages.Dto
{
    [AutoMapTo(typeof(ChargepointModelImage))]
    public class CreateOrUpdateChargepointModelImageInput
    {
        [Required]
        [MaxLength(400)]
        public int TenantId { get; set; }
        public string OriginalFileName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Ext { get; set; }
        public bool IsActive { get; set; }
        public int ChargepointModelId { get; set; }
    }
}
