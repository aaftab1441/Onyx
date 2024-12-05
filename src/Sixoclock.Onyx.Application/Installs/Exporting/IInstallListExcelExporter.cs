using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Installs.Exporting
{
    public interface IInstallListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> installListDtos);
    }
}