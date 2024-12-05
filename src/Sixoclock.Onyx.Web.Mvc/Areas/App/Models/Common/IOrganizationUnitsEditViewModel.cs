using System.Collections.Generic;
using Sixoclock.Onyx.Organizations.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}