using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.ServiceController;
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
    public partial class SplashScreen : ContentPage
    {

        ManageServices service = new ManageServices();

        public SplashScreen()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //imageWave.HeightRequest = imageWave.Width;
            await Task.Delay(1000);

            try
            {

                if (CrossSecureStorage.Current.HasKey("loggedUserEmail") && CrossSecureStorage.Current.HasKey("loggedUserPassword"))
                {

                    string email = CrossSecureStorage.Current.GetValue("loggedUserEmail");
                    string password = CrossSecureStorage.Current.GetValue("loggedUserPassword");
                    User currentUser = new User();
                    currentUser = await service.Login(email, password);
                    if (currentUser != null)
                    {
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else
                    {
                        CrossSecureStorage.Current.DeleteKey("loggedUserEmail");
                        CrossSecureStorage.Current.DeleteKey("loggedUserPassword");
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new IndexPageMain());
                }



                
                //User realUser = new User();



            }
            catch (Exception)
            {

                CrossSecureStorage.Current.DeleteKey("loggedUserEmail");
                CrossSecureStorage.Current.DeleteKey("loggedUserPassword");
                App.Current.MainPage = new NavigationPage(new IndexPageMain());
            }


        }
    }
}