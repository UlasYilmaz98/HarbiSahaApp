using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldDetailPage : ContentPage
    {
        Field mainField = new Field();
        FielDetailPageViewModel vm = new FielDetailPageViewModel();
        List<FieldDetailCommentsViewModel> commentsVmList = new List<FieldDetailCommentsViewModel>();

        public FieldDetailPage(Field field)
        {
            InitializeComponent();
            mainField = field;
            //mainField.FieldName = "Nihat Yüceur Spor Kompleksi";
            //mainField.FieldInformation = "Sefaköy'ün merkezinde metrobüse 15 dakika uzaklıkta olan tesisimizde" +
            //    " 2 adet halı sahamız bulunmaktadır.Sabah 09.00 ile 23.59 arası açığız.Cafe,langırt masası ve gym salonumuz " +
            //    "mevcut olup soyunma odalarımızda sıcak su seçeneğimiz bulunmaktadır.Siz değerli sporseverleri tesisimizde keyifli bir" +
            //    " maç için bekliyoruz.";
            //mainField.BaseAdress = " Kemalpaşa, Özmen Sk. No:13, 34295 Küçükçekmece/İstanbul";
            //mainField.City = "İstanbul";
            //mainField.District = "Bakırköy";
            //mainField.haveCafe = true;
            //mainField.haveCamera = true;
            //mainField.haveCarPark = true;
            //mainField.haveCreditCard = false;
            //mainField.haveFood = true;
            //mainField.haveGloves = true;
            //mainField.haveInternet = false;
            //mainField.haveShoes = true;
            //mainField.haveShower = true;
            //mainField.haveTribun = false;
            //mainField.haveWC = true;
            //mainField.LocationPath = "https://www.google.com/maps/@40.9987867,28.7851176,15.29z";
            //mainField.RandomLine5 = "40.9987867";  //LONG
            //mainField.RandomLine6 = "28.7851176";  // lat

            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.White;
            //mainField = field;
            mainField.RateFormFields = field.RateFormFields;
            mainField.RateFormFields.OrderByDescending(x => x.CreatedOn);
            vm.Field = mainField;

            foreach( RateFormField frm in vm.Field.RateFormFields)
            {
                commentsVmList.Add(new FieldDetailCommentsViewModel() { Form = frm });
            }
            
            
            FillStars();
            FillEntries();
            FillIcons();
            //GenerateMap();
        }

        //private void GenerateMap()
        //{
        //    var map = new Xamarin.Forms.Maps.Map(
        //    MapSpan.FromCenterAndRadius(
        //            new Position(40.9987867, 28.7851176), Distance.FromMiles(0.3)))
        //    {
        //        IsShowingUser = false,
        //        HorizontalOptions = LayoutOptions.FillAndExpand,
        //        HeightRequest = 200,
        //    };
        //    layoutMaps.Children.Add(map);
        //    string latStr = vm.Field.RandomLine5;
        //    string longStr = vm.Field.RandomLine6;
        //    latStr = latStr.Replace(".", ",");
        //    longStr = longStr.Replace(".", ",");
        //    Double lat = Convert.ToDouble(latStr);
        //    Double _long = Convert.ToDouble(longStr);
        //    var position = new Position(lat,_long); // Latitude, Longitude
        //    var pin = new Pin
        //    {
        //        Type = PinType.Place,
        //        Position = position,
        //        Label = vm.Field.FieldName,
        //        Address = vm.Field.BaseAdress
        //    };
        //    map.Pins.Add(pin);
        //}

        private void FillIcons()
        {
            if( vm.Field.haveCafe == false)
            {
                lblCafe.Opacity = 0.3;
                imgCafe.Opacity = 0.3;
            }
            if (vm.Field.haveCamera == false)
            {
                lblCamera.Opacity = 0.3;
                imgCamera.Opacity = 0.3;
            }
            if (vm.Field.haveCarPark == false)
            {
                lblCar.Opacity = 0.3;
                imgCar.Opacity = 0.3;
            }
            if (vm.Field.haveCarPark == false)
            {
                lblCar.Opacity = 0.3;
                imgCar.Opacity = 0.3;
            }
            if (vm.Field.haveCreditCard == false)
            {
                lblCreditCard.Opacity = 0.3;
                imgCreditCard.Opacity = 0.3;
            }
            if (vm.Field.haveFood == false)
            {
                lblFood.Opacity = 0.3;
                imgFood.Opacity = 0.3;
            }
            if (vm.Field.haveGloves == false)
            {
                lblGloves.Opacity = 0.3;
                imgGloves.Opacity = 0.3;
            }
            if (vm.Field.haveInternet == false)
            {
                lblWifi.Opacity = 0.3;
                imgWifi.Opacity = 0.3;
            }
            if (vm.Field.haveShoes == false)
            {
                lblShoes.Opacity = 0.3;
                imgShoes.Opacity = 0.3;
            }
            if (vm.Field.haveShower == false)
            {
                lblShower.Opacity = 0.3;
                imgShower.Opacity = 0.3;
            }
            if (vm.Field.haveTribun == false)
            {
                lblGrandStand.Opacity = 0.3;
                imgGrandStand.Opacity = 0.3;
            }

        }

        private void FillEntries()
        {
            lblFieldName.Text = vm.Field.FieldName;
            lblPoint.Text = vm.PointsOverFive;
            lblTownAndCity.Text = vm.CityAndTown;
            collectionViewComments.ItemsSource = commentsVmList;
            lblInfo.Text = vm.Field.FieldInformation;
            
        }

        private void FillStars()
        {
            if ( vm.OverallPoint < 1.25 )
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "emptyStar.png";
                imgStar3.Source = "emptyStar.png";
                imgStar4.Source = "emptyStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if ( vm.OverallPoint < 1.75)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "halfStar.png";
                imgStar3.Source = "emptyStar.png";
                imgStar4.Source = "emptyStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if ( vm.OverallPoint < 2.25)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "emptyStar.png";
                imgStar4.Source = "emptyStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if (vm.OverallPoint < 2.75)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "halfStar.png";
                imgStar4.Source = "emptyStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if (vm.OverallPoint < 3.25)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "fullStar.png";
                imgStar4.Source = "emptyStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if (vm.OverallPoint < 3.75)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "fullStar.png";
                imgStar4.Source = "halfStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if (vm.OverallPoint < 4.25)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "fullStar.png";
                imgStar4.Source = "fullStar.png";
                imgStar5.Source = "emptyStar.png";
            }
            else if (vm.OverallPoint < 4.75)
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "fullStar.png";
                imgStar4.Source = "fullStar.png";
                imgStar5.Source = "halfStar.png";
            }
            else
            {
                imgStar1.Source = "fullStar.png";
                imgStar2.Source = "fullStar.png";
                imgStar3.Source = "fullStar.png";
                imgStar4.Source = "fullStar.png";
                imgStar5.Source = "fullStar.png";
            }

        }

        private async void BtnLookTimes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FieldTimesPage(mainField));
        }
    }
}