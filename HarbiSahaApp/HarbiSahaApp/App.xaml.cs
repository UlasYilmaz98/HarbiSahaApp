
using HarbiSahaApp.IndexTabbedPages;
using Plugin.SecureStorage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;

namespace HarbiSahaApp
{
    public partial class App : Application
    {
        public const string SenderID = "866792046638";
        public const string ListenConnectionString = "Endpoint=sb://harbisahan.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=pzotclM3kFHe+J3E79iKd9Er3qdOBgyGA7EMg7NuY/U=";
        public const string NotificationHubName = "HarbiSahaN";
        public const string PackageName = "com.harbisahaapp.android";

        public App()
        {
            InitializeComponent();
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQwNTI0QDMxMzcyZTMyMmUzMG9TcmtBRXJNZVYvZ0hITlM4WjlQT2JUV2dCZGQ1K2QrMyt5aUljV0hjL2s9");
            //MainPage = new NavigationPage(new IndexPageMain());
            //MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Transparent);
            //MainPage = new NavigationPage(new LoginPage());
            //if (CrossSecureStorage.Current.HasKey("loggedUserEmail") && CrossSecureStorage.Current.HasKey("loggedUserPassword"))
            //{
            //    //tests = indexManager.GetIndexElements();
            //    MainPage = new SplashScreen();
            //    MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Transparent);
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new IndexPageMain());
            //    MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Transparent);
            //}
            MainPage = new SplashScreen();

           
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=22e8514f-2a0a-4c36-8f74-0596132d2b01;android=dc8d46cc-ae65-4141-b3b1-52941bf28625;uwp={Your App Secret}", typeof(Analytics), typeof(Crashes),typeof(Push));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
