using Abp.Domain.Repositories;
using Sixoclock.Onyx.RestTypes.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.RestTypes
{
    public class RestTypeAppService : OnyxAppServiceBase, IRestTypeAppService
    {
        private readonly IRepository<ResetType> _restTypeRepository;
        public RestTypeAppService(IRepository<ResetType> restTypeRepository)
        {
            _restTypeRepository = restTypeRepository;
        }
        public GetRestTypesListOutput GetRestTypesList()
        {
            IEnumerable<RestTypeListDto> _restTypesList = from restType in _restTypeRepository.GetAll()
                                                          select new RestTypeListDto
                                                          {
                                                              Id = restType.Id,
                                                              Name = restType.Type
                                                          };
            return new GetRestTypesListOutput { RestTypes = _restTypesList.ToList() };
        }
    }
}
