using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Sixoclock.Onyx.Bills.Dto;
using Sixoclock.Onyx.DataExporting.Excel.EpPlus;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Bills.Exporting
{
    public class BillListExcelExporter: EpPlusExcelExporterBase, IBillListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
       
        public BillListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<BillListDto> billListDtos)
        {
            return CreateExcelPackage(
                "BillList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Bills"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Number"),
                        L("Status"),
                        L("Type"),
                        L("Totalkwh"),
                        L("Transactions"),
                        L("Comment"),
                        L("Date"),
                        L("DueDate")
                    );

                    AddObjects(
                        sheet, 2, billListDtos.ToList(),
                        _ => _.Number,
                        _ => _.BillStatus,
                        _ => _.BillType,
                        _ => _.Totalkwh,
                        _ => _.Transactions,
                        _ => _.Comment,
                        _ => _.BillDate,
                        _ => _.DueDate
                    );

                    //Formatting cells

                    var billDateColumn = sheet.Column(7);
                    billDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    var dueDateColumn = sheet.Column(8);
                    dueDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                    for (var i = 1; i <= 9; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
