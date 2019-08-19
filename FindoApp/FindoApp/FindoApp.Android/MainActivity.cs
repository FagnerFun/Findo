using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Android.Webkit;
using FindoApp.Services;
using FindoApp.Model.interfaces;
using Xamarin.Facebook;
using Newtonsoft.Json.Linq;
using Android.Util;
using Android.Gms.Common;

namespace FindoApp.Droid
{
    [Activity(Label = "FindoApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        public const string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";

        MobileServiceUser user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App.Init((IAuthenticate)this);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            IsPlayServicesAvailable();
            CreateNotificationChannel();

            LoadApplication(new App());
        }

        public async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await AzureService.DefaultManager.CurrentClient.LoginAsync(this, provider, Settings.URLScheme);
                    if (user != null)
                    {
                        CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    CookieManager.Instance.RemoveAllCookie();
                    await AzureService.DefaultManager.CurrentClient.LogoutAsync();
                    CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug(TAG, "This device is not supported");
                    Finish();
                }
                return false;
            }

            Log.Debug(TAG, "Google Play Services is available.");
            return true;
        }
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                return;

            var channelName = CHANNEL_ID;
            var channelDescription = string.Empty;
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", (sender, args) => { });
            builder.Create().Show();
        }
    }

    
}