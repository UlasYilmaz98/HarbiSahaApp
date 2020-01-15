using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ControlModels;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpponentAdvertsDetalFilterPage : ContentPage
    {
        CitiesAndTowns ct = new CitiesAndTowns();
        List<string> cities = new List<string>();
        List<string> towns = new List<string>();
        //List<OpponentAdvertMenuViewModel> viewModels = new List<OpponentAdvertMenuViewModel>();
        List<OpponentAdvert> adverts = new List<OpponentAdvert>();
        //ICollection<OpponentAdvertMenuViewModel> = new ICollection<List<OpponentAdvertMenuViewModel>>();
        IList<OpponentAdvertMenuViewModel> viewModels = new ObservableCollection<OpponentAdvertMenuViewModel>();
        PlayerAdvertsServices serviceAdvert = new PlayerAdvertsServices();
        
        public bool isFiltered = false;
        string nna;
        public OpponentAdvertsDetalFilterPage()
        {
            InitializeComponent();
            nna = "success";
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            datePickerDate.MaximumDate = DateTime.Today.AddDays(90);
            DateTime time = DateTime.Now;
            timePicker1.Time = new TimeSpan(time.Hour, time.Minute,time.Second);
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.White;
            FillList();
        }

        private async void FillList()
        {
            cities = ct.Cities;
            pickerCities.ItemsSource = cities;
            ActivityControl ctrl = new ActivityControl();
            ctrl.MakeVisible(ActivityLayout, activity);
            //adverts = 
            adverts = await serviceAdvert.GetMainOpponentAdverts(12);
            if (adverts != null)
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
            towns.Clear();
            pickerTowns.ItemsSource = null;
            switch (pickerCities.SelectedItem as string)
            {
                case "İstanbul": towns = ct.IstanbulTowns; break;
                case "İzmir": towns = ct.IstanbulTowns; break;
                default: towns.Add("Tüm İlçeler"); break;
            }
            pickerTowns.ItemsSource = towns;
        }

        private void LstOpponentAdverts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {
            lblErrorMessage.IsVisible = false;
            //btnFilter.IsEnabled = false;
            FilterThem();
            //btnFilter.IsEnabled = true;
        }

        private async void FilterThem()
        {
            FilterModelOpponentAdverts filterModel = new FilterModelOpponentAdverts();
            ActivityControl ctrl = new ActivityControl();
            ctrl.MakeVisible(ActivityLayout, activity);
            adverts.Clear();
            viewModels.Clear();
            
            if (pickerCities.SelectedItem == null)
                filterModel.City = "Tüm Şehirler";
            else
            {
                filterModel.City = pickerCities.SelectedItem as string;
            }
            if (pickerTowns.SelectedItem == null )
                filterModel.Town = "Tüm İlçeler";
            else
                filterModel.Town = pickerTowns.SelectedItem as string;
            filterModel.Date = datePickerDate.Date;
            filterModel.Time = timePicker1.Time;
            adverts = await serviceAdvert.GetFilteredOpponentAdverts(filterModel);
            try
            {
                if (adverts == null)
                    lblErrorMessage.IsVisible = true;
                else
                {
                    foreach (OpponentAdvert adv in adverts)
                    {
                        viewModels.Add(new OpponentAdvertMenuViewModel() { Advert = adv });
                    }

                    lstOpponentAdverts.ItemsSource = viewModels;
                    ctrl.MakeUnVisible(ActivityLayout, activity);
                }
            }
            catch (Exception ex)
            {

               await  DisplayAlert("Warn", ex.Message, "OK");
            }
            
        }
    }
}