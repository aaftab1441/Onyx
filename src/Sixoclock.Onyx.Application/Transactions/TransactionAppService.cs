using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Transactions.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;

namespace Sixoclock.Onyx.Transactions
{
    public class TransactionAppService : OnyxAppServiceBase, ITransactionAppService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<TagTransaction> _tagTransactionRepository;
        private readonly ITransactionRuleSetExpressionBuilder _transactionRuleSetExpressionBuilder;

        public TransactionAppService(IRepository<Transaction> transactionRepository,
            IRepository<TagTransaction> tagTransactionRepository,
            ITransactionRuleSetExpressionBuilder transactionRuleSetExpressionBuilder)
        {
            _transactionRepository = transactionRepository;
            _tagTransactionRepository = tagTransactionRepository;
            _transactionRuleSetExpressionBuilder = transactionRuleSetExpressionBuilder;
        }

        public async Task<PagedResultDto<TransactionDto>> GetTransactionsByChargepoint(GetTransactionsByChargepointInput input)
        {
            var ruleCondition = await _transactionRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from trx in _transactionRepository.GetAll().AsExpandable()
                               .Include(t => t.TagTransactions)
                               .Include(t => t.EVSE.Chargepoint)
                               .Include(t => t.EVSE.MeterType)
                               .Include(t => t.TransactionType)
                               .Include(t => t.TransactionStatus)
                               .Include(t => t.Reason)
                               .Include(t => t.TagTransactions)
                               .Where(t => t.EVSE.ChargepointId == input.Id).Where(ruleCondition)
                         select new TransactionDto
                         {
                             Id = trx.Id,
                             Place = trx.EVSE.Chargepoint.Place,
                             TransactionStartTime = trx.TransactionStartTime,
                             TransactionStopTime = trx.TransactionStopTime,
                             ReasonId = trx.ReasonId,
                             EVSEId = trx.EVSEId,
                             EVSE_id = trx.EVSE.EVSE_id,
                             TransactionStatusId = trx.TransactionStatusId,
                             TransactionStatusValue = trx.TransactionStatus.Value,
                             TransactionTypeId = trx.TransactionTypeId,
                             TransactionType = trx.TransactionType.Type,
                             Comment = trx.Comment,
                             Identity = trx.EVSE.Chargepoint.Identity,
                             RemoteReason = trx.Reason.ReasonName,
                             CreationTime = trx.CreationTime,
                             MeterType = trx.EVSE.MeterType.Type
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            foreach (var transaction in results.ToList())
            {
                var tagtrxs = from tagtrx in _tagTransactionRepository.GetAll().Where(tt => tt.TransactionId == transaction.Id).Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Tag.User)
                              select tagtrx;
                if (tagtrxs != null)
                {
                    try
                    {
                        transaction.TransactionStartUserName = tagtrxs.Where(t => t.TagTransactionTypeId == 1).FirstOrDefault()?.Tag.User.UserName;
                    }
                    catch
                    {

                    }
                }
            }

            return new PagedResultDto<TransactionDto>(resultCount, results.ToList());
        }
        public async Task<PagedResultDto<TransactionDto>> GetUserTransactions(GetTransactionInput input)
        {
            var ruleCondition = await _transactionRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from tagTrx in _tagTransactionRepository.GetAll()
                               .Include(t => t.Tag)
                               .Include(t => t.Transaction)
                               .Include(t => t.Transaction.EVSE)
                               .Include(t => t.Transaction.EVSE.Chargepoint)
                               .Include(t => t.Transaction.EVSE.Chargepoint.ChargepointModel)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group)
                               .AsExpandable()
                               .Where(t => t.Tag.UserId == GetCurrentUser().Id)
                         select new TransactionDto
                         {
                             TransactionStartTime = tagTrx.Transaction.TransactionStartTime,
                             Duration = tagTrx.Transaction.Duration,
                             Kwh = tagTrx.Transaction.KwhDelivered,
                             Tag = tagTrx.Tag.IdToken,
                             InstallName = tagTrx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                             GroupName = tagTrx.Transaction.EVSE.Chargepoint.Group.GroupName,
                             ModelName = tagTrx.Transaction.EVSE.Chargepoint.ChargepointModel.ModelName,
                             Cost = tagTrx.Transaction.Cost
                         });

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<TransactionDto>(resultCount, results.ToList());
        }
        public async Task<PagedResultDto<TransactionDto>> GetTenantTransactions(GetTransactionInput input)
        {
            var ruleCondition = await _transactionRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from tagTrx in _tagTransactionRepository.GetAll()
                               .Include(t => t.Tag)
                               .Include(t => t.Transaction)
                               .Include(t => t.Transaction.EVSE)
                               .Include(t => t.Transaction.EVSE.Chargepoint)
                               .Include(t => t.Transaction.EVSE.Chargepoint.ChargepointModel)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install)
                               .Include(t => t.Transaction.EVSE.Chargepoint.Group)
                               .AsExpandable()
                               .Where(t => t.TenantId == GetCurrentTenant().Id)
                         select new TransactionDto
                         {
                             TransactionStartTime = tagTrx.Transaction.TransactionStartTime,
                             Duration = tagTrx.Transaction.Duration,
                             Kwh = tagTrx.Transaction.KwhDelivered,
                             Tag = tagTrx.Tag.IdToken,
                             InstallName = tagTrx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                             GroupName = tagTrx.Transaction.EVSE.Chargepoint.Group.GroupName,
                             ModelName = tagTrx.Transaction.EVSE.Chargepoint.ChargepointModel.ModelName,
                             Cost = tagTrx.Transaction.Cost
                         });

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<TransactionDto>(resultCount, results.ToList());
        }
    }
}
