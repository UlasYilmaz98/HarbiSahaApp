using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarbiSahaApp.ServiceController;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlayerAdvertPage : ContentPage
    {
        public int stepNumber = 1;
        CitiesAndTowns modelCT = new CitiesAndTowns();
        List<AutoCompleteViewModel> cityList = new List<AutoCompleteViewModel>();
        List<AutoCompleteViewModel> townList = new List<AutoCompleteViewModel>();
        public bool isControlOnCity = true;
        CreatePlayerAdvertViewModel advert = new CreatePlayerAdvertViewModel();
        //string position1, position2, position3;
        bool finishingControl = false;
        PlayerAdvertsServices advertsServices = new PlayerAdvertsServices();

        //List<string> cityListString = new List<string>();


        public CreatePlayerAdvertPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;

            //cityListString = modelCT.Cities;
            foreach ( string cityName in modelCT.Cities)
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

            if ( cityName.ToLower() == "istanbul")
            {
                foreach ( string townName in modelCT.IstanbulTowns )
                {
                    AutoCompleteViewModel vm = new AutoCompleteViewModel() { Name = townName};
                    townList.Add(vm);
                }
            }
            else if ( cityName.ToLower() == "izmir")
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

            if( entryCity.Text.Count() == 0)
            {
                lstCities.ItemsSource = null;
            }
            else if ( entryCity.Text.Count() == 1)
            {
                char first = entryCity.Text.ToLower().ToString()[0];
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower()[0] == first).Take(4).ToList();
            }
            else if ( entryCity.Text.Count() == 2 )
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Substring(0,2) == entryCity.Text.ToLower().Substring(0,2)).Take(4).ToList();
            }
            else if (entryCity.Text.Count() == 3)
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Substring(0,3) == entryCity.Text.ToLower().Substring(0,3)).Take(4).ToList();
            }
            else
            {
                lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Contains(entryCity.Text.ToString().ToLower())).ToList();
            }

            //lstCities.ItemsSource = cityList.Where(x => x.Name.ToLower().Contains(entryCity.Text.ToString().ToLower())).ToList();
        }

        private void LstCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if ( lstCities.SelectedItem != null)
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
            if ( stepNumber == 1)
            {
                if( entryCity.Text == null || entryCity.Text == " " || entryTown.Text == null || entryTown.Text == " ")
                {
                    await DisplayAlert("HATA", "Şehir ve ilçe alanları doldurulmalıdır.", "OK");
                }
                else
                {
                    advert.City = entryCity.Text.Substring(0, 1).ToUpper() + entryCity.Text.Substring(1).ToLower();
                    advert.Town = entryTown.Text.Substring(0, 1).ToUpper() + entryTown.Text.Substring(1).ToLower();
                    btnNext.IsVisible = false;
                    layoutStep1.IsVisible = false;
                    btnNext.IsVisible = true;
                    layoutStep2.IsVisible = true;
                    stepNumber++;
                }              
            }
            else if ( stepNumber == 2)
            {
                advert.FieldName = entryFieldName.Text;
                advert.BaseAdress = editorBaseAdress.Text;
                btnNext.IsVisible = false;
                layoutStep2.IsVisible = false;
                btnNext.IsVisible = true;
                //btnNext.Text = "SON ADIMA İLERLE";
                layoutStep3.IsVisible = true;
                //btnNext.Text = "TAMAMLA";
                FillPickers();
                
                stepNumber++;
            }
            else if ( stepNumber == 3 )
            {
                if ( datePickerDate.Date != null && timePickerTime.Time != null )
                {
                    if (framePositionPicker1.IsVisible == true && (positionPicker1.SelectedItem == null || positionPicker1.SelectedItem == " "))
                        await DisplayAlert("UYARI", "Oyuncu1 için mevki seçin", "Tamam");
                    else if (framePositionPicker2.IsVisible == true && (positionPicker2.SelectedItem == null || positionPicker2.SelectedItem == " "))
                        await DisplayAlert("UYARI", "Oyuncu2 için mevki seçin", "Tamam");
                    else if (framePositionPicker3.IsVisible == true && (positionPicker3.SelectedItem == null || positionPicker3.SelectedItem == " "))
                        await DisplayAlert("UYARI", "Oyuncu3 için mevki seçin", "Tamam");
                    else
                    {
                        
                        advert.Day = datePickerDate.Date.Day;
                        advert.Month = datePickerDate.Date.Month;
                        advert.Year = datePickerDate.Date.Year;
                        advert.MatchStart = timePickerTime.Time;
                        advert.NeededPosition1 = positionPicker1.SelectedItem as string;
                        advert.NeededPosition2 = positionPicker2.SelectedItem as string;
                        advert.NeededPosition3 = positionPicker3.SelectedItem as string;
                        layoutStep3.IsVisible = false;
                        btnNext.Text = "TAMAMLA";
                        btnNext.IsVisible = true;
                        layoutStep4.IsVisible = true;
                        

                        //btnNext.Text = "SON ADIMA İLERLE";
                        //layoutStep3.IsVisible = true;
                        
                        stepNumber++;
                        //CreatePlayerAdvertViewModel _advertmodel = new CreatePlayerAdvertViewModel();
                        //_advertmodel = await advertsServices.CreatePlayerAdvert(advert);
                        //await Navigation.PopPopupAsync();
                        //if ( _advertmodel != null )
                        //{
                        //    App.Current.MainPage = new NavigationPage(new IndexPageMain());
                        //    await DisplayAlert("Başarılı", "Oyuncu ilanınız oluşturuldu! Teşekkür ederiz.", "Tamam");
                            
                        //}
                        
                    }

                    
                }
                else
                {
                    await DisplayAlert("HATA", "Tarih ve Saat bilgisi doldurulmalıdır", "OK");
                }
            }
            else if ( stepNumber == 4)
            {
                await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Oluşturuluyor..."));
                if ( editorAdvertInfos.Text != null)
                {
                    //advert.BaseAdress = editorAdvertInfos.Text;
                    advert.AdvertInfos = editorAdvertInfos.Text;
                }
                btnNext.IsVisible = false;
                CreatePlayerAdvertViewModel _advertmodel = new CreatePlayerAdvertViewModel();
                _advertmodel = await advertsServices.CreatePlayerAdvert(advert);
                await Navigation.PopPopupAsync();
                if (_advertmodel != null)
                {
                    App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    await DisplayAlert("Başarılı", "Oyuncu ilanınız oluşturuldu! Teşekkür ederiz.", "Tamam");

                }
            }

            btnNext.IsEnabled = true;
            
        }

        private void FillPickers()
        {
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            DateTime maxDay = DateTime.Today.AddMonths(6);
            datePickerDate.MaximumDate = maxDay;
            positionPicker1.ItemsSource = new List<string>() { "Tüm Mevkiler","Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            positionPicker2.ItemsSource = new List<string>() { "Tüm Mevkiler", "Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            positionPicker3.ItemsSource = new List<string>() { "Tüm Mevkiler", "Kaleci", "Defans", "Orta Saha", "Hücumcu" };

        }

        private void EntryFieldName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnAddPlayerAdvert_Clicked(object sender, EventArgs e)
        {
            if ( framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == false)
            {
                //position1 = positionPicker1.SelectedItem as string;
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = true;
            }
            else if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true)
            {
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = true;
                framePositionLabel3.IsVisible = true;
                frameAddPlayerAdvert.IsVisible = false;
                frameRemovePlayerAdvert.IsVisible = true;
            }
        }

        private void BtnRemovePlayerAdvert_Clicked(object sender, EventArgs e)
        {
            if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true && framePositionPicker3.IsVisible == true)
            {
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = true;
            }
            else if(framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true && framePositionPicker3.IsVisible == false)
            {
                framePositionPicker2.IsVisible = false;
                framePositionLabel2.IsVisible = false;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = false;
            }
        }
    }
}