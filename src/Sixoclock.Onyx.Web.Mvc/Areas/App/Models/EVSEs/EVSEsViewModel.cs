using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.EVSEs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.EVSEs
{
    [AutoMapFrom(typeof(ListResultDto<EVSEDto>))]
    public class EVSEsViewModel : ListResultDto<EVSEDto>
    {
        public EVSEsViewModel(ListResultDto<EVSEDto> output)
        {
            output.MapTo(this);
        }
    }
}
