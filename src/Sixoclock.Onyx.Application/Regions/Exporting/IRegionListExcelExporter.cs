using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Regions.Exporting
{
    public interface IRegionListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> regionListDtos);
    }
}