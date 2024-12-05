using Abp.Domain.Repositories;
using Sixoclock.Onyx.OCPPVersions.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.OCPPVersions
{
    public class OCPPVersionAppService : OnyxAppServiceBase, IOCPPVersionAppService
    {
        private readonly IRepository<OCPPVersion> _oCPPVersionRepository;

        public OCPPVersionAppService(IRepository<OCPPVersion> oCPPVersionRepository)
        {
            _oCPPVersionRepository = oCPPVersionRepository;
        }
        public GetOCPPVersionsListOutput GetOCPPVersionsList()
        {
            IEnumerable<OCPPVersionListDto> _oCPPVersionsList = from oCPPVersion in _oCPPVersionRepository.GetAll()
                                                                select new OCPPVersionListDto
                                                                {
                                                                    Id = oCPPVersion.Id,
                                                                    Name = oCPPVersion.VersionName
                                                                };
            return new GetOCPPVersionsListOutput { OCPPVersions = _oCPPVersionsList.ToList() };
        }
    }
}
