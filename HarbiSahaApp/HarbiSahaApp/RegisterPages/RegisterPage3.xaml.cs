using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
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
    public partial class RegisterPage3 : ContentPage
    {

        RegisterModel mainModel = new RegisterModel();
        string infoPhone;

        public RegisterPage3(RegisterModel model)
        {
            InitializeComponent();
            mainModel = model;
            infoPhone = "Harbi Saha olarak kullanıcı deneyimini her zaman ön planda tutuyoruz.Harbi Saha'da kayıtlı olan " +
                "kullanıcı hesaplarının gerçek kişilere ait olması hem kullanıcı deneyimini, hem de veri güvenliğini " +
                "korumamıza yardımcı olur.Telefon numaranız izniniz olmadan asla 3.kişi,şahıs ve kurumlar ile paylaşılmaz. " +
                "Telefon doğrulama sistemimizi inşa ederken verilerinizi korumak için en güvenilir " +
                "ve alanında öncü firmalar ile çalışıyoruz.Bu sistem ile bot hesapları sistemimizden uzak tutuyor ve hem kullanıcılarımızın " +
                "hem de iş partnerlerimizin haklarını güvence altına alıyoruz.Anlayışınız ve iş birliğiniz için teşekkür ederiz! ";
        }

        private async void BtnConfirmPhone_Clicked(object sender, EventArgs e)
        {
            if ( entryPhoneNumber.Text!= null)
            {
                if ( entryPhoneNumber.Text.Count() < 18)
                {
                    await DisplayAlert("UYARI", "Lütfen geçerli bir telefon numarası giriniz.","Tamam");
                }
                else
                {
                    string PhoneNumber = "0" + entryPhoneNumber.Text.Substring(4, 3) + entryPhoneNumber.Text.Substring(9, 3) + entryPhoneNumber.Text.Substring(13, 2)
                        + entryPhoneNumber.Text.Substring(16, 2);
                    mainModel.PhoneNumber = PhoneNumber;
                    await Navigation.PushAsync(new RegisterPage4(mainModel));
                }
            }
            else
            {
                await DisplayAlert("UYARI", "Telefon numarası boş geçilemez", "OK");
            }
        }

        private async void TapWhy_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("BİLGİ", infoPhone, "Tamam");
        }
    }
}