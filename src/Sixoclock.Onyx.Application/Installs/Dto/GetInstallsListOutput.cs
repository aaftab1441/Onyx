using System.Collections.Generic;

namespace Sixoclock.Onyx.Installs.Dto
{
    public class GetInstallsListOutput
    {
        public IEnumerable<InstallListDto> Installs { get; set; }
    }
}
