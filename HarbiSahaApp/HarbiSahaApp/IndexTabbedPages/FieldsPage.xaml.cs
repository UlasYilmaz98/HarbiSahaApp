using HarbiSahaApp.Models;
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
    public partial class FieldsPage : ContentPage
    {
        CitiesAndTowns ct = new CitiesAndTowns();
        List<string> cities = new List<string>();
        List<string> towns = new List<string>();
        List<Field> fieldsList = new List<Field>();
        List<FielDetailPageViewModel> viewModels = new List<FielDetailPageViewModel>();
        ManageMatchServices service = new ManageMatchServices();
        //Field field1 = new Field()
        //{
        //    FieldName = "Harbi Saha Halı Saha",
        //    City = "İstanbul",
        //    District = "Bakırköy",
        //    LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
        //    PhotoPath1 = "https://img-s1.onedio.com/id-54de2dbee7e6b61020f88132/rev-0/raw/s-6d4d05207d22cf5e46a566f5a8bb5337c5283fe4.jpg",
        //    RateFormFields = new List<RateFormField>()
        //    {
        //        new RateFormField(){ Rate = "5" },
        //        new RateFormField(){ Rate = "5" },
        //        new RateFormField(){ Rate = "5" },
        //        new RateFormField(){ Rate = "5" },
                

        //    }
        //};
        //Field field2 = new Field()
        //{
        //    FieldName = "Değirmen Teknoloji Halı Saha",
        //    City = "İstanbul",
        //    District = "Küçükçekmece",
        //    LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
        //    PhotoPath1 = "indexImage3.jpg",
        //    RateFormFields = new List<RateFormField>()
        //    {
        //        new RateFormField(){ Rate = "5" },
        //        new RateFormField(){ Rate = "4" },
        //        new RateFormField(){ Rate = "3" },
        //        new RateFormField(){ Rate = "5" },

        //    }
        //};
        //Field field3 = new Field()
        //{
        //    FieldName = "Spontane Halı Saha",
        //    City = "İstanbul",
        //    District = "Şişli",
        //    LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
        //    PhotoPath1 = "https://seyler.ekstat.com/img/max/800/Q/QDEtr6dBBNLE5OHB-636138529093684633.jpg",
        //    RateFormFields = new List<RateFormField>()
        //    {
        //        new RateFormField(){ Rate = "3" },
        //        new RateFormField(){ Rate = "4" },
        //        new RateFormField(){ Rate = "1" },
        //        new RateFormField(){ Rate = "1" },

        //    }
        //};
        //FieldZone GuralZone = new FieldZone()
        //{
        //    PaymentModel1FullCost = 150,
        //    PaymentModel1SmallCost = 50,
        //};
        //FieldZone SpontaneZone = new FieldZone()
        //{
        //    PaymentModel1FullCost = 220,
        //    PaymentModel1SmallCost = 60,
        //};
        //FieldZone MSPZone = new FieldZone()
        //{
        //    PaymentModel1FullCost = 200,
        //    PaymentModel1SmallCost = 70,

        //};


        public FieldsPage()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            Fill();
            //field1.FieldZones.Add(GuralZone);
            //field2.FieldZones.Add(MSPZone);
            //field3.FieldZones.Add(SpontaneZone);
            //cities = ct.Cities;
            ////pickerCities.ItemsSource = cities;
            //for (int i = 0; i < 4; i++)
            //{
            //    viewModels.Add(new FielDetailPageViewModel() {
            //        Field = field1

            //    });
            //    viewModels.Add(new FielDetailPageViewModel()
            //    {
            //        Field = field2

            //    });
            //    viewModels.Add(new FielDetailPageViewModel()
            //    {
            //        Field = field3

            //    });

               
            //}

           
        }

        private async void Fill()
        {
            await Navigation.PushPopupAsync(new Base1PopupPage("Yükleniyor..."));
            try
            {
                fieldsList = await service.GetFields();
                foreach (Field field in fieldsList)
                {
                    viewModels.Add(new FielDetailPageViewModel()
                    {
                        Field = field

                    });
                    
                }
                lstFields.ItemsSource = viewModels;
            }
            catch (Exception)
            {

                await DisplayAlert("Hata", "Lütfen internet bağlantınızı kontrol edin", "OK");
            }
            await Navigation.PopPopupAsync();
                
           
        }

        //private void PickerCities_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    towns.Clear();
        //    pickerTowns.ItemsSource = null;
        //    switch (pickerCities.SelectedItem as string)
        //    {
        //        case "İstanbul": towns = ct.IstanbulTowns; break;
        //        case "İzmir": towns = ct.IstanbulTowns; break;
        //        default: towns.Add("Tüm İlçeler"); break;
        //    }
        //    pickerTowns.ItemsSource = towns;
        //}

        private async void LstFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( lstFields.SelectedItem != null)
            {
                FielDetailPageViewModel selectedFieldVM = lstFields.SelectedItem as FielDetailPageViewModel;
                Field selectedField = selectedFieldVM.Field;
                await Navigation.PushAsync(new FieldDetailPage(selectedField));

            }
        }
    }
}