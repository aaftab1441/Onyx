using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Authorization.Users.Dto;
using Sixoclock.Onyx.MeterValues;
using Sixoclock.Onyx.Tags.Dto;
using Sixoclock.Onyx.TagTransactions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.TagStatuses;
using Sixoclock.Onyx.TagStatuses.Dto;

namespace Sixoclock.Onyx.Tags
{
    public class TagAppService : OnyxAppServiceBase, ITagAppService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagTransaction> _tagTransactionRepository;
        private readonly IMeterValueAppService _meterValueAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IUserAppService _userAppService;
        private readonly ITagRuleSetExpressionBuilder _tagRuleSetExpressionBuilder;
        private readonly ITagStatusAppService _tagStatusAppService;

        public TagAppService(IRepository<Tag> tagRepository,
            IRepository<TagTransaction> tagTransactionRepository,
            IMeterValueAppService meterValueAppService,
            ITagTransactionAppService tagTransactionAppService,
            IRepository<Transaction> transactionRepository,
            IUserAppService userAppService,
            ITagStatusAppService tagStatusAppService,
            ITagRuleSetExpressionBuilder tagRuleSetExpressionBuilder)
        {
            _tagRepository = tagRepository;
            _tagTransactionRepository = tagTransactionRepository;
            _meterValueAppService = meterValueAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _transactionRepository = transactionRepository;
            _userAppService = userAppService;
            _tagRuleSetExpressionBuilder = tagRuleSetExpressionBuilder;
            _tagStatusAppService = tagStatusAppService;
        }
        public async Task CreateOrUpdateTag(CreateOrUpdateTagInput input)
        {
            var tag = new Tag();
            tag.AuthorizationStatusId = input.AuthorizationStatusId;
            tag.Comment = input.Comment;
            tag.Expiry = input.Expiry;
            tag.IdToken = input.IdToken;
            tag.ParentTagId = input.ParentTagId;
            tag.ServiceContact = input.ServiceContact;
            tag.UserId = input.UserId;

            if (input.Id == 0)
            {
                await _tagRepository.InsertAsync(tag);
            }
            else
            {
                await _tagRepository.UpdateAsync(tag);
            }
        }
        public async Task<GetTagForEditOutput> GetTagForEdit(EntityDto<int> input)
        {
            //Editing an existing tag
            var output = new GetTagForEditOutput();
            if (input.Id == 0)
            {
                output.Tag = new TagDto();
            }
            else
            {
                var tag = await _tagRepository.GetAsync(input.Id);

                output.Tag = ObjectMapper.Map<TagDto>(tag);
            }

            return output;
        }
        public async Task<PagedResultDto<TagDto>> GetTag(GetTagInput input)
        {
            var ruleCondition = await _tagRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from tag in _tagRepository.GetAll().AsExpandable().Include(t => t.AuthorizationStatus).Include(t => t.User)
                         select new TagDto
                         {
                             Id = tag.Id,
                             IdToken = tag.IdToken,
                             Expiry = tag.Expiry,
                             ServiceContact = tag.ServiceContact,
                             ParentTagId = tag.ParentTagId,
                             AuthorizationStatusId = tag.AuthorizationStatusId,
                             AuthorizationStatus = tag.AuthorizationStatus.Value,
                             UserId = tag.UserId,
                             Comment = tag.Comment,
                             CreationTime = tag.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.AuthorizationStatus.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), item => item.AuthorizationStatus == input.Status)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var tags = new ListResultDto<TagDto>(ObjectMapper.Map<List<TagDto>>(results));
            foreach (var tag in tags.Items)
            {
                var user = UserManager.Users.Where(u => u.Id == tag.UserId).FirstOrDefault();
                tag.UserName = user?.Name;
            }

            return new PagedResultDto<TagDto>(resultCount, results.ToList());
        }
        public TagDto GetTagForViewDetails(EntityDto<int> input)
        {
            TagDto transaction = (from trx in _transactionRepository.GetAll().Where(t => t.TagTransactions.FirstOrDefault().TagId == input.Id)
                               .Include(t => t.TagTransactions)
                               .Include(t => t.EVSE.Chargepoint)
                               .Include(t => t.EVSE.Chargepoint.Group).Include(t => t.EVSE.Chargepoint.Group.Install)
                               .Include(t => t.EVSE.Chargepoint.Group.Install.Region)
                               .Include(t => t.EVSE.Chargepoint.Group.Install.Region.Market)
                               .Include(t => t.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                               .Include(t => t.EVSE.Chargepoint.ChargepointModel)
                               .Include(t => t.EVSE.Chargepoint.ChargepointModel.Vendor)
                               .Include(t => t.EVSE.MeterType)
                               .Include(t => t.TransactionType)
                               .Include(t => t.TransactionStatus)
                               .Include(t => t.Reason)
                               .Include(t => t.TagTransactions)
                               select new TagDto
                               {
                                   Id = trx.Id,
                                   ClientName = trx.EVSE.Chargepoint.Group.Install.Region.Market.Customer.CustomerName,
                                   MarketName = trx.EVSE.Chargepoint.Group.Install.Region.Market.MarketName,
                                   RegionName = trx.EVSE.Chargepoint.Group.Install.Region.RegionName,
                                   InstallName = trx.EVSE.Chargepoint.Group.Install.InstallName,
                                   GroupName = trx.EVSE.Chargepoint.Group.GroupName,
                                   VendorName = trx.EVSE.Chargepoint.ChargepointModel.Vendor.Name,
                                   ModelName = trx.EVSE.Chargepoint.ChargepointModel.ModelName,
                                   Place = trx.EVSE.Chargepoint.Place,
                                   TransactionStartTime = trx.TransactionStartTime,
                                   TransactionStopTime = trx.TransactionStopTime,
                                   ReasonId = trx.ReasonId,
                                   EVSEId = trx.EVSEId,
                                   TransactionStatusId = trx.TransactionStatusId,
                                   TransactionStatusValue = trx.TransactionStatus.Value,
                                   TransactionTypeId = trx.TransactionTypeId,
                                   TransactionType = trx.TransactionType.Type,
                                   Comment = trx.Comment,
                                   Identity = trx.EVSE.Chargepoint.Identity,
                                   RemoteReason = trx.Reason.ReasonName,
                                   CreationTime = trx.CreationTime,
                                   MeterType = trx.EVSE.MeterType.Type
                               }).FirstOrDefault();
            
            var tagtrxs = from tagtrx in _tagTransactionRepository.GetAll().Where(tt => tt.TransactionId == transaction.Id).Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Tag.User)
                          select tagtrx;
            if (tagtrxs != null)
            {
                try
                {
                    transaction.TransactionStartUserName = tagtrxs.Where(t => t.TagTransactionTypeId == 1).FirstOrDefault()?.Tag.User.UserName;
                    transaction.TransactionStopUserName = tagtrxs.Where(t => t.TagTransactionTypeId == 2).FirstOrDefault()?.Tag.User.UserName;
                }
                catch
                {

                }
            }
            if (transaction != null)
            {
                input.Id = transaction.Id;
                transaction.ConnectorMeterValues = _meterValueAppService.GetMeterValueByTransaction(input);
                transaction.TagDetails = _tagTransactionAppService.GetTagTransactionsByTransaction(input);
            }
            else
            {
                transaction = new TagDto();
                transaction.ConnectorMeterValues = new ListResultDto<MeterValues.Dto.MeterValueDto>();
                transaction.TagDetails = new ListResultDto<TagTransactions.Dto.TagTransactionDto>();
            }

            return transaction;
        }
        public UserEditDto GetUserByIdToken(string idToken)
        {
            var tag = _tagRepository.GetAll().Where(t => t.IdToken.Contains(idToken)).FirstOrDefault();


            var user = UserManager.Users.Where(u => u.Id == tag.UserId).FirstOrDefault();
            var obj = ObjectMapper.Map<UserEditDto>(user);
            obj.Id = tag.Id;
            return obj;
        }
        public async Task<PagedResultDto<GetKeyCardOutput>> GetKeyCards(GetKeyCardsInput input)
        {
            var query = (from tag in _tagRepository.GetAll()
                                  .Include(t => t.TagStatus)
                                  .Include(t => t.TagTransactions)
                                  .Where(t => t.User.Id == GetCurrentUser().Id)
                         select new GetKeyCardOutput
                         {
                             Id = tag.Id,
                             IdToken = tag.IdToken,
                             AuthorizationStatus = tag.TagStatus.Status,
                             KwhCharged = tag.TagTransactions.Sum(t => t.Transaction.KwhDelivered),
                             Transactions = tag.TagTransactions.Count(),
                             Expiry = tag.Expiry,
                             LastUsed = tag.TagTransactions.OrderByDescending(x => x.CreationTime).FirstOrDefault().CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<GetKeyCardOutput>(resultCount, results.ToList());
        }
        public async Task ActivateTag(EntityDto<int> input)
        {
            GetTagStatusByNameInput tagStatusInput = new GetTagStatusByNameInput();
            tagStatusInput.StatusName = "Active";
            tagStatusInput.TenantId = GetCurrentTenant().Id;
            var tagStatusId = _tagStatusAppService.GetTagStatusByName(tagStatusInput).Id;
            var tag = await _tagRepository.GetAsync(input.Id);
            tag.TagStatusId = tagStatusId;
            await _tagRepository.UpdateAsync(tag);
        }
        public async Task InActivateTag(EntityDto<int> input)
        {
            GetTagStatusByNameInput tagStatusInput = new GetTagStatusByNameInput();
            tagStatusInput.StatusName = "Inactive";
            tagStatusInput.TenantId = GetCurrentTenant().Id;
            var tagStatusId = _tagStatusAppService.GetTagStatusByName(tagStatusInput).Id;
            var tag = await _tagRepository.GetAsync(input.Id);
            tag.TagStatusId = tagStatusId;
            await _tagRepository.UpdateAsync(tag);
        }
        public async Task DeleteTag(EntityDto<int> input)
        {
            var tag = await _tagRepository.GetAsync(input.Id);
            await _tagRepository.DeleteAsync(tag);
        }
    }
}
