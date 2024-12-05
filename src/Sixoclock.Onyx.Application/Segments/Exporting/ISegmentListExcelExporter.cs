using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Segments.Exporting
{
    public interface ISegmentListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> segmentListDtos);
    }
}