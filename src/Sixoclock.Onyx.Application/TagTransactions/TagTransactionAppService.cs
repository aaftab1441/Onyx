using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.TagTransactions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.TagTransactions
{
    public class TagTransactionAppService : OnyxAppServiceBase, ITagTransactionAppService
    {
        private readonly IRepository<TagTransaction> _tagTransactionRepository;
        public TagTransactionAppService(IRepository<TagTransaction> tagTransactionRepository)
        {
            _tagTransactionRepository = tagTransactionRepository;
        }
        public async Task CreateOrUpdateTagTransaction(CreateOrUpdateTagTransactionInput input)
        {
            var tagTransaction = ObjectMapper.Map<TagTransaction>(input);

            if (input.Id == 0)
            {
                await _tagTransactionRepository.InsertAsync(tagTransaction);
            }
            else
            {
                await _tagTransactionRepository.UpdateAsync(tagTransaction);
            }
        }
        public async Task<GetTagTransactionForEditOutput> GetTagTransactionForEdit(EntityDto<int> input)
        {
            //Editing an existing tag transaction
            var output = new GetTagTransactionForEditOutput();
            if (input.Id == 0)
            {
                output.TagTransaction = new TagTransactionDto();
            }
            else
            {
                var tagTransaction = await _tagTransactionRepository.GetAsync(input.Id);

                output.TagTransaction = ObjectMapper.Map<TagTransactionDto>(tagTransaction);
            }

            return output;
        }
        public GetTagTransactionsListOutput GetTagTransactionsList()
        {
            IEnumerable<TagTransactionListDto> _tagTransactionsList = from tagTransaction in _tagTransactionRepository.GetAll().Include(t=>t.Tag.ParentTag)
                                                      select new TagTransactionListDto
                                                      {
                                                          Id = tagTransaction.Id,
                                                          Name = tagTransaction.Tag.ParentTag.Value
                                                      };
            return new GetTagTransactionsListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public ListResultDto<TagTransactionDto> GetTagTransactionsByTransaction(EntityDto<int> input)
        {
            var tagTransactions = from tagTransaction in _tagTransactionRepository.GetAll().Where(t => t.TransactionId == input.Id)
                                  .Include(t => t.Tag)
                                  .Include(t => t.Tag.User)
                                  .Include(t => t.TagTransactionType)
                                  .Include(t => t.Tag.ParentTag)
                                  select new TagTransactionDto
                                  {
                                      Id = tagTransaction.Id,
                                      UserName = tagTransaction.Tag.User.UserName,
                                      TagTransactionType = tagTransaction.TagTransactionType.Value,
                                      TagId = tagTransaction.TagId,
                                      Parent = tagTransaction.Tag.ParentTag.Value,
                                      Expiry = tagTransaction.Tag.Expiry,
                                      CreationTime = tagTransaction.CreationTime
                                  };

            return new ListResultDto<TagTransactionDto>(ObjectMapper.Map<List<TagTransactionDto>>(tagTransactions));
        }

        public async Task<GetTransactionsTotalOutput[]> GetTransactionsTotal(EntityDto<long> input)
        {
            if (input.Id == 0)
            {
                input.Id = GetCurrentUser().Id;
            }
            DateTime start = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek), // prev sunday 00:00
                     end = start.AddDays(7); // next sunday 00:00

            var query = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Where(t => t.Tag.UserId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                        select new GetTransactionsTotalOutput
                        {
                            Date = trx.Transaction.TransactionStopTime.ToString(),
                            KwhDelivered = trx.Transaction.KwhDelivered.ToString()
                        };
            var q = from k in query
                    group k by Convert.ToDateTime(k.Date).Day into g
                    select new GetTransactionsTotalOutput
                    {
                        Date = g.Key.ToString(),
                        TransactionsCount = g.Count().ToString(),
                        KwhDelivered = g.Sum(x => Convert.ToDecimal(x.KwhDelivered)).ToString()
                    };
            var results = await q
                .ToListAsync();

            return results.ToArray();
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByGroup(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.GroupId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByInstall(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.Group.InstallId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByRegion(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.Group.Install.RegionId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByMarket(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.MarketId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalByCustomer(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.CustomerId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public GetTagTransactionsDownloadListOutput GetTransactionsUtilisationTotalBySegment(EntityDto<int> input)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), // prev sunday 00:00
                     end = start.AddMonths(1).AddDays(-1); // next sunday 00:00

            IEnumerable<TagTransactionDownloadListDto> _tagTransactionsList = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                        .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                        .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                        .Where(t => t.Tag.UserId == GetCurrentUser().Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.SegmentId == input.Id && t.Transaction.TransactionStartTime >= start && t.Transaction.TransactionStopTime < end)
                                                                              select new TagTransactionDownloadListDto
                                                                              {
                                                                                  Date = trx.Transaction.CreationTime.ToString(),
                                                                                  TimeStart = trx.Transaction.TransactionStartTime.ToString(),
                                                                                  TimeEnd = trx.Transaction.TransactionStopTime.ToString(),
                                                                                  Duration = trx.Transaction.Duration.ToString(),
                                                                                  KwhDelivered = trx.Transaction.KwhDelivered.ToString(),
                                                                                  User = trx.Tag.User.UserName.ToString(),
                                                                                  Name = trx.Tag.User.FullName.ToString(),
                                                                                  SurName = trx.Tag.User.Surname.ToString(),
                                                                                  Email = trx.Tag.User.EmailAddress.ToString(),
                                                                                  CostKwh = trx.Tag.User.CostkWh.ToString(),
                                                                                  CostMin = trx.Tag.User.CostMin.ToString(),
                                                                                  TagId = trx.Tag.IdToken.ToString(),
                                                                                  Install = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                                                                                  Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                                                                                  Chargepoint = trx.Transaction.EVSE.Chargepoint.Identity,
                                                                                  EVSE = trx.Transaction.EVSE.EVSE_id
                                                                              };

            return new GetTagTransactionsDownloadListOutput { TagTransactions = _tagTransactionsList.ToList() };
        }
        public async Task<PagedResultDto<TransactionListDto>> GetTransactions(GetTransactionsInput input)
        {
            var query = (from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint)
                .Include(t => t.Transaction.EVSE.Chargepoint.ChargepointModel).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Where(t => t.Tag.UserId == input.Id)
                         select new TransactionListDto
                         {
                             Date = trx.Transaction.CreationTime,
                             Duration = trx.Transaction.TransactionStartTime.HasValue && trx.Transaction.TransactionStopTime.HasValue ? (Convert.ToDateTime(trx.Transaction.TransactionStartTime) - Convert.ToDateTime(trx.Transaction.TransactionStopTime)).ToString() : "Ongoing",
                             StartDate = trx.Transaction.TransactionStartTime,
                             StopDate = trx.Transaction.TransactionStopTime,
                             Kwh = trx.Tag.User.TotalkWh,
                             Installation = trx.Transaction.EVSE.Chargepoint.Group.Install.InstallName,
                             Group = trx.Transaction.EVSE.Chargepoint.Group.GroupName,
                             Charger = trx.Transaction.EVSE.Chargepoint.ChargepointModel.ModelName,
                             EVSE = trx.Transaction.EVSE.EVSE_id,
                             User = trx.Tag.User.UserName,
                             BillKwh = trx.Tag.User.CostkWh,
                             BillMin = trx.Tag.User.CostMin,
                             Cost = trx.Transaction.Cost,
                             ToBilled = trx.Transaction.ToBilled,
                             Earned = trx.Transaction.Earned
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<TransactionListDto>(resultCount, results.ToList());
        }
        public GetTransactionsOverviewOutput GetTransactionsOverview()
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardByGroup(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.GroupId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardByInstall(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.Group.InstallId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardByRegion(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.Group.Install.RegionId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardByMarket(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.MarketId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardByCustomer(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.CustomerId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public GetTransactionsOverviewOutput GetTransactionsDashboardBySegment(EntityDto<int> input)
        {
            var user = GetCurrentUser();
            DateTime afterMonday = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
            DateTime beforeMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            var currentWeekAfterMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                         .Include(t => t.Transaction.EVSE).Include(t => t.Transaction.EVSE.Chargepoint).Include(t => t.Transaction.EVSE.Chargepoint.Group)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market).Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer)
                                         .Include(t => t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.Segment)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.EVSE.Chargepoint.Group.Install.Region.Market.Customer.SegmentId == input.Id && t.Transaction.TransactionStopTime >= afterMonday && t.Transaction.TransactionStopTime <= DateTime.Now)
                                         select trx;

            var currentWeekBeforeMonday = from trx in _tagTransactionRepository.GetAll().Include(t => t.Tag).Include(t => t.Tag.User).Include(t => t.Transaction)
                                        .Where(t => t.Tag.UserId == user.Id && t.Transaction.TransactionStopTime >= beforeMonday && t.Transaction.TransactionStopTime < afterMonday)
                                          select trx;

            var output = new GetTransactionsOverviewOutput();
            output.CurrentWeekEnergy = currentWeekAfterMonday.Sum(x => x.Transaction.KwhDelivered);
            output.CurrentWeekCo2Saved = currentWeekAfterMonday.Sum(x => x.Transaction.CO2Saved);
            output.CurrentWeekTransactions = currentWeekAfterMonday.Count();
            output.CurrentWeekChargetime = currentWeekAfterMonday.Sum(x => x.Transaction.Duration.Value.Hour);

            var currentWeekEnergyBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.KwhDelivered);
            var currentWeekCo2SavedBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.CO2Saved);
            var currentWeekTransactionsBeforeMonday = currentWeekBeforeMonday.Count();
            var currentWeekChargetimeBeforeMonday = currentWeekBeforeMonday.Sum(x => x.Transaction.Duration.Value.Hour);
            if (currentWeekBeforeMonday.Count() > 0)
            {
                output.CurrentWeekEnergyChange = ((output.CurrentWeekEnergy - currentWeekEnergyBeforeMonday) / currentWeekEnergyBeforeMonday) * 100;
                output.CurrentWeekCo2SavedChange = ((output.CurrentWeekCo2Saved - currentWeekCo2SavedBeforeMonday) / currentWeekCo2SavedBeforeMonday) * 100;
                output.CurrentWeekTransactionsChange = ((output.CurrentWeekTransactions - currentWeekTransactionsBeforeMonday) / currentWeekTransactionsBeforeMonday) * 100;
                output.CurrentWeekChargetimeChange = ((output.CurrentWeekChargetime - currentWeekChargetimeBeforeMonday) / currentWeekChargetimeBeforeMonday) * 100;
            }
            output.TotalTransactions = user.TotalSessions;
            output.TotalDeliveredEnergy = user.TotalkWh;
            output.TotalCo2Saved = user.TotalCo2Saved;
            output.TotalChargeTime = user.TotalChargeTime;

            return output;
        }
        public async Task DeleteTagTransaction(EntityDto<int> input)
        {
            var tagTransaction = await _tagTransactionRepository.GetAsync(input.Id);
            await _tagTransactionRepository.DeleteAsync(tagTransaction);
        }
    }
}
