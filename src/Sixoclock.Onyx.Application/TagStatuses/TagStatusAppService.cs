using Abp.Domain.Repositories;
using Sixoclock.Onyx.TagStatuses.Dto;
using System.Linq;

namespace Sixoclock.Onyx.TagStatuses
{
    public class TagStatusAppService : OnyxAppServiceBase, ITagStatusAppService
    {
        private readonly IRepository<TagStatus> _tagStatusRepository;

        public TagStatusAppService(IRepository<TagStatus> tagStatusRepository)
        {
            _tagStatusRepository = tagStatusRepository;
        }
        public GetTagStatusByNameOutput GetTagStatusByName(GetTagStatusByNameInput input)
        {
            return (from tagStatus in _tagStatusRepository.GetAll()
                                  .Where(t => t.Status.Contains(input.StatusName) && t.TenantId == input.TenantId)
                    select new GetTagStatusByNameOutput
                    {
                        Id = tagStatus.Id,
                        Status = tagStatus.Status,
                    }).FirstOrDefault();
        }
    }
}
