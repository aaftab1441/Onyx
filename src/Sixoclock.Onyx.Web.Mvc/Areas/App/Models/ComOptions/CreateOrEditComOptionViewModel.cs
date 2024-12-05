using Abp.AutoMapper;
using Sixoclock.Onyx.ComOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ComOptions
{
    [AutoMapFrom(typeof(GetComOptionForEditOutput))]
    public class CreateOrEditComOptionViewModel : GetComOptionForEditOutput
    {
        public CreateOrEditComOptionViewModel(GetComOptionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
