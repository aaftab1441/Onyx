using Abp.AutoMapper;
using Sixoclock.Onyx.MountTypes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.MountTypes
{
    [AutoMapFrom(typeof(GetMountTypeForEditOutput))]
    public class CreateOrEditMountTypeViewModel : GetMountTypeForEditOutput
    {
        public CreateOrEditMountTypeViewModel(GetMountTypeForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
