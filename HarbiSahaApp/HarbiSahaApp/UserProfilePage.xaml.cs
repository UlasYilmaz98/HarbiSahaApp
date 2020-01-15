using HarbiSahaApp.Models;
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
    public partial class UserProfilePage : ContentPage
    {
        User mainUser = new User();
        public UserProfilePage(User user,bool isFromMyTeam)
        {
            InitializeComponent();
            NavigationPage mp = App.Current.MainPage as NavigationPage;
            mp.BarBackgroundColor = Color.White;
            mainUser = user;
            Fill();
        }

        private void Fill()
        {
            //lblPosition.Text = mainUser.Position1;
            lblName.Text = mainUser.Name;
            lblCompletedMatches.Text = mainUser.CountPlayedMathes.ToString();
            lblCity.Text = mainUser.City + " şehrinde yaşıyor";
            lblAge.Text = CalculateAge(mainUser.BirthYear, mainUser.BirthMonth, mainUser.BirthDay).ToString() + " yaşında";
            lblPosition.Text = mainUser.Position1 + " mevkisinde oynamayı tercih ediyor";
            lblTotalPoints.Text = mainUser.OverallPoint.ToString() + " Harbi Puanı var";
            imgProfilePicture.Source = mainUser.PhotoPath;
            //spanCity.Text = mainUser.City;
            //spanAge.Text = CalculateAge(mainUser.BirthYear, mainUser.BirthMonth, mainUser.BirthDay).ToString();
            //spanPosition.Text = mainUser.Position1;
            lblDegree.Text = "Çaylak";
            //lblHarbiSahaPoints.Text = mainUser.OverallPoint.ToString();
            lblAveragePoints.Text = mainUser.OverallStarPoint.ToString();
            lblHeight.Text = mainUser.Height.ToString() + " cm boyunda";
            if (mainUser.isConfirmed == true)
                lblConfirmPhone.Text = "Telefon doğrulaması yapıldı";
            else
            {
                lblConfirmPhone.IsVisible = false;
                imgTickPhone.IsVisible = false;
            }
            if (mainUser.RandomLine3 == "OK")
                lblConfirmEmail.Text = "E-posta doğrulaması yapıldı";
            else
            {
                lblConfirmEmail.IsVisible = false;
                imgTickEmail.IsVisible = false;
            }
            lblAboutMe.Text = mainUser.ProfileMessage;


        }

        private int CalculateAge(int birthYear, int birthMonth, int birthDay)
        {
            DateTime today = DateTime.Now;
            DateTime thatDay = new DateTime(birthYear, birthMonth, birthDay);
            TimeSpan diff = today.Subtract(thatDay);
            return diff.Days / 365;
        }
    }
}