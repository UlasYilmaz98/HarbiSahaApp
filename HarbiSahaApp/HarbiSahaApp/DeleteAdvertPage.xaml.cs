using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
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
    public partial class DeleteAdvertPage : ContentPage
    {
        string reason;
        string detailedReason;
        PlayerAdvert mainPlayerAdvert = new PlayerAdvert();
        OpponentAdvert mainOpponentAdvert = new OpponentAdvert();
        PlayerAdvertsServices service = new PlayerAdvertsServices();
        List<string> reasonsList = new List<string>()
        {
            "Acil bir durum oluştu,maçı iptal etmek zorundayım.",
            "Maçın oynanacağı saha ile ilgili bir anlaşmazlık var.",
            "Maçta oynayacak diğer oyuncular gelemeyecek.",
            "Maçın oynanacağı sahanın üstü açık ve hava sağanak yağışlı olacak.",
            "Yanlış maça katılma isteğini kabul ettiğimi farkettim.",
            "İsteğini kabul ettiğim kişi ile aramda anlaşmazlık söz konusu.",
            "Maç detayları hakkında bir değişiklik oldu.","Maç ilanı oluştururken hata yaptım.",
            "Diğer nedenler."
        };


        public DeleteAdvertPage( string type,PlayerAdvert playerAdvert,OpponentAdvert opponentAdvert )
        {
            InitializeComponent();
            mainPlayerAdvert = playerAdvert;
            mainOpponentAdvert = opponentAdvert;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
            pickerReasons.ItemsSource = reasonsList;
            pickerReasons.SelectedItem = reasonsList[0];

        }

        private async void BtnOK_Clicked(object sender, EventArgs e)
        {
            reason = pickerReasons.SelectedItem as string;
            detailedReason = entryMoreDetails.Text;
            if (String.IsNullOrEmpty(detailedReason))
            {
                await DisplayAlert("UYARI", "En az 6 karakter uzunluğunda bir neden belirtmelisiniz.", "Tamam");
            }
            else if (reason.Length < 6 || detailedReason.Length < 6)
            {
                await DisplayAlert("UYARI", "En az 6 karakter uzunluğunda bir neden belirtmelisiniz.", "Tamam");
            }
            else
            {
                if(mainPlayerAdvert != null && mainOpponentAdvert == null)
                {
                    await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
                    string response = await service.DeletePlayerAdvert(mainPlayerAdvert);
                    if (response == "OK")
                    {
                       
                        await DisplayAlert("Başarılı", "İlanınız kaldırılmıştır.", "Tamam");
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else
                    {
                        await DisplayAlert("UYARI", response, "OK");
                    }
                    await Navigation.PopPopupAsync();
                }
                else if(mainPlayerAdvert == null && mainOpponentAdvert != null)
                {
                    await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));


                    await Navigation.PopPopupAsync();
                }
                
            }
        }
    }
}