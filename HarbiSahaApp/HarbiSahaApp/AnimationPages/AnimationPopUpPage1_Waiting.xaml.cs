using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.AnimationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPopUpPage1_Waiting : Rg.Plugins.Popup.Pages.PopupPage
    {
        double countSec = 0.75;
        List<string> sourcesList = new List<string>() {"hsAnimationLogo1.png", "hsAnimationLogo2.png", "hsAnimationLogo3.png",
            "hsAnimationLogo4.png","hsAnimationLogo5.png","hsAnimationLogo6.png" };
        int sourceNumber = 0;
        string messageMain;
        public AnimationPopUpPage1_Waiting( string message )
        {
            InitializeComponent();
            //messageMain = message;
            lblMsg.Text = message;
            
        }

        private async Task ChangeIt()
        {

            uint transitionTime = 600;
            double displacement = image.Width;

            await Task.WhenAll(
                image.FadeTo(0, transitionTime, Easing.Linear),
                image.TranslateTo(-displacement, image.Y, transitionTime, Easing.CubicInOut));

            // Changes image source.
            image.Source = ImageSource.FromFile("Icon");

            await image.TranslateTo(displacement, 0, 0);
            await Task.WhenAll(
                image.FadeTo(1, transitionTime, Easing.Linear),
                image.TranslateTo(0, image.Y, transitionTime, Easing.CubicInOut));
        }

        private async Task<int> ChangeImage(int _sourceNumber)
        {
            image.Source = sourcesList[sourceNumber];
            return 1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TimerTick();
        }

        private  void TimerTick()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                if (countSec <= 0)
                {
                    if (sourceNumber == 5)
                        sourceNumber = 0;
                    else
                        sourceNumber++;
                    Task<int> b;
                    b = ChangeImage(sourceNumber);
                    countSec = 0.75;


                    //return false;
                }
                else if (countSec > 0)
                {
                    countSec -= 0.25;
                    
                }


                return true;

            });

        }
    }
}