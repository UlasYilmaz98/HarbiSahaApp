using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ControlModels;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.PopUpPages;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.IndexTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchesPage : TabbedPage
    {
         
        List<PlayerAdvert> advertsList = new List<PlayerAdvert>();
        List<PlayerAdvertMenuViewModel> viewModels = new List<PlayerAdvertMenuViewModel>();
        PlayerAdvertsServices servicePlayerAdv = new PlayerAdvertsServices();
        ActivityControl activityCtr = new ActivityControl();
        public MatchesPage()
        {
            InitializeComponent();
            CitiesAndTowns modelClass = new CitiesAndTowns();
            BarBackgroundColor = Color.FromHex("#2dbefc");
            
        }

        private async Task FillLists()
        {
            advertsList = await servicePlayerAdv.GetPlayerAdverts();
            if ( advertsList == null)
            {
                await DisplayAlert("Uyarı", "İnternet bağlantınızı kontrol edin.", "Tamam");
            }
            else if( advertsList.Count > 0 )
            {
                
                foreach( PlayerAdvert adv in advertsList)
                {
                    viewModels.Add(new PlayerAdvertMenuViewModel()
                    {
                        Advert = adv,
                        City = adv.City,
                        District = adv.Town,
                        Day = adv.Day,
                        Month = adv.Month,
                        Year = adv.Year,
                        StartOn = adv.MatchTime,
                        Name = adv.User.Name,
                        Position = adv.NeededPosition,
                        ProfilePicturePath = adv.User.PhotoPath,
                        FieldName = adv.FieldName,
                        Cost = adv.Neighborhood + " ₺"

                    });
                }
                Fill();
            }
            
            
        }

        

        private void Fill()
        {
            //colSearchPlayers.ItemsSource = null;
            colSearchPlayers.ItemsSource = viewModels;
            activityCtr.MakeUnVisible(ActivityLayout, activity);
            
            
        }

        private async void ColSearchPlayers_SelectionChanged(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if ( colSearchPlayers.SelectedItem != null)
            {
                if ( App.Current.Properties.ContainsKey("loggedUser"))
                {
                    PlayerAdvert advert = (colSearchPlayers.SelectedItem as PlayerAdvertMenuViewModel).Advert;
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(advert));
                }
                else
                {
                    await Navigation.PushAsync(new LoginPageMain());
                }
               
                colSearchPlayers.SelectedItem = null;

            }

            

        }

        private async void BtnFilter_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushPopupAsync(new FilterPlayerAdvertPopUpPage());
            await Navigation.PushAsync(new FilterPlayerAdvertPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            activityCtr.MakeVisible(ActivityLayout, activity);            
            await FillLists();
            //await Fill();

        }

        private void FillAfterFiltered()
        {
            throw new NotImplementedException();
        }
    }
}