using System.Collections.Generic;

namespace Sixoclock.Onyx.ModelEVSEs.Dto
{
    public class GetModelEVSEsListOutput
    {
        public IEnumerable<ModelEVSEListDto> ModelEVSEs { get; set; }
    }
}
