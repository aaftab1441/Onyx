using Abp.Application.Navigation;

namespace Sixoclock.Onyx.Web.Areas.App.Views.Shared.Components.AppSideBar
{
    public class UserMenuItemViewModel
    {
        public UserMenuItem MenuItem { get; set; }

        public string CurrentPageName { get; set; }

        public int MenuItemIndex { get; set; }

        public bool RootLevel { get; set; }
    }
}
