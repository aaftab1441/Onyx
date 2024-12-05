using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using LinqKit;
using Sixoclock.Onyx.Url;
using Sixoclock.Onyx.Customers.Exporting;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Customers
{
    public class CustomerAppService : OnyxAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        public IAppUrlService AppUrlService { get; set; }
        private readonly ICustomerRuleSetExpressionBuilder _customerRuleSetExpressionBuilder;
        private readonly IAbpSession _abpSession;
        private readonly ICustomerPolicy _customerPolicy;
        private readonly ICustomerListExcelExporter _customerListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public CustomerAppService(IRepository<Customer> customerRepository, 
            ICustomerRuleSetExpressionBuilder customerRuleSetExpressionBuilder, 
            IAbpSession abpSession, 
            ICustomerPolicy customerPolicy,
            ICustomerListExcelExporter customerListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _customerRepository = customerRepository;
            _customerRuleSetExpressionBuilder = customerRuleSetExpressionBuilder;
            this._abpSession = abpSession;
            AppUrlService = NullAppUrlService.Instance;
            _customerPolicy = customerPolicy;
            _customerListExcelExporter = customerListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }

        public async Task CreateCustomer(CreateCustomerInput input)
        {
            var customer = ObjectMapper.Map<Customer>(input);
            var tenantId = _abpSession.GetTenantId();
            var tenant = await TenantManager.GetByIdAsync(tenantId);
            
            if (input.Id == 0)
            {
                if (AbpSession.TenantId.HasValue)
                {
                    await _customerPolicy.CheckMaxCustomerCountAsync(AbpSession.GetTenantId());
                }

                var customerTenantId=await TenantManager.CreateWithAdminUserAsync(input.TenancyName,
                    input.CustomerName,
                    input.AdminPassword,
                    input.AdminEmailAddress,
                    "",
                    true,
                    tenant.EditionId,
                    false,
                    true,
                    tenant.SubscriptionEndDateUtc?.ToUniversalTime(),
                    tenant.IsInTrialPeriod,
                    AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName)
                );
                customer.CustomerTenantId = customerTenantId;
                await _customerRepository.InsertAndGetIdAsync(customer);
            }
            else
            {
                var customerModel = await _customerRepository.GetAsync(customer.Id);
                customer.CustomerTenantId = customerModel.CustomerTenantId;
                await _customerRepository.UpdateAsync(customer);
            }
        }

        public async Task<IEnumerable<CustomerListDto>> GetCustomersList()
        {
            var expressions = await _customerRuleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<CustomerListDto> _customersList = from customers in _customerRepository.GetAll().AsExpandable().Include(x=>x.Country).Include(x => x.Segment).Where(expressions)
                                                      select new CustomerListDto
                                                      {
                                                          Id = customers.Id,
                                                          Name = customers.CustomerName,
                                                      };
            return _customersList.ToList();
        }
        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(EntityDto<int> input)
        {
            //Editing an existing customer
            var output = new GetCustomerForEditOutput();
            if (input.Id == 0)
            {
                output.Customer = new CustomerDto();
            }
            else
            {
                var customer = await _customerRepository.GetAsync(input.Id);

                output.Customer = ObjectMapper.Map<CustomerDto>(customer);
               
            }

            return output;
        }
        public async Task<PagedResultDto<CustomerDto>> GetCustomer(GetCustomerInput input)
        {
            var expressions = await _customerRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from customer in _customerRepository.GetAll().AsExpandable().Include(x => x.Segment).Include(x => x.Country).Where(expressions)
                         select new CustomerDto
                         {
                             Id = customer.Id,
                             CustomerName = customer.CustomerName,
                             Address1 = customer.Address1,
                             Address2 = customer.Address2,
                             ZipCode = customer.ZipCode,
                             City = customer.City,
                             CountryId = customer.Country.Id,
                             CountryName = customer.Country.Value,
                             Phone1 = customer.Phone1,
                             Phone2 = customer.Phone2,
                             SegmentId = customer.SegmentId,
                             SegmentName = customer.Segment.Name,
                             CreationTime = customer.CreationTime,
                             TenantId = customer.TenantId
                         }).AsQueryable();


            var resultCount = await  query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.CustomerName.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.CustomerName == input.Name)
                .WhereIf(!input.Address1.IsNullOrWhiteSpace(), item => item.Address1 == input.Address1)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<CustomerDto>(resultCount, results.ToList());
            
        }
        public FileDto GetCustomersToExcel(EntityDto<int> input)
        {
            var customers = _tagTransactionAppService.GetTransactionsUtilisationTotalByCustomer(input).TagTransactions.ToList();
            
            return _customerListExcelExporter.ExportToFile(customers);
        }
        public async Task DeleteCustomer(EntityDto<int> input)
        {
            var customer = await _customerRepository.GetAsync(input.Id);
            var tenant = await TenantManager.GetByIdAsync(customer.CustomerTenantId);
            await TenantManager.DeleteAsync(tenant);
            await _customerRepository.DeleteAsync(customer);
        }
    }
}
