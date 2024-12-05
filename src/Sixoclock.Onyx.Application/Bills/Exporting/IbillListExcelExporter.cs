using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.Bills.Dto;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Bills.Exporting
{
    public interface IBillListExcelExporter
    {
        FileDto ExportToFile(List<BillListDto> billListDtos);
    }
}
