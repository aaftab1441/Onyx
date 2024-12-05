using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Sixoclock.Onyx.Grants.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Grants
{
    [AutoMapFrom(typeof(GetRuleForEditOutputDto))]
    public class CreateOrEditGrantRuleModelViewModel : GetRuleForEditOutputDto
    {
        public bool IsEditMode => Rule.Id != 0;

        public CreateOrEditGrantRuleModelViewModel(GetRuleForEditOutputDto output)
        {
            output.MapTo(this);
        }
    }
}
