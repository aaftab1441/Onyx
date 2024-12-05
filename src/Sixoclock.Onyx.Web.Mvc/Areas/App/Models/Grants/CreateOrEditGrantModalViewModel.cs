using Abp.AutoMapper;
using Sixoclock.Onyx.Grants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Grants
{
    [AutoMapFrom(typeof(GetRuleSetForEditOutput))]
    public class CreateOrEditGrantModalViewModel : GetRuleSetForEditOutput
    {
        public bool IsEditMode
        {
            get { return RuleSet.Id != 0; }
        }

        public CreateOrEditGrantModalViewModel(GetRuleSetForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
