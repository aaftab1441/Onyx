using System.Collections.Generic;

namespace Sixoclock.Onyx.RemoteStartStopEvents.Dto
{
    public class GetRemoteStartStopEventsListOutput
    {
        public IEnumerable<RemoteStartStopEventListDto> RemoteStartStopEvents { get; set; }
    }
}
