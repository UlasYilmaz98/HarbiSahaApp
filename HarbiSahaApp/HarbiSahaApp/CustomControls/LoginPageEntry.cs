using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HarbiSahaApp.CustomControls
{
    public class LoginPageEntry : Entry
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadiusProperty), typeof(float), typeof(LoginPageEntry), 0f);


        public float CornerRadius
        {
            get
            {
                return (float)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

    }
}
