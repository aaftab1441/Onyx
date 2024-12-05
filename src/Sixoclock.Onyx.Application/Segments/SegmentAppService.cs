using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Segments.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.Segments.Exporting;
using Sixoclock.Onyx.TagTransactions.Dto;
using Sixoclock.Onyx.TagTransactions;

namespace Sixoclock.Onyx.Segments
{
    public class SegmentAppService : OnyxAppServiceBase, ISegmentAppService
    {
        private readonly IRepository<Segment> _segmentRepository;
        private readonly ISegmentRuleSetExpressionBuilder _segmentExpressionBuilder;
        private readonly ISegmentListExcelExporter _segmentListExcelExporter;
        private readonly ITagTransactionAppService _tagTransactionAppService;

        public SegmentAppService(IRepository<Segment> segmentRepository, 
            ISegmentRuleSetExpressionBuilder segmentExpressionBuilder,
            ISegmentListExcelExporter segmentListExcelExporter,
            ITagTransactionAppService tagTransactionAppService)
        {
            _segmentRepository = segmentRepository;
            _segmentExpressionBuilder = segmentExpressionBuilder;
            _segmentListExcelExporter = segmentListExcelExporter;
            _tagTransactionAppService = tagTransactionAppService;
        }
        public async Task CreateOrUpdateSegment(CreateOrUpdateSegmentInput input)
        {
            var segment = ObjectMapper.Map<Segment>(input);

            if (input.Id == 0)
            {
                await _segmentRepository.InsertAsync(segment);
            }
            else
            {
                await _segmentRepository.UpdateAsync(segment);
            }
        }
        public async Task<GetSegmentForEditOutput> GetSegmentForEdit(EntityDto<int> input)
        {
            //Editing an existing segment
            var output = new GetSegmentForEditOutput();
            if (input.Id == 0)
            {
                output.Segment = new segmentListDtos();
            }
            else
            {
                var segment = await _segmentRepository.GetAsync(input.Id);

                output.Segment = ObjectMapper.Map<segmentListDtos>(segment);
            }

            return output;
        }
        public async Task<GetSegmentsListOutput> GetSegmentsList()
        {
            var grantExpression = await _segmentExpressionBuilder.BuiExpressionTree();
            IEnumerable<SegmentListDto> _segmentsList = from segment in _segmentRepository.GetAll().AsExpandable().Where(grantExpression)
                                                        select new SegmentListDto
                                                        {
                                                            Id = segment.Id,
                                                            Name = segment.Name
                                                        };
            return new GetSegmentsListOutput { Segments = _segmentsList.ToList() };
        }
        public async Task<PagedResultDto<segmentListDtos>> GetSegment(GetSegmentInput input)
        {
            var grantExpression = await _segmentExpressionBuilder.BuiExpressionTree();
            var query = (from segment in _segmentRepository.GetAll().AsExpandable().Where(grantExpression)
                         select new segmentListDtos
                         {
                             Id = segment.Id,
                             Name = segment.Name,
                             Comment = segment.Comment,
                             CreationTime = segment.CreationTime
                         }).AsQueryable();


            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    item => item.Name.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.Name == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<segmentListDtos>(resultCount, results.ToList());

        }
        public FileDto GetSegmentsToExcel(EntityDto<int> input)
        {
            var segments = _tagTransactionAppService.GetTransactionsUtilisationTotalBySegment(input);
            var segmentDtos = segments.TagTransactions.ToList();

            return _segmentListExcelExporter.ExportToFile(segmentDtos);
        }
        public async Task DeleteSegment(EntityDto<int> input)
        {
            var segment = await _segmentRepository.GetAsync(input.Id);
            await _segmentRepository.DeleteAsync(segment);
        }
    }
}

