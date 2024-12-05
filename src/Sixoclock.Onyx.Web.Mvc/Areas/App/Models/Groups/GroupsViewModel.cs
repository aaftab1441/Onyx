using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Groups.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Groups
{
    [AutoMapFrom(typeof(ListResultDto<GroupDto>))]
    public class GroupsViewModel : ListResultDto<GroupDto>
    {
        public GroupsViewModel(ListResultDto<GroupDto> output)
        {
            output.MapTo(this);
        }
    }
}
