using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.ModelKeyValues.Dto
{
    public class GetModelKeyValuesListByChargepointModelIdListOutput
    {
        public IEnumerable<ModelKeyValueByChargepointModelIdListDto> ModelKeyValues { get; set; }
    }
}
