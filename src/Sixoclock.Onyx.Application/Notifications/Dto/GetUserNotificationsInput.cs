using Abp.Notifications;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}