using System.Collections.Generic;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Customers.Exporting
{
    public interface ICustomerListExcelExporter
    {
        FileDto ExportToFile(List<TagTransactionDownloadListDto> customerListDtos);
    }
}