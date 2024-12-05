using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Sixoclock.Onyx.OCPPFeatures.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.OCPPFeatures
{
    public class OCPPFeatureAppService : OnyxAppServiceBase, IOCPPFeatureAppService
    {
        private readonly IRepository<OCPPFeature> _oCPPFeatureRepository;
        public OCPPFeatureAppService(IRepository<OCPPFeature> oCPPFeatureRepository)
        {
            _oCPPFeatureRepository = oCPPFeatureRepository;
        }
        public GetOCPPFeaturesListOutput GetOCPPFeaturesList()
        {
            IEnumerable<OCPPFeatureListDto> _oCPPFeaturesList = from oCPPFeature in _oCPPFeatureRepository.GetAll()
                                                      select new OCPPFeatureListDto
                                                      {
                                                          Id = oCPPFeature.Id,
                                                          Name = oCPPFeature.FeatureName
                                                      };
            return new GetOCPPFeaturesListOutput { OCPPFeatures = _oCPPFeaturesList.ToList() };
        }
        public GetOCPPFeaturesListOutput GetOCPPFeaturesByOCPPVersionList(EntityDto<int> input)
        {
            IEnumerable<OCPPFeatureListDto> _oCPPFeaturesList = from oCPPFeature in _oCPPFeatureRepository.GetAll().Where(f=>f.OCPPVersionId == input.Id)
                                                                select new OCPPFeatureListDto
                                                                {
                                                                    Id = oCPPFeature.Id,
                                                                    Name = oCPPFeature.FeatureName
                                                                };
            return new GetOCPPFeaturesListOutput { OCPPFeatures = _oCPPFeaturesList.ToList() };
        }
    }
}
