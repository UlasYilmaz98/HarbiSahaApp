using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HarbiSahaApp.CustomControls;
using HarbiSahaApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPageEntry), typeof(AndroidLoginPageEntryRenderer))]
namespace HarbiSahaApp.Droid.CustomRenderers
{
    public class AndroidLoginPageEntryRenderer : EntryRenderer
    {
        public AndroidLoginPageEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.RoundCornerButton);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(50, 0, 0, 0);
            }
        }
    }


}
