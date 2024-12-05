using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Segments
{
    [AutoMapFrom(typeof(GetSegmentServiceForEditOutput))]
    public class CreateOrEditSegmentServiceViewModel:GetSegmentServiceForEditOutput
    {
        public CreateOrEditSegmentServiceViewModel(GetSegmentServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
