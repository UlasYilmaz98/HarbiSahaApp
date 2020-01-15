using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.ServiceController;
using Plugin.SecureStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage4 : ContentPage
    {
        string code = String.Empty;
        private int countsec;
        RegisterModel mainModel = new RegisterModel();
        int numberOfSend;
        bool isWarned = false;
        ManageServices service = new ManageServices();
        SmsServices smsService = new SmsServices();

        public RegisterPage4(RegisterModel model)
        {
            
            InitializeComponent();
            code = smsService.GenerateRandomPass(model.PhoneNumber);
            mainModel = model;
            countsec = 240;
            TimerTick();
        }

        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            btnConfirm.IsEnabled = false;
            if (entryCode.Text == code)
            {
                await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor...")); 
                RegisterModel returned = await service.Register(mainModel);
                if ( returned  != null)
                {
                    User _model = new User();
                    _model = await service.Login(returned.Email, returned.Password);
                    if ( _model != null)
                    {
                        await DisplayAlert("BAŞARILI", "Kullanıcı oluşturuldu.Hoşgeldiniz!", "Tamam");
                        CrossSecureStorage.Current.SetValue("loggedUserId", _model.Id.ToString());
                        CrossSecureStorage.Current.SetValue("loggedUserEmail", _model.Email);
                        CrossSecureStorage.Current.SetValue("loggedUserPassword", _model.Password);
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                        await Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await DisplayAlert("UYARI", "Hata oluştu", "OK");
                        await Navigation.PopPopupAsync();
                    }
                    
                }
                else
                {
                    await DisplayAlert("UYARI", "Hata oluştu", "OK");
                    await Navigation.PopPopupAsync();
                }

            }            
            else
            {
                await DisplayAlert("UYARI", "Girilen kod yanlış!", "Tamam");
                btnConfirm.IsEnabled = true;
            }
        }



        private void TimerTick()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (countsec <= 0 && isWarned == false )
                {
                    lblTimer.Text = "Süre Doldu";
                    Task<int> b;
                    b = TimeIsUp();


                    //return false;
                }
                else if (countsec > 0)
                {
                    countsec -= 1;
                    lblTimer.Text = String.Format("Süre : {0}", countsec + " saniye");
                    if (countsec <= 5)
                    {
                        lblTimer.TextColor = Color.Crimson;
                    }
                }


                return true;

            });
        }

        private async Task<int> TimeIsUp()
        {
            try
            {
                await DisplayAlert("UYARI", "İşlem zamanında yapılamadı.Lütfen telefon numaranızı kontrol edin ve yeniden kod göndermeyi deneyin", "OK");
                entryCode.IsEnabled = false;
                lblTimer.Text = "Süre Doldu";
                isWarned = true;
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        private async void TapSendAgain_Tapped(object sender, EventArgs e)
        {
            if ( numberOfSend >= 5)
            {
                await DisplayAlert("HATA", "Çok fazla kod gönderim hatası", "OK");
                App.Current.MainPage = new NavigationPage(new IndexPageMain());
            }
            try
            {
                numberOfSend++;
                code = smsService.GenerateRandomPass(mainModel.PhoneNumber);
                countsec = 240;
                entryCode.IsEnabled = true;
                //TimerTick();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}