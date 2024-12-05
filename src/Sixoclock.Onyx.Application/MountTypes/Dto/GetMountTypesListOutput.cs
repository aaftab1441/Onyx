using System.Collections.Generic;

namespace Sixoclock.Onyx.MountTypes.Dto
{
    public class GetMountTypesListOutput
    {
        public IEnumerable<MountTypeListDto> MountTypes { get; set; }
    }
}
