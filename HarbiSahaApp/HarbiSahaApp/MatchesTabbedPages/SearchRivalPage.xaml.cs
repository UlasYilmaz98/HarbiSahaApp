using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ControlModels;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.OpponentAdvertCreatingPages;
using HarbiSahaApp.ServiceController;
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
    public partial class SearchRivalPage : ContentPage
    {
        CitiesAndTowns ct = new CitiesAndTowns();
        List<string> cities = new List<string>();
        List<string> towns = new List<string>();
        List<OpponentAdvertMenuViewModel> viewModels = new List<OpponentAdvertMenuViewModel>();
        List<OpponentAdvert> adverts = new List<OpponentAdvert>();
        PlayerAdvertsServices serviceAdvert = new PlayerAdvertsServices();
        public SearchRivalPage()
        {
            InitializeComponent();
         
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                if ( App.Current.Properties.ContainsKey("loggedUserTeamPlayer") )
                {
                    
                    if (App.Current.Properties["loggedUserTeamPlayer"] != null)
                    {
                        TeamPlayer currentUserTeamPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                        btnCreateAdvert.Text = "RAKİP İLANI VER";
                        lblMyTeamName.Text = currentUserTeamPlayer.Team.Name;
                    }
                    else
                    {
                        btnCreateAdvert.Text = "HEMEN TAKIN KUR";
                        lblMyTeamName.Text = "Henüz takımınız yok.";
                    }
                }
                
            }
            else
            {
                btnCreateAdvert.Text = "HEMEN ÜYE OL";
                lblMyTeamName.Text = "Üye olun,takımınızı kurun ve harbisahada hemen maçlara başlayın.";
            }
            cities = ct.Cities;
            //pickerCities.ItemsSource = cities;
            ActivityControl ctrl = new ActivityControl();
            ctrl.MakeVisible(ActivityLayout, activity);
            //adverts = 
            adverts = await serviceAdvert.GetMainOpponentAdverts(6);
            if ( adverts != null)
            {
                foreach (OpponentAdvert adv in adverts)
                {
                    viewModels.Add(new OpponentAdvertMenuViewModel() { Advert = adv });
                }
               
            }
            ctrl.MakeUnVisible(ActivityLayout, activity);
            lstOpponentAdverts.ItemsSource = viewModels;
        }

        private void PickerCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            //towns.Clear();
            //pickerTowns.ItemsSource = null;
            //switch (pickerCities.SelectedItem as string)
            //{
            //    case "İstanbul": towns = ct.IstanbulTowns; break;
            //    case "İzmir": towns = ct.IstanbulTowns; break;
            //    default: towns.Add("Tüm İlçeler"); break;
            //}
            //pickerTowns.ItemsSource = towns;
        }

        private async void LstOpponentAdverts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( lstOpponentAdverts.SelectedItem != null)
            {
                OpponentAdvertMenuViewModel model = new OpponentAdvertMenuViewModel();
                model = lstOpponentAdverts.SelectedItem as OpponentAdvertMenuViewModel;
                await Navigation.PushAsync(new OpponentAdvertDetailsPage(model));
            }
        }

        private async void BtnShowAll_Clicked(object sender, EventArgs e)
        {
            //if (App.Current.Properties.ContainsKey("loggedUser"))
            //{

            //}
            //else
            //{
            //    await Navigation.PushAsync(new LoginPageMain());
            //}
            await Navigation.PushAsync(new OpponentAdvertsDetalFilterPage());

        }

        private async void BtnCreateAdvert_Clicked(object sender, EventArgs e)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                if (App.Current.Properties.ContainsKey("loggedUserTeamPlayer"))
                {

                    if (App.Current.Properties["loggedUserTeamPlayer"] != null)
                    {
                        await Navigation.PushAsync(new CreateOpponentAdvertStep1());
                    }
                    else
                    {
                        await Navigation.PushAsync(new CreateTeamPage());
                    }
                }

            }
            else
            {
                //btnCreateAdvert.Text = "HEMEN ÜYE OL";
                //lblMyTeamName.Text = "Üye olun,takımınızı kurun ve harbisahada hemen maçlara başlayın.";
                await Navigation.PushAsync(new LoginPageMain());
            }
        }
    }
}