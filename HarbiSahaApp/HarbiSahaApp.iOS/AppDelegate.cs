using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace HarbiSahaApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags("CollectionView_Experimental");
            //new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();
            global::Xamarin.Forms.Forms.Init();
            //Xamarin.FormsMaps.Init();
            CachedImageRenderer.Init();
            ImageCircleRenderer.Init();
            AppCenter.Start("22e8514f-2a0a-4c36-8f74-0596132d2b01", typeof(Analytics), typeof(Crashes));
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            switch (Xamarin.Forms.Device.Idiom)
            {
                case TargetIdiom.Phone:
                    return UIInterfaceOrientationMask.Portrait;
                case TargetIdiom.Tablet:
                    return UIInterfaceOrientationMask.Portrait;
                default:
                    return UIInterfaceOrientationMask.Portrait;
            }
        }
    }
}
