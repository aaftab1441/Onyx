using System.Collections.Generic;
using Sixoclock.Onyx.Auditing.Dto;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
