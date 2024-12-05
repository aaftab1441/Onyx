using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Sixoclock.Onyx.OCPPTransports.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.OCPPTransports
{
    public class OCPPTransportAppService : OnyxAppServiceBase, IOCPPTransportAppService
    {
        private readonly IRepository<OCPPTransport> _oCPPTransportRepository;
        public OCPPTransportAppService(IRepository<OCPPTransport> oCPPTransportRepository)
        {
            _oCPPTransportRepository = oCPPTransportRepository;
        }
        public async Task CreateOrUpdateOCPPTransport(CreateOrUpdateOCPPTransportInput input)
        {
            var oCPPTransport = ObjectMapper.Map<OCPPTransport>(input);

            if (input.Id == 0)
            {
                await _oCPPTransportRepository.InsertAsync(oCPPTransport);
            }
            else
            {
                await _oCPPTransportRepository.UpdateAsync(oCPPTransport);
            }
        }
        public async Task<GetOCPPTransportForEditOutput> GetOCPPTransportForEdit(EntityDto<int> input)
        {
            //Editing an existing occp transport
            var output = new GetOCPPTransportForEditOutput();
            if (input.Id == 0)
            {
                output.OCPPTransport = new OCPPTransportDto();
            }
            else
            {
                var oCPPTransport = await _oCPPTransportRepository.GetAsync(input.Id);

                output.OCPPTransport = ObjectMapper.Map<OCPPTransportDto>(oCPPTransport);
            }

            return output;
        }
        public GetOCPPTransportsListOutput GetOCPPTransportsList()
        {
            IEnumerable<OCPPTransportListDto> _oCPPTransportsList = from oCPPTransport in _oCPPTransportRepository.GetAll()
                                                      select new OCPPTransportListDto
                                                      {
                                                          Id = oCPPTransport.Id,
                                                          Name = oCPPTransport.OCPPTransportName
                                                      };
            return new GetOCPPTransportsListOutput { OCPPTransports = _oCPPTransportsList.ToList() };
        }
        public ListResultDto<OCPPTransportDto> GetOCPPTransport(GetOCPPTransportInput input)
        {
            var oCPPTransports = from oCPPTransport in _oCPPTransportRepository.GetAll()
            .WhereIf(!input.Filter.IsNullOrEmpty(),
                p => p.OCPPTransportName.ToLower().Contains(input.Filter.ToLower()) ||
                        p.Comment.ToLower().Contains(input.Filter.ToLower())
            )
            .WhereIf(!input.OCPPTransportName.IsNullOrEmpty(),
            p => p.OCPPTransportName.ToLower().Contains(input.OCPPTransportName.ToLower()))
            .WhereIf(!input.Comment.IsNullOrEmpty(),
            p => p.Comment.ToLower().Contains(input.Comment.ToLower()))
            .OrderBy(p => p.OCPPTransportName)
            .ThenBy(p => p.Comment)
                          select new OCPPTransportDto
                          {
                              Id = oCPPTransport.Id,
                              OCPPTransportName = oCPPTransport.OCPPTransportName,
                              Comment = oCPPTransport.Comment
                          };

            return new ListResultDto<OCPPTransportDto>(ObjectMapper.Map<List<OCPPTransportDto>>(oCPPTransports));
        }
        public async Task DeleteOCPPTransport(EntityDto<int> input)
        {
            var oCPPTransport = await _oCPPTransportRepository.GetAsync(input.Id);
            await _oCPPTransportRepository.DeleteAsync(oCPPTransport);
        }
    }
}
