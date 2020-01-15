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
    public partial class DeleteAdvertRequestPage : ContentPage
    {
        string reason;
        string detailedReason;
        AdvertRequestPlayer mainPlayerRequest = new AdvertRequestPlayer();
        AdvertRequestOpponent mainOpponentRequest = new AdvertRequestOpponent();
        PlayerAdvertsServices service = new PlayerAdvertsServices();
        AdvertActionsController reqController = new AdvertActionsController();
        List<string> reasonsList = new List<string>()
        {
            "Acil bir durum oluştu,isteği iptal etmek zorundayım.",
            "İlan sahibi ile ilgili bir anlaşmazlık var.",        
            "Maçın konumunun bana uygun olmadığını fark ettim.",
            "Yanlış maça katılma isteği gönderdiğimi fark ettim.",           
            "ilan sahibi ile iletişime geçemiyorum.","İstek oluştururken hata yaptım.",
            "Diğer nedenler."
        };


        public DeleteAdvertRequestPage(string type,AdvertRequestPlayer playerAdvertRequest,AdvertRequestOpponent opponentAdvertRequest)
        {
            InitializeComponent();
            mainPlayerRequest = playerAdvertRequest;
            mainOpponentRequest = opponentAdvertRequest;
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
            if ( String.IsNullOrEmpty(detailedReason))
            {
                await DisplayAlert("UYARI", "En az 6 karakter uzunluğunda bir neden belirtmelisiniz.", "Tamam");
            }
            else if (reason.Length < 6 || detailedReason.Length < 6)
            {
                await DisplayAlert("UYARI", "En az 6 karakter uzunluğunda bir neden belirtmelisiniz.", "Tamam");
            }
            else
            {
                if (mainPlayerRequest != null && mainOpponentRequest == null)
                {
                    await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
                    string response = await reqController.DeletePlayerAdvertRequest(mainPlayerRequest);
                    if (response == "OK")
                    {

                        await DisplayAlert("Başarılı", "İsteğiniz kaldırılmıştır.", "Tamam");
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else
                    {
                        await DisplayAlert("UYARI", response, "OK");
                    }
                    await Navigation.PopPopupAsync();
                }
                else if (mainPlayerRequest == null && mainOpponentRequest != null)
                {
                    await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
                    string response = await reqController.DeleteOpponentAdvertRequest(mainOpponentRequest);
                    if (response == "OK")
                    {

                        await DisplayAlert("Başarılı", "İsteğiniz kaldırılmıştır.", "Tamam");
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else
                    {
                        await DisplayAlert("UYARI", response, "OK");
                    }

                    await Navigation.PopPopupAsync();
                }

            }
        }

    }
}