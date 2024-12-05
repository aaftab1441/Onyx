using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Editions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}