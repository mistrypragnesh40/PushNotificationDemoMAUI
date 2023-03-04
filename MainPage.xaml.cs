using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using PushNotificationDemoMAUI.Models;
using System.Text;

namespace PushNotificationDemoMAUI;

public partial class MainPage : ContentPage
{
    int count = 0;
    private string _deviceToken;
    public MainPage()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<PushNotificationReceived>(this, (r, m) =>
        {
            string msg = m.Value;
            NavigateToPage();
        });

        if (Preferences.ContainsKey("DeviceToken"))
        {
            _deviceToken = Preferences.Get("DeviceToken", "");
        }

        NavigateToPage();
    }

    private void NavigateToPage()
    {

        if (Preferences.ContainsKey("NavigationID"))
        {
            string id = Preferences.Get("NavigationID", "");
            if (id == "1")
            {
                AppShell.Current.GoToAsync(nameof(NewPage1));
            }
            if (id == "2")
            {
                AppShell.Current.GoToAsync(nameof(NewPage2));
            }
            Preferences.Remove("NavigationID");
        }
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var androidNotificationObject = new Dictionary<string, string>();
        androidNotificationObject.Add("NavigationID", "2");

        var pushNotificationRequest = new PushNotificationRequest
        {
            notification = new NotificationMessageBody
            {
                title = "Notification Title",
                body = "Notification body"
            },
            data = androidNotificationObject,
            registration_ids = new List<string> { _deviceToken }
        };

        string url = "https://fcm.googleapis.com/fcm/send";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + "Cloud Messaging Server Key");

            string serializeRequest = JsonConvert.SerializeObject(pushNotificationRequest);
            var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Notification sent", "notification sent", "OK");
            }
        }
    }
}

