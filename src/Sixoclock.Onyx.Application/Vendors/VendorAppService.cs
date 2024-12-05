using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Vendors.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Vendors
{
    public class VendorAppService : OnyxAppServiceBase, IVendorAppService
    {
        private readonly IRepository<Vendor> _vendorRepository;
        public VendorAppService(IRepository<Vendor> vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task CreateOrUpdateVendor(CreateOrUpdateVendorInput input)
        {
            var vendor = ObjectMapper.Map<Vendor>(input);

            if (input.Id == 0)
            {
                await _vendorRepository.InsertAsync(vendor);
            }
            else
            {
                await _vendorRepository.UpdateAsync(vendor);
            }
        }
        public async Task<GetVendorForEditOutput> GetVendorForEdit(EntityDto<int> input)
        {
            //Editing an existing vendor
            var output = new GetVendorForEditOutput();
            if (input.Id == 0)
            {
                output.Vendor = new VendorDto();
            }
            else
            {
                var vendor = await _vendorRepository.GetAsync(input.Id);

                output.Vendor = ObjectMapper.Map<VendorDto>(vendor);
            }

            return output;
        }
        public GetVendorsListOutput GetVendorsList()
        {
            IEnumerable<VendorListDto> _vendorsList = from vendor in _vendorRepository.GetAll()
                                                        select new VendorListDto
                                                        {
                                                            Id = vendor.Id,
                                                            Name = vendor.Name
                                                        };
            return new GetVendorsListOutput { Vendors = _vendorsList.ToList() };
        }
        public async Task<PagedResultDto<VendorDto>> GetVendor(GetVendorInput input)
        {
            var query = (from vendor in _vendorRepository.GetAll()
                         select new VendorDto
                         {
                             Id = vendor.Id,
                             Comment = vendor.Comment,
                             CreationTime = vendor.CreationTime,
                             Name = vendor.Name
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Name.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.Name == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<VendorDto>(resultCount, results.ToList());
        }
        public async Task DeleteVendor(EntityDto<int> input)
        {
            var vendor = await _vendorRepository.GetAsync(input.Id);
            await _vendorRepository.DeleteAsync(vendor);
        }
    }
}
