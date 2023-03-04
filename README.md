# PushNotificationDemoMAUI

## Youtube Video 
* Implement Push Notification for android: https://youtu.be/gBbbctEvbOk
* Implement PUsh Notification for iOS: https://youtu.be/YNkdsJTyOJE
	


## Android Platform

Download GoogleService.Json File from google firebase console & add it in Platforms/Android folder & Set that file build action as GoogleServicesJson.

If you can not see this GoogleServicesJson Option. Then Install following plugin in your android project depedency.
* Xamarin.Google.Dagger 
* Xamarin.GooglePlayService.Base


## iOS Platform
Download GoogleService-Info.plist File from google firebase console & add it in Platforms/iOS folder & You need to set build action as BundleResource manually 
by editing project file.

```
<ItemGroup>
		<BundleResource Include="Platforms\iOS\GoogleService-Info.plist" Link="GoogleService-Info.plist" />
	</ItemGroup>
```
