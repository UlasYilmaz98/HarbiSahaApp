using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
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
    public partial class FieldsPage : ContentPage
    {
        CitiesAndTowns ct = new CitiesAndTowns();
        List<string> cities = new List<string>();
        List<string> towns = new List<string>();
        List<Field> fieldsList = new List<Field>();
        Field field1 = new Field()
        {
            FieldName = "Güral Halı Saha",
            City = "İstanbul",
            District = "Şişli",
            LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
            PhotoPath1 = "https://www.olleyy.com/api/Foto/2356",
        };
        Field field2 = new Field()
        {
            FieldName = "Mahmut Şevket Paşa Halı Saha",
            City = "İstanbul",
            District = "Şişli",
            LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
            PhotoPath1 = "https://www.olleyy.com/api/Foto/2356",
        };
        Field field3 = new Field()
        {
            FieldName = "Spontane Halı Saha",
            City = "İstanbul",
            District = "Şişli",
            LocationPath = "https://goo.gl/maps/StqDDFqBYmHwfct78",
            PhotoPath1 = "https://www.olleyy.com/api/Foto/2356",
        };
        

        public FieldsPage()
        {
            InitializeComponent();
            cities = ct.Cities;
            pickerCities.ItemsSource = cities;


        }

        private void PickerCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            towns.Clear();
            pickerTowns.ItemsSource = null;
            switch (pickerCities.SelectedItem as string)
            {
                case "İstanbul": towns = ct.IstanbulTowns;break;
                case "İzmir": towns = ct.IstanbulTowns;break;
                default: towns.Add("Hepsi");break;
            }
            pickerTowns.ItemsSource = towns;
        }
    }
}