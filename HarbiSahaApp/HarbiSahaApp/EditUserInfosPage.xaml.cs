using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
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
    public partial class EditUserInfosPage : ContentPage
    {
        User currentUser = new User();
        List<string> cities = new List<string>();
        CitiesAndTowns CitiesAndTowns = new CitiesAndTowns();
        ManageServices service = new ManageServices();

        List<int> yearsList = new List<int>();
        CitiesAndTowns cityController = new CitiesAndTowns();
        List<string> monthsList = new List<string>();
        Dictionary<string, int> monthAndDaysDict = new Dictionary<string, int>()
        {
            {"Ocak",31 },{"Şubat",29},{"Mart",31},{"Nisan",30},{"Mayıs",31},{"Haziran",30},{"Temmuz",31},{"Ağustos",31},
            {"Eylül",30 },{"Ekim",31},{"Kasım",30},{"Aralık",31}
        };
        List<int> listOfHeights = new List<int>();

        

        List<int> dayOfMonths = new List<int>();
        public EditUserInfosPage()
        {
            InitializeComponent();
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
            navPage.BarTextColor = Color.White;
            currentUser = App.Current.Properties["loggedUser"] as User;
            imgProfilePicture.Source = currentUser.PhotoPath;
            entryName.Text = currentUser.Name;
            entrySurname.Text = currentUser.Surname;
            cities = CitiesAndTowns.Cities;
            pickerCity.ItemsSource = cities;
            pickerCity.SelectedItem = currentUser.City;
            editorInfo.Text = currentUser.ProfileMessage;
         

            for (int i = 1940; i <= DateTime.Now.Year - 10; i++)
                yearsList.Add(i);
            for (int i = 1; i <= 31; i++)
                dayOfMonths.Add(i);
            foreach (KeyValuePair<string, int> pair in monthAndDaysDict)
            {
                monthsList.Add(pair.Key);
            }

            pickerBirthDay.ItemsSource = dayOfMonths;
            pickerBirthMonth.ItemsSource = monthsList;
            pickerBirthYear.ItemsSource = yearsList;
            pickerBirthDay.SelectedItem = currentUser.BirthDay;
            pickerBirthMonth.SelectedItem = monthsList[currentUser.BirthMonth - 1];
            pickerBirthYear.SelectedItem = currentUser.BirthYear;
            int height = currentUser.Height;
            int divided = height % 100;
            if ( divided==0)
            {
                string heightStr = (height / 100).ToString() + "." + "00";
            }
            else if ( divided<10)
            {
                string heightStr = (height / 100).ToString() + "." + "0" + divided.ToString();
            }
            else
            {
                string heightStr = (height / 100).ToString() + "." + divided.ToString();
            }
            List<string> positionList = new List<string>() { "Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            for( int i=140;i<221;i++)
            {
                listOfHeights.Add(i);
            }
            pickerHeight.ItemsSource = listOfHeights;
            pickerPosition.ItemsSource = positionList;
            pickerPosition.SelectedItem = currentUser.Position1;
            //entryHeight.Text = currentUser.Height.ToString();
            pickerHeight.SelectedItem = currentUser.Height;





        }

        private void TapImageChange_Tapped(object sender, EventArgs e)
        {

        }

        private async void BtnFinish_Clicked(object sender, EventArgs e)
        {
            //int realHeight = CalculateHeightInt(entryHeight.Text);
            //bool properHeight = IsProperHeight(entryHeight.Text);
            if (!NameValidationControl(entryName.Text, entrySurname.Text))
            {
                await DisplayAlert("UYARI", "Lütfen isim ve soyisim alanlarını kontrol ediniz.", "Tamam");
            }
            else if (pickerPosition.SelectedItem == null)
            {
                await DisplayAlert("UYARI", "Favori mevki alanı boş geçilemez", "Tamam");
            }
            else if (pickerCity.SelectedItem == null)
            {
                await DisplayAlert("UYARI", "Şehir alanı boş geçilemez", "Tamam");
            }
            else if ( pickerHeight.SelectedItem == null)
            {
                await DisplayAlert("UYARI", "Boy alanı boş geçilemez", "Tamam");
            }
            else if ( editorInfo.Text.Length >= 320)
            {
                await DisplayAlert("UYARI", "Profil Mesajı en fazla 320 karakter içerebilir.Şu anda " + editorInfo.Text.Length.ToString() + " adet karakter içeriyor.", "Tamam");
            }
            else
            {
                await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
                User editedUser = new User();
                editedUser.City = pickerCity.SelectedItem as string;
                editedUser.Name = entryName.Text;
                editedUser.Surname = entrySurname.Text;
                editedUser.Position1 = pickerPosition.SelectedItem as string;
                editedUser.BirthDay = Convert.ToInt32(pickerBirthDay.SelectedItem);
                string month = pickerBirthMonth.SelectedItem as string;
                editedUser.BirthMonth = monthsList.IndexOf(month)+1;
                editedUser.BirthYear = Convert.ToInt32(pickerBirthYear.SelectedItem);
                editedUser.ProfileMessage = editorInfo.Text;
                editedUser.CreatedOn = DateTime.Now;
                editedUser.Password = currentUser.Password;
                editedUser.Email = currentUser.Email;

                int heightInt;
                //string newHeightStr = 
                //heightInt = 
                editedUser.Height = Convert.ToInt32(pickerHeight.SelectedItem);
                editedUser.Id = currentUser.Id;
                //editorInfo.Text = currentUser.ProfileMessage;
                User toReturn = await service.EditUserProfileInfos(editedUser);
                if ( toReturn == null)
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert("HATA", "Bir sorun oluştu.", "OK");
                }
                else
                {
                    SaveMadeChanges(editedUser);
                    await Navigation.PopPopupAsync();
                    await DisplayAlert("BAŞARILI", "Değişikleriniz kaydedildi.", "Tamam");
                }
                

            }
        }

        private async void SaveMadeChanges(User editedUser)
        {
            User mainUser = App.Current.Properties["loggedUser"] as User;
            mainUser.BirthDay = editedUser.BirthDay;

            mainUser.City = pickerCity.SelectedItem as string;
            mainUser.Name = entryName.Text;
            mainUser.Surname = entrySurname.Text;
            mainUser.Position1 = pickerPosition.SelectedItem as string;
            mainUser.BirthDay = Convert.ToInt32(pickerBirthDay.SelectedItem);
            string month = pickerBirthMonth.SelectedItem as string;
            mainUser.BirthMonth = monthsList.IndexOf(month) + 1;
            mainUser.BirthYear = Convert.ToInt32(pickerBirthYear.SelectedItem);
            mainUser.ProfileMessage = editorInfo.Text;
            mainUser.CreatedOn = DateTime.Now;
            mainUser.Password = currentUser.Password;
            mainUser.Email = currentUser.Email;

            int heightInt;
            //string newHeightStr = 
            //heightInt = 
            mainUser.Height = Convert.ToInt32(pickerHeight.SelectedItem);
            App.Current.Properties["loggedUser"] = mainUser;
            await App.Current.SavePropertiesAsync();

        }

        //private bool IsProperHeight(string text)
        //{
        //    int h = Convert.ToInt32(entryHeight.Text);
        //    if (h < 100 || h >= 270)
        //        return false;
        //    return true;
        //}

       

        private void PickerBirthDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerBirthYear.SelectedItem != null || pickerBirthMonth.SelectedItem != null)
            {
                if (pickerBirthMonth.SelectedItem.ToString() == "Şubat" || Convert.ToInt32(pickerBirthDay.SelectedItem) == 29)
                {
                    if (Convert.ToInt32(pickerBirthMonth.SelectedItem) % 4 != 0)
                    {
                        pickerBirthDay.SelectedItem = 28;
                    }
                }
            }
        }

        private void PickerBirthMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerBirthMonth.SelectedItem != null)
            {
                List<int> dayCounts = new List<int>();
                int selectedDay = Convert.ToInt32(pickerBirthDay.SelectedItem);
                string selected = pickerBirthMonth.SelectedItem as string;

                for (int i = 1; i <= monthAndDaysDict[selected]; i++)
                    dayCounts.Add(i);
                pickerBirthDay.ItemsSource = null;
                pickerBirthDay.ItemsSource = dayCounts;


                if (selectedDay > monthAndDaysDict[selected])
                {
                    if ( selected == "Şubat")
                    {
                        pickerBirthDay.SelectedItem = 28;
                    }
                    else
                    {
                        pickerBirthDay.SelectedItem = monthAndDaysDict[selected];
                    }
                    
                }
                else
                {
                    pickerBirthDay.SelectedItem = selectedDay;
                }
            }
            if( pickerBirthYear.SelectedItem != null || pickerBirthDay.SelectedItem != null)
            {                
                if ( pickerBirthMonth.SelectedItem.ToString() == "Şubat" || Convert.ToInt32(pickerBirthDay.SelectedItem) == 29)
                {
                    if( Convert.ToInt32(pickerBirthMonth.SelectedItem) % 4 != 0)
                    {
                        pickerBirthDay.SelectedItem = 28;
                    }
                }
            }
        }

        private void PickerBirthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerBirthDay.SelectedItem != null || pickerBirthMonth.SelectedItem != null)
            {
                if (pickerBirthMonth.SelectedItem.ToString() == "Şubat" || Convert.ToInt32(pickerBirthDay.SelectedItem) == 29)
                {
                    if (Convert.ToInt32(pickerBirthMonth.SelectedItem) % 4 != 0)
                    {
                        pickerBirthDay.SelectedItem = 28;
                    }
                }
            }
        }

        private bool NameValidationControl(string name, string surname)
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

        private async void BtnChangeProfilePicture_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePhoto());
        }
    }
}