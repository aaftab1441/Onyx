using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Customer
{
    [AutoMapFrom(typeof(ListResultDto<CustomerDto>))]
    public class IndexViewModel : ListResultDto<CustomerDto>
    {
        public IndexViewModel(ListResultDto<CustomerDto> output)
        {
            output.MapTo(this);
        }
    }
}
