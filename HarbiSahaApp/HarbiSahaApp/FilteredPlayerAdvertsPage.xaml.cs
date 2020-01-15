using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilteredPlayerAdvertsPage : ContentPage
    {
        List<PlayerAdvert> mainAdverts = new List<PlayerAdvert>();
        List<PlayerAdvertMenuViewModel> viewModels = new List<PlayerAdvertMenuViewModel>();
        

        public FilteredPlayerAdvertsPage(List<PlayerAdvert> adverts)
        {
            InitializeComponent();
            mainAdverts = adverts;
            if ( adverts.Count == 0 || adverts == null )
            {
                lblSituationText.Text = "Aradığınız kriterlere uygun maç bulunmuyor.";
            }
            else
            {
                lblSituationText.Text = "Aradığınız kriterlere uygun maçları listeledik.";
                Fill();
            }
        }

        private void Fill()
        {
            foreach (PlayerAdvert adv in mainAdverts)
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
                    Cost = adv.Neighborhood + " ₺",

                });
            }
            colSearchPlayers.ItemsSource = viewModels;
        }

        private async void ColSearchPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( colSearchPlayers.SelectedItem != null )
            {
                if( App.Current.Properties.ContainsKey("loggedUser") )
                {
                    PlayerAdvertMenuViewModel model = colSearchPlayers.SelectedItem as PlayerAdvertMenuViewModel;
                    PlayerAdvert advert = model.Advert;
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(advert));
                }
                else
                {
                    await Navigation.PushAsync(new LoginPageMain());
                }
                
                colSearchPlayers.SelectedItem = null;
            }
        }
    }
}