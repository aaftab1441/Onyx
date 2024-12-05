using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.VendorErrorCodes.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.VendorErrorCodes
{
    public class VendorErrorCodeAppService : OnyxAppServiceBase, IVendorErrorCodeAppService
    {
        private readonly IRepository<VendorErrorCode> _vendorErrorCodeRepository;
        public VendorErrorCodeAppService(IRepository<VendorErrorCode> vendorErrorCodeRepository)
        {
            _vendorErrorCodeRepository = vendorErrorCodeRepository;
        }

        public async Task CreateOrUpdateVendorErrorCode(CreateOrUpdateVendorErrorCodeInput input)
        {
            var vendorErrorCode = ObjectMapper.Map<VendorErrorCode>(input);

            if (input.Id == 0)
            {
                await _vendorErrorCodeRepository.InsertAsync(vendorErrorCode);
            }
            else
            {
                await _vendorErrorCodeRepository.UpdateAsync(vendorErrorCode);
            }
        }

        public async Task<GetVendorErrorCodeForEditOutput> GetVendorErrorCodeForEdit(EntityDto<int> input)
        {
            //Editing an existing Vendor Error Code
            var output = new GetVendorErrorCodeForEditOutput();
            if (input.Id == 0)
            {
                output.VendorErrorCode = new VendorErrorCodeDto();
            }
            else
            {
                var vendorErrorCode = await _vendorErrorCodeRepository.GetAsync(input.Id);

                output.VendorErrorCode = ObjectMapper.Map<VendorErrorCodeDto>(vendorErrorCode);
            }

            return output;
        }

        public async Task<PagedResultDto<VendorErrorCodeDto>> GetVendorErrorCode(GetVendorErrorCodeInput input)
        {
            var query = (from vendorErrorCode in _vendorErrorCodeRepository.GetAll().Include(c => c.Vendor)
                         select new VendorErrorCodeDto
                         {
                             Id = vendorErrorCode.Id,
                             ErrorCode = vendorErrorCode.ErrorCode,
                             ErrorText = vendorErrorCode.ErrorText,
                             Comment = vendorErrorCode.Comment,
                             VendorId = vendorErrorCode.VendorId,
                             VendorName = vendorErrorCode.Vendor.Name,
                             CreationTime = vendorErrorCode.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ErrorCode.Equals(input.Filter) || item.VendorName.Contains(input.Filter))
                .WhereIf(input.ErrorCode != 0, item => item.ErrorCode == input.ErrorCode)
                .WhereIf(!input.VendorName.IsNullOrWhiteSpace(), item => item.VendorName == input.VendorName)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<VendorErrorCodeDto>(resultCount, results.ToList());
        }

        public async Task DeleteVendorErrorCode(EntityDto<int> input)
        {
            var vendorErrorCode = await _vendorErrorCodeRepository.GetAsync(input.Id);
            await _vendorErrorCodeRepository.DeleteAsync(vendorErrorCode);
        }
    }
}
