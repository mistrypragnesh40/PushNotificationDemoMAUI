using CommunityToolkit.Mvvm.Messaging;
using ObjCRuntime;
using PushNotificationDemoMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace PushNotificationDemoMAUI.Platforms.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            string navigationID = userInfo["NavigationID"].ToString();
            Preferences.Set("NavigationID", navigationID);

            WeakReferenceMessenger.Default.Send(new PushNotificationReceived("test"));
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

    }
}
