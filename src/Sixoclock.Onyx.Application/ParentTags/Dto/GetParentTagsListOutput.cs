using System.Collections.Generic;

namespace Sixoclock.Onyx.ParentTags.Dto
{
    public class GetParentTagsListOutput
    {
        public IEnumerable<ParentTagListDto> ParentTags { get; set; }
    }
}
