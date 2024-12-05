﻿using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Sixoclock.Onyx.DataExporting.Excel.EpPlus;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.TagTransactions.Dto;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Regions.Exporting
{
    public class RegionListExcelExporter : EpPlusExcelExporterBase, IRegionListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public RegionListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<TagTransactionDownloadListDto> regionListDtos)
        {
            return CreateExcelPackage(
                "RegionList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Regions"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Date"),
                        L("TimeStart"),
                        L("TimeEnd"),
                        L("Duration"),
                        L("KwhDelivered"),
                        L("User"),
                        L("Name"),
                        L("SurName"),
                        L("Email"),
                        L("CostKwh"),
                        L("CostMin"),
                        L("TagId"),
                        L("Install"),
                        L("Group"),
                        L("Chargepoint"),
                        L("EVSE")
                        );

                    AddObjects(
                        sheet, 2, regionListDtos,
                        _ => _.Date,
                        _ => _.TimeStart,
                        _ => _.TimeEnd,
                        _ => _.Duration,
                        _ => _.KwhDelivered,
                        _ => _.User,
                        _ => _.Name,
                        _ => _.SurName,
                        _ => _.Email,
                        _ => _.CostKwh,
                        _ => _.CostMin,
                        _ => _.TagId,
                        _ => _.Install,
                        _ => _.Group,
                        _ => _.Chargepoint,
                        _ => _.EVSE
                        );

                    for (var i = 1; i <= 16; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}