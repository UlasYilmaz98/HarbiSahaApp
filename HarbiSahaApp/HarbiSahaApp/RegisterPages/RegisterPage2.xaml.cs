using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage2 : ContentPage
    {
        RegisterModel mainModel = new RegisterModel();
        List<int> yearsList = new List<int>();
        CitiesAndTowns cityController = new CitiesAndTowns(); 
        List<string> monthsList = new List<string>();
        Dictionary<string, int> monthAndDaysDict = new Dictionary<string, int>()
        {
            {"Ocak",31 },{"Şubat",29},{"Mart",31},{"Nisan",30},{"Mayıs",31},{"Haziran",30},{"Temmuz",31},{"Ağustos",31},
            {"Eylül",30 },{"Ekim",31},{"Kasım",30},{"Aralık",31}
        };
       
        List<int> dayOfMonths = new List<int>();
        public RegisterPage2(RegisterModel model)
        {
            
            InitializeComponent();
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
            mainModel = model;
            


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            for (int i = 1940; i <= DateTime.Now.Year - 10; i++)
                yearsList.Add(i);
            for (int i = 1; i <= 31; i++)
                dayOfMonths.Add(i);
            foreach (KeyValuePair<string,int> pair in monthAndDaysDict)
            {
                monthsList.Add(pair.Key);
            }
            pickerCity.ItemsSource = cityController.Cities;
            pickerDay.ItemsSource = dayOfMonths;
            pickerMonth.ItemsSource = monthsList;
            pickerYear.ItemsSource = yearsList;
            pickerDay.SelectedItem = 1;
            pickerMonth.SelectedItem = "Ocak";
            pickerYear.SelectedItem = DateTime.Now.Year - 20;
            List<string> positionList = new List<string>() { "Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            pickerPosition.ItemsSource = positionList;

        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (!NameValidationControl(entryName.Text, entrySurName.Text))
            {
                await DisplayAlert("UYARI", "Lütfen isim ve soyisim alanlarını kontrol ediniz.", "Tamam");
            }
            else if ( pickerPosition.SelectedItem == null)
            {
                await DisplayAlert("UYARI", "Favori mevki alanı boş geçilemez", "Tamam");
            }
            else if ( pickerCity.SelectedItem == null)
            {
                await DisplayAlert("UYARI", "Şehir alanı boş geçilemez", "Tamam");
            }
            else
            {
                mainModel.City = pickerCity.SelectedItem as string;
                mainModel.Day = Convert.ToInt32(pickerDay.SelectedItem);
                mainModel.Year = Convert.ToInt32(pickerYear.SelectedItem);
                //int selectedIndex = pickerMonth.SelectedIndex;
                mainModel.Month = pickerMonth.SelectedItem as string;
                mainModel.FavoritePosition = pickerPosition.SelectedItem as string;
                mainModel.Name = entryName.Text;
                mainModel.Surname = entrySurName.Text;
                await Navigation.PushAsync(new RegisterPage3(mainModel));
            }
        }

        private void EntryName_Completed(object sender, EventArgs e)
        {
            string toReturn;
            string current = entryName.Text;
            int whiteSpaceIndex;
            if ( !String.IsNullOrWhiteSpace(current))
            {
                if (current.First() == ' ')
                {
                    current.Remove(0, 1);
                }
                if (current.Last() == ' ')
                {
                    current.Remove(current.Length - 1, 1);
                }                               
                if (current.Any(Char.IsWhiteSpace))
                {
                   
                    whiteSpaceIndex = current.IndexOf(' ');
                    current[whiteSpaceIndex + 1].ToString().ToUpper();
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, whiteSpaceIndex - 1) + " " + current.Substring( whiteSpaceIndex +1 );
                }
                else
                {
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, current.Length - 1);
                }
                entryName.Text = toReturn;
                entrySurName.Focus();
            }
        }

        private void PickerMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( pickerMonth.SelectedItem != null)
            {
                List<int> dayCounts = new List<int>();
                int selectedDay = Convert.ToInt32(pickerDay.SelectedItem);
                string selected = pickerMonth.SelectedItem as string;

                for (int i = 1; i <= monthAndDaysDict[selected]; i++)
                    dayCounts.Add(i);
                pickerDay.ItemsSource = null;
                pickerDay.ItemsSource = dayCounts;

               
                if ( selectedDay > monthAndDaysDict[selected])
                {
                    pickerDay.SelectedItem = monthAndDaysDict[selected];
                }
                else
                {
                    pickerDay.SelectedItem = selectedDay;
                }
            }
        }

        private bool NameValidationControl(string name,string surname)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(surname))
                return false;
            else if (name.Any(Char.IsDigit) || surname.Any(Char.IsDigit))
                return false;
            else if (name.Length > 50 || surname.Length > 50)
                return false;
            else
                return true;
        }

        private void EntrySurName_Completed(object sender, EventArgs e)
        {
            string toReturn;
            string current = entrySurName.Text;
            int whiteSpaceIndex;
            if (!String.IsNullOrWhiteSpace(current))
            {
                if (current.First() == ' ')
                {
                    current.Remove(0, 1);
                }
                if (current.Last() == ' ')
                {
                    current.Remove(current.Length - 1, 1);
                }
                if (current.Any(Char.IsWhiteSpace))
                {

                    whiteSpaceIndex = current.IndexOf(' ');
                    current[whiteSpaceIndex + 1].ToString().ToUpper();
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, whiteSpaceIndex - 1) + " " + current.Substring(whiteSpaceIndex + 1);
                }
                else
                {
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, current.Length - 1);
                }
                entrySurName.Text = toReturn;
                //entrySurName.Focus();
            }
        }

        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {
            string toReturn;
            string current = entryName.Text;
            int whiteSpaceIndex;
            if (!String.IsNullOrWhiteSpace(current))
            {
                if (current.First() == ' ')
                {
                    current.Remove(0, 1);
                }
                if (current.Last() == ' ')
                {
                    current.Remove(current.Length - 1, 1);
                }
                if (current.Any(Char.IsWhiteSpace))
                {

                    whiteSpaceIndex = current.IndexOf(' ');
                    current[whiteSpaceIndex + 1].ToString().ToUpper();
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, whiteSpaceIndex - 1) + " " + current.Substring(whiteSpaceIndex + 1);
                }
                else
                {
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, current.Length - 1);
                }
                entryName.Text = toReturn;
                //entrySurName.Focus();
            }
        }

        private void EntrySurName_Unfocused(object sender, FocusEventArgs e)
        {
            string toReturn;
            string current = entrySurName.Text;
            int whiteSpaceIndex;
            if (!String.IsNullOrWhiteSpace(current))
            {
                if ( current.First() == ' ')
                {
                    current.Remove(0,1);
                }
                if (current.Last() == ' ')
                {
                    current.Remove(current.Length - 1, 1);
                }
                if (current.Any(Char.IsWhiteSpace))
                {

                    whiteSpaceIndex = current.IndexOf(' ');
                    current[whiteSpaceIndex + 1].ToString().ToUpper();
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, whiteSpaceIndex - 1) + " " + current.Substring(whiteSpaceIndex + 1);
                }
                else
                {
                    toReturn = current[0].ToString().ToUpper() + current.Substring(1, current.Length - 1);
                }
                entrySurName.Text = toReturn;
                //entrySurName.Focus();
            }
        }
    }
}