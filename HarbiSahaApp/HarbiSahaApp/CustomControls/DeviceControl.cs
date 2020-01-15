using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HarbiSahaApp.CustomControls
{
    public class DeviceControl
    {
        public static bool isTablet()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
