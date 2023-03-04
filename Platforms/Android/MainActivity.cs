using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using PushNotificationDemoMAUI.Models;

namespace PushNotificationDemoMAUI;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    internal static readonly string Channel_ID = "TestChannel";
    internal static readonly int NotificationID= 101;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
       
        CreateNotificationChannel();
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        if (intent.Extras != null)
        {
            foreach (var key in intent.Extras.KeySet())
            {
                if (key == "NavigationID")
                {
                    string idValue = intent.Extras.GetString(key);
                    if (Preferences.ContainsKey("NavigationID"))
                        Preferences.Remove("NavigationID");

                    Preferences.Set("NavigationID", idValue);

                    WeakReferenceMessenger.Default.Send(new PushNotificationReceived("test"));
                }
            }
        }
    }

    private void CreateNotificationChannel()
    {
        if (OperatingSystem.IsOSPlatformVersionAtLeast("android", 26))
        {
            var channel = new NotificationChannel(Channel_ID, "Test Notfication Channel", NotificationImportance.Default);

            var notificaitonManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificaitonManager.CreateNotificationChannel(channel);
        }
    }
}
