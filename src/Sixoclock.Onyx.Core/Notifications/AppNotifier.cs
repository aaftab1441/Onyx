using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.MultiTenancy;

namespace Sixoclock.Onyx.Notifications
{
    public class AppNotifier : OnyxDomainServiceBase, IAppNotifier
    {
        private readonly INotificationPublisher _notificationPublisher;

        public AppNotifier(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public async Task WelcomeToTheApplicationAsync(User user)
        {
            await _notificationPublisher.PublishAsync(
                AppNotificationNames.WelcomeToTheApplication,
                new MessageNotificationData(L("WelcomeToTheApplicationNotificationMessage")),
                severity: NotificationSeverity.Success,
                userIds: new[] { user.ToUserIdentifier() }
                );
        }

        public async Task NewUserRegisteredAsync(User user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewUserRegisteredNotificationMessage",
                    OnyxConsts.LocalizationSourceName
                    )
                );

            notificationData["userName"] = user.UserName;
            notificationData["emailAddress"] = user.EmailAddress;

            await _notificationPublisher.PublishAsync(AppNotificationNames.NewUserRegistered, notificationData, tenantIds: new[] { user.TenantId });
        }

        public async Task NewTenantRegisteredAsync(Tenant tenant)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewTenantRegisteredNotificationMessage",
                    OnyxConsts.LocalizationSourceName
                    )
                );

            notificationData["tenancyName"] = tenant.TenancyName;
            await _notificationPublisher.PublishAsync(AppNotificationNames.NewTenantRegistered, notificationData);
        }

        //This is for test purposes
        public async Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info)
        {
            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: severity,
                userIds: new[] { user }
                );
        }
    }
}