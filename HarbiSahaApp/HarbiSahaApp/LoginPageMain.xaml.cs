using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ControlModels;
using HarbiSahaApp.ServiceController;
using HarbiSahaApp.ValidationControls;
using Plugin.SecureStorage;
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
    public partial class LoginPageMain : TabbedPage
    {
        ManageServices service = new ManageServices();
        ActivityControl activityControl = new ActivityControl();
        bool isButtonClicked;
        public LoginPageMain()
        {
            InitializeComponent();
           
            //BarBackgroundColor = Color.Transparent;
            //BarTextColor = Color.DeepSkyBlue;            
            //this.SetValue(NavigationPage.BarBackgroundColorProperty, Color.DeepSkyBlue);


        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            activityControl.MakeVisible(ActivityLayout, activity);
            btnLogin.IsEnabled = false;
            User logonUser = new User();
            string email = entryEmail.Text;
            string password = entryPassword.Text;
            LoginValidation validate = new LoginValidation();
            if ( validate.ReturnSituation(entryEmail.Text,entryPassword.Text) != "Success")
            {
                activityControl.MakeUnVisible(ActivityLayout, activity);
                btnLogin.IsEnabled = true;
                await DisplayAlert("Uyarı", validate.ReturnSituation(entryEmail.Text, entryPassword.Text), "OK");
                
            }
            else
            {
                try
                {
                    logonUser = await service.Login(email, password);
                    if (logonUser != null)
                    {
                        CrossSecureStorage.Current.SetValue("loggedUserId", logonUser.Id.ToString());
                        CrossSecureStorage.Current.SetValue("loggedUserEmail", logonUser.Email);
                        CrossSecureStorage.Current.SetValue("loggedUserPassword", logonUser.Password);
                        Application.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else
                    {
                        activityControl.MakeUnVisible(ActivityLayout, activity);
                        btnLogin.IsEnabled = true;
                        await DisplayAlert("Uyarı", "Kullanıcı adı veya şifre hatalı", "OK");
                    }
                }
                catch (Exception ex)
                {
                    activityControl.MakeUnVisible(ActivityLayout, activity);
                    btnLogin.IsEnabled = true;
                    await DisplayAlert("Uyarı", "Kullanıcı adı veya şifre hatalı", "OK");
                }

            }
            
           
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new NavigationPage(new IndexPageMain());
            return base.OnBackButtonPressed();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.FromHex("#2dbefc");
            //page.BarTextColor = Color.White;

            this.BarBackgroundColor = Color.FromHex("#2dbefc");
            //this.BarTextColor = Color.White;
        }


    }
}