using HarbiSahaApp.Models;
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

namespace HarbiSahaApp.OpponentAdvertCreatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOpponentAdvertStep1 : ContentPage
    {
        List<string> citiesList = new List<string>();

        CitiesAndTowns modelClassCT = new CitiesAndTowns();
        List<SelectableTown> selectableTowns = new List<SelectableTown>();
        //FilterModelPlayerAdvert mainModel = new FilterModelPlayerAdvert();
        List<string> selectedTowns = new List<string>();
        string SelectedCity;
        PlayerAdvertsServices serviceAdverts = new PlayerAdvertsServices();
        List<PlayerAdvert> filteredAdverts = new List<PlayerAdvert>();
        CreateOpponentAdvertViewModel model = new CreateOpponentAdvertViewModel();


        public CreateOpponentAdvertStep1()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;

            citiesList = modelClassCT.Cities;
            layoutButtons.IsVisible = false;
            pickerCities.ItemsSource = citiesList;

        }

        private void PickerCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectableTowns != null)
                selectableTowns.Clear();
            if (pickerCities.SelectedItem != null)
            {
                SelectedCity = pickerCities.SelectedItem.ToString();

                switch (SelectedCity)
                {
                    case "İstanbul": FillTowns("İstanbul"); break;
                    case "İzmir": FillTowns("İzmir"); break;
                    default: FillTowns(SelectedCity); break;

                }
            }
            lstTowns.ItemsSource = null;
            lstTowns.ItemsSource = selectableTowns;
            if (selectableTowns.Count > 0)
                modelClassCT.Towns = selectableTowns;
            layoutDistrictMain.IsVisible = true;
            layoutButtons.IsVisible = true;
        }

        private void FillTowns(string cityName)
        {
            if (cityName == "İstanbul")
            {
                foreach (string townName in modelClassCT.IstanbulTowns)
                {
                    SelectableTown town = new SelectableTown() { Name = townName, isSelected = false };
                    selectableTowns.Add(town);
                }
            }
            else if (cityName == "İzmir")
            {
                foreach (string townName in modelClassCT.IzmirTowns)
                {
                    SelectableTown town = new SelectableTown() { Name = townName, isSelected = false };
                    selectableTowns.Add(town);
                }
            }
            else
            {
                SelectableTown common = new SelectableTown() { Name = "Tüm İlçeler", isSelected = true };
                selectableTowns.Add(common);
            }

        }

        private void LstTowns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstTowns.SelectedItem != null)
            {
                lstTowns.SelectedItem = null;
            }
        }

        private async void BtnForward_Clicked(object sender, EventArgs e)
        {
            if( pickerCities.SelectedItem == null )
            {
                await DisplayAlert("UYARI", "Şehir seçilmelidir", "OK");
            }
            else
            {
                model.City = SelectedCity;
                if (selectedTowns.Count == 0)
                {
                    model.District1 = "Tüm İlçeler";
                    model.District2 = "NO";
                    model.District3 = "NO";
                    model.District4 = "NO";
                }
                else if (selectedTowns.Contains("Tüm İlçeler"))
                {
                    model.District1 = "Tüm İlçeler";
                    model.District2 = "NO";
                    model.District3 = "NO";
                    model.District4 = "NO";
                }
                else
                {
                    switch (selectedTowns.Count)
                    {
                        case 1:
                            model.District1 = selectedTowns[0]; break;
                        case 2:
                            model.District1 = selectedTowns[0]; model.District2 = selectedTowns[1]; break;
                        case 3:
                            model.District1 = selectedTowns[0]; model.District2 = selectedTowns[1]; model.District3 = selectedTowns[2]; break;
                        case 4:
                            model.District1 = selectedTowns[0]; model.District2 = selectedTowns[1]; model.District3 = selectedTowns[2]; model.District4 = selectedTowns[3]; break;
                        default:
                            model.District1 = selectedTowns[0]; model.District2 = selectedTowns[1]; model.District3 = selectedTowns[2]; model.District4 = selectedTowns[3]; break;
                    }
                }
                //MessagingCenter.Send(this, "MainCreatingOpponentAdvertPage", model);
                await Navigation.PushAsync(new CreateOpponentAdvertStep2(model));
            }

           

        }
    }
}