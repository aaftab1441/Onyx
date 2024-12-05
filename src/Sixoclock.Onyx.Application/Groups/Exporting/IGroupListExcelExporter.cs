using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Groups.Exporting
{
    public interface IGroupListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> groupListDtos);
    }
}