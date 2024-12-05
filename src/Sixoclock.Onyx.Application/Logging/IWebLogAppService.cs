using Abp.Application.Services;
using Sixoclock.Onyx.Dto;
using Sixoclock.Onyx.Logging.Dto;

namespace Sixoclock.Onyx.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
