using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.PlayerAdvertCreatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlayerAdvertPage1 : ContentPage
    {
        CitiesAndTowns modelCT = new CitiesAndTowns();
        List<AutoCompleteViewModel> cityList = new List<AutoCompleteViewModel>();
        List<AutoCompleteViewModel> townList = new List<AutoCompleteViewModel>();
        public bool isControlOnCity = true;
        CreatePlayerAdvertViewModel advertModel = new CreatePlayerAdvertViewModel();
        //string position1, position2, position3;
        bool finishingControl = false;
        PlayerAdvertsServices advertsServices = new PlayerAdvertsServices();

        public CreatePlayerAdvertPage1()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;

            //cityListString = modelCT.Cities;
            foreach (string cityName in modelCT.Cities)
            {
                AutoCompleteViewModel vm = new AutoCompleteViewModel() { Name = cityName };
                cityList.Add(vm);
            }
            cityList.RemoveAt(0);
            cityList.RemoveAt(1);
        }

        public void FillTowns(string cityName)
        {
            townList.Clear();

            if (cityName.ToLower() == "istanbul")
            {
                foreach (string townName in modelCT.IstanbulTowns)
                {
                    AutoCompleteViewModel vm = new AutoCompleteViewModel() { Name = townName };
                    townList.Add(vm);
                }
            }
            else if (cityName.ToLower() == "izmir")
            {
                foreach (string townName in modelCT.IzmirTowns)
                {
                    AutoCompleteViewModel vm = new AutoCompleteViewModel() { Name = townName };
                    townList.Add(vm);
                }
            }


        }

        private void EntryCity_TextChanged(object sender, TextChangedEventArgs e)
        {

            lstTowns.IsVisible = false;
            lstCities.IsVisible = true;

            if (entryCity.Text.Count() == 0)
            {
                lstCities.ItemsSource = null;
            }
            else if (entryCity.Text.Count() == 1)
            {
                char first = entryCity.Text.ToLower().ToString()[0];
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower()[0] == first).Take(4).ToList();
            }
            else if (entryCity.Text.Count() == 2)
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Substring(0, 2) == entryCity.Text.ToLower().Substring(0, 2)).Take(4).ToList();
            }
            else if (entryCity.Text.Count() == 3)
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Substring(0, 3) == entryCity.Text.ToLower().Substring(0, 3)).Take(4).ToList();
            }
            else
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Contains(entryCity.Text.ToString().ToLower())).ToList();
            }

            //lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Contains(entryCity.Text.ToString().ToLower())).ToList();
        }

        private void LstCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstCities.SelectedItem != null)
            {
                AutoCompleteViewModel vmCity = lstCities.SelectedItem as AutoCompleteViewModel;

                entryCity.Text = vmCity.Name;

                lstCities.SelectedItem = null;

                lstCities.ItemsSource = null;

                FillTowns(vmCity.Name);

            }
        }

        private void EntryTown_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstCities.IsVisible = false;
            lstTowns.IsVisible = true;

            if (entryTown.Text.Count() == 0)
            {
                lstTowns.ItemsSource = null;
            }
            else if (entryTown.Text.Count() == 1)
            {
                char first = entryTown.Text.ToLower().ToString()[0];
                lstTowns.ItemsSource = townList.Where(x => x.Name.ToLower()[0] == first).Take(4).ToList();
            }
            else if (entryTown.Text.Count() == 2)
            {
                lstTowns.ItemsSource = townList.Where(x => x.Name.ToLower().Substring(0, 2) == entryTown.Text.ToLower().Substring(0, 2)).Take(4).ToList();
            }
            else if (entryTown.Text.Count() == 3)
            {
                lstTowns.ItemsSource = townList.Where(x => x.Name.ToLower().Substring(0, 3) == entryTown.Text.ToLower().Substring(0, 3)).Take(4).ToList();
            }
            else
            {
                lstTowns.ItemsSource = townList.Where(x => x.Name.ToLower().Contains(entryTown.Text.ToString().ToLower())).ToList();
            }

        }

        private void LstTowns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstTowns.SelectedItem != null)
            {
                AutoCompleteViewModel vmTown = lstTowns.SelectedItem as AutoCompleteViewModel;

                entryTown.Text = vmTown.Name;

                lstTowns.SelectedItem = null;

                lstTowns.ItemsSource = null;

                //FillTowns(vmTown.Name);

            }
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {

            btnNext.IsEnabled = false;
            if (entryCity.Text == null || entryCity.Text == " " || entryTown.Text == null || entryTown.Text == " ")
            {
                await DisplayAlert("HATA", "Şehir ve ilçe alanları doldurulmalıdır.", "OK");

            }
            else
            {
                advertModel.City = entryCity.Text.Substring(0, 1).ToUpper() + entryCity.Text.Substring(1).ToLower();
                advertModel.Town = entryTown.Text.Substring(0, 1).ToUpper() + entryTown.Text.Substring(1).ToLower();
                btnNext.IsVisible = false;
                layoutStep1.IsVisible = false;
                await Navigation.PushAsync(new CreatePlayerAdvertPage2(advertModel));
                //layoutStep2.IsVisible = true;
                //stepNumber++;
            }
            btnNext.IsVisible = true;
            btnNext.IsEnabled = true;
        }
    }
}