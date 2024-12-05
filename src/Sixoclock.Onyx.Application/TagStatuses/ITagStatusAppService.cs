using Abp.Application.Services;
using Sixoclock.Onyx.TagStatuses.Dto;

namespace Sixoclock.Onyx.TagStatuses
{
    public interface ITagStatusAppService : IApplicationService
    {
        GetTagStatusByNameOutput GetTagStatusByName(GetTagStatusByNameInput input);
    }
}