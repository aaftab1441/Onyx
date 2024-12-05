using Abp.AutoMapper;
using Sixoclock.Onyx.Grants.Dto;
using Sixoclock.Onyx.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Services
{
    [AutoMapFrom(typeof(GetServiceForEditOutput))]
    public class CreateOrEditServicesModalViewModel : GetServiceForEditOutput
    {
        public bool IsEditMode
        {
            get { return Service.Id != 0; }
        }

        public CreateOrEditServicesModalViewModel(GetServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
