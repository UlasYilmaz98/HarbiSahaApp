using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
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
    public partial class FilterPlayerAdvertPage : ContentPage
    {
        List<string> citiesList = new List<string>();
        
        CitiesAndTowns modelClassCT = new CitiesAndTowns();
        List<SelectableTown> selectableTowns = new List<SelectableTown>();
        FilterModelPlayerAdvert mainModel = new FilterModelPlayerAdvert();
        List<string> selectedTowns = new List<string>();
        string SelectedCity;
        PlayerAdvertsServices serviceAdverts = new PlayerAdvertsServices();
        List<PlayerAdvert> filteredAdverts = new List<PlayerAdvert>();
        

        public FilterPlayerAdvertPage()
        {
            InitializeComponent();
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.White;
            //page.BarTextColor = Color.White;

            //this.BarBackgroundColor = Color.DeepSkyBlue;
            //this.BarTextColor = Color.White;
            //datePickerDate.Date = DateTime.Now;
            //datePickerDate.Format = "dd/MM/yyyy dddd";
            //datePickerDate.MinimumDate = DateTime.Today;
            citiesList = modelClassCT.Cities;
            layoutButtons.IsVisible = false;
            //foreach( string cityName in citiesList)
            //{
            //    SfAutoCompleteItem item = new SfAutoCompleteItem(cityName, "");
            //    cityItems.Add(item);
            //}

            //autoCompleteCity.ItemsSource = cityItems;
            pickerCities.ItemsSource = citiesList;

        }

        public void OnWaiting()
        {

            layoutDistrictMain.IsVisible = false;
            
            ActivityLayout.IsVisible = true;
            activity.IsVisible = true;
            activity.IsRunning = true;
            //activity.IsFocused = true;
            activity.IsEnabled = true;
        }

        public void FinishWaiting()
        {
            ActivityLayout.IsVisible = false;

            ActivityLayout.IsVisible = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            //activity.IsFocused = true;
            activity.IsEnabled = false;
            layoutDistrictMain.IsVisible = true;
            

        }

        private void PickerCities_SelectedIndexChanged(object sender, EventArgs e)
        {

            OnWaiting();
            if (selectableTowns != null)
                selectableTowns.Clear();
            if ( pickerCities.SelectedItem != null )
            {
                SelectedCity = pickerCities.SelectedItem.ToString();

                switch (SelectedCity)
                {
                    case "İstanbul": FillTowns("İstanbul");break;
                    case "İzmir": FillTowns("İzmir"); break;
                    default: FillTowns(SelectedCity); break;

                }
            }
            lstTowns.ItemsSource = null;
            lstTowns.ItemsSource = selectableTowns;
            if (selectableTowns.Count > 0)
                modelClassCT.Towns = selectableTowns;
            //layoutDistrictMain.IsVisible = true;
            FinishWaiting();
            layoutButtons.IsVisible = true;

        }

        private void FillTowns(string cityName)
        {
            if ( cityName == "İstanbul" )
            {
                foreach ( string townName in modelClassCT.IstanbulTowns )
                {
                    SelectableTown town = new SelectableTown() { Name = townName, isSelected = false };
                    selectableTowns.Add(town);
                }
            }
            else if ( cityName == "İzmir")
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
            if ( lstTowns.SelectedItem != null)
            {
                lstTowns.SelectedItem = null;
            }
        }

        private void BtnForward_Clicked(object sender, EventArgs e)
        {
            if ( selectableTowns.Count > 0 )
            {
                foreach( SelectableTown town in lstTowns.ItemsSource)
                {
                    if (town.isSelected == true)
                        selectedTowns.Add(town.Name);
                }
            }                          
            layoutSection1.IsVisible = false;
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            layoutSection2.IsVisible = true;
            


        }

        private async void BtnFinish_Clicked(object sender, EventArgs e)
        {
            if ( filteredAdverts!= null && filteredAdverts.Count >0 )
                filteredAdverts.Clear();

            mainModel.City = SelectedCity;
            
            mainModel.Day = datePickerDate.Date.Day;
            mainModel.Year = datePickerDate.Date.Year;
            mainModel.Month = datePickerDate.Date.Month;
            if ( timePicker1.Time >= new TimeSpan(23, 59, 0))
            {
                mainModel.MinimumTime = new TimeSpan(23, 59, 0);
                mainModel.MaximumTime = timePicker1.Time;
            }
            else
            {
                mainModel.MinimumTime = timePicker1.Time;
                mainModel.MaximumTime = new TimeSpan(23, 59, 0);
            }
            mainModel.Discricts = selectedTowns;
            await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("İlanlar filtreleniyor..."));
            filteredAdverts = await serviceAdverts.FilterPlayerAdverts(mainModel);    
            if ( mainModel != null)
            {
                
                filteredAdverts = await serviceAdverts.FilterPlayerAdverts(mainModel);
                await Navigation.PushAsync(new FilteredPlayerAdvertsPage(filteredAdverts));
                await Navigation.PopPopupAsync();
            }
            else
            {

            }
        }
    }
}