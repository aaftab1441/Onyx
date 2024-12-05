using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ChargepointModelImages.Dto
{
    public class GetChargepointModelImageInput : PagedAndSortedInputDto
    {
        public string Filter { get; set; }

        public string Comment { get; set; }

        public string ModelName { get; set; }
    }
}
