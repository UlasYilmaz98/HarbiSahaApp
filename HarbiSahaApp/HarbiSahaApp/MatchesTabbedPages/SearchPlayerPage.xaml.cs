using HarbiSahaApp.CustomControls;
using HarbiSahaApp.PlayerAdvertCreatingPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.MatchesTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPlayerPage : ContentPage
    {
        public SearchPlayerPage()
        {
            InitializeComponent();
            if ( DeviceControl.isTablet())
            {
                btnFindPlayer.FontSize = 30;
                btnMyAdverts.FontSize = 30;
                btnFindPlayer.HeightRequest = 60;
                btnMyAdverts.HeightRequest = 60;
            }
            //this.BackgroundImageSource = ImageSource.FromResource("soccer_green_bg.jpg");
        }

        private async void BtnFindPlayer_Clicked(object sender, EventArgs e)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
               await Navigation.PushAsync(new CreatePlayerAdvertPage1());
            }
            else
            {
                await Navigation.PushAsync(new LoginPageMain());
            }
        }

        private async void BtnMyAdverts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyAdvertsPage());
        }
    }
}