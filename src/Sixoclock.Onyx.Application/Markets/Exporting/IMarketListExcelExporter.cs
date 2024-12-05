using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Markets.Exporting
{
    public interface IMarketListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> marketListDtos);
    }
}