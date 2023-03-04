using Firebase.CloudMessaging;
using Foundation;
using PushNotificationDemoMAUI.Platforms.iOS;
using UIKit;
using UserNotifications;
using static UIKit.UIGestureRecognizer;

namespace PushNotificationDemoMAUI;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate, IMessagingDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        Firebase.Core.App.Configure();

        if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
        {
            var authOption = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

            UNUserNotificationCenter.Current.RequestAuthorization(authOption, (granted, error) =>
            {

            });

            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            Messaging.SharedInstance.AutoInitEnabled = true;
            Messaging.SharedInstance.Delegate = this;
        }
        UIApplication.SharedApplication.RegisterForRemoteNotifications();

        return base.FinishedLaunching(application, launchOptions);
    }

    [Export("messaging:didReceiveRegistrationToken:")]
    public void DidReceiveRegistrationToken(Messaging message, string regToken)
    {
        if (Preferences.ContainsKey("DeviceToken"))
        {
            Preferences.Remove("DeviceToken");
        }
        Preferences.Set("DeviceToken", regToken);
    }
}
