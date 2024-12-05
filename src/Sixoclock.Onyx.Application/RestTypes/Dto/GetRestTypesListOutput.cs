using System.Collections.Generic;

namespace Sixoclock.Onyx.RestTypes.Dto
{
    public class GetRestTypesListOutput
    {
        public IEnumerable<RestTypeListDto> RestTypes { get; set; }
    }
}
