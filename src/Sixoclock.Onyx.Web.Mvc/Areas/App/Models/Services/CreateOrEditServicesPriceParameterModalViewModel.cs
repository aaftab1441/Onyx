using Abp.AutoMapper;

using Sixoclock.Onyx.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Services
{
    [AutoMapFrom(typeof(GetServicePriceParameterForEditOutput))]
    public class CreateOrEditServicesPriceParameterModalViewModel : GetServicePriceParameterForEditOutput
    {
        public bool IsEditMode
        {
            get { return PriceParameter.Id != 0; }
        }

        public CreateOrEditServicesPriceParameterModalViewModel(GetServicePriceParameterForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
