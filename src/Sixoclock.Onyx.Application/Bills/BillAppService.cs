using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Bills.Dto;
using Sixoclock.Onyx.Bills.Exporting;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Bills
{
    public class BillAppService:OnyxAppServiceBase,IBillAppService
    {
        private readonly IRepository<Bill> _billRepository;
        private readonly IBillListExcelExporter _billListExcelExporter;
        private readonly IBillRuleSetExpressionBuilder billRuleSetExpressionBuilder;


        public BillAppService(IRepository<Bill> billRepository, IBillListExcelExporter billListExcelExporter, IBillRuleSetExpressionBuilder billRuleSetExpressionBuilder)
        {
            _billRepository = billRepository;
            _billListExcelExporter = billListExcelExporter;
            this.billRuleSetExpressionBuilder = billRuleSetExpressionBuilder;
        }

        public async Task<GetBillsListoutput> GetBillsList()
        {
            var rules = await billRuleSetExpressionBuilder.BuiExpressionTree();
            var bills = await _billRepository.GetAll().AsExpandable().Include(x => x.BillingType).Include(x => x.BillingStatus).Where(rules).Select(
                x => new BillListDto()
                {
                    BillStatus = x.BillingStatus.Value,
                    BillType = x.BillingType.Type,
                    BillDate = x.BillDate,
                    Comment = x.Comment,
                    DueDate = x.DueDate,
                    Id = x.Id,
                    Number = x.Number,
                    Totalkwh = x.Totalkwh,
                    Transactions = x.Transactions
                }
            ).ToListAsync();
            return new GetBillsListoutput() {Bills = bills};
        }

        public async Task AddComment(AddCommentInputDto input)
        {
            var bill = await _billRepository.GetAsync(input.Id);
            bill.Comment = input.Comment;
            await _billRepository.UpdateAsync(bill);
        }

        public async Task<FileDto> GetBillsToExcel()
        {
            var list = await this.GetBillsList();

            return _billListExcelExporter.ExportToFile(list.Bills.ToList());

        }
    }
}
