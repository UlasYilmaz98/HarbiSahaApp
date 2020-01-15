using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
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
    public partial class PlayerAdvertDetailsPage : ContentPage
    {
        PlayerAdvertDetailsViewModel viewModel = new PlayerAdvertDetailsViewModel();
        PlayerAdvert mainPlayerAdvert = new PlayerAdvert();
        bool isRequestEnabled = true;
        User currentUser = new User();
        int currentUserId = 0;
        PlayerAdvertsServices serviceAdvert = new PlayerAdvertsServices();
        public PlayerAdvertDetailsPage(PlayerAdvert advert)
        {
            InitializeComponent();
            
            mainPlayerAdvert = advert;
            viewModel.PlayerAdvert = advert;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;

            currentUser = App.Current.Properties["loggedUser"] as User;
            if ( advert.UserId == currentUser.Id)
            {
                btnAnswerAdvert.IsEnabled = false;
                btnAnswerAdvert.Text = "Bu maça başvuruda bulunamazsınız";
            }
            else
            {
                CheckRequests();
            }

            
         
            Fill();
            
        }

        private void CheckRequests()
        {
            if (App.Current.Properties.ContainsKey("loggedUser") && mainPlayerAdvert.AdvertRequestPlayers != null)
            {
                currentUser = App.Current.Properties["loggedUser"] as User;
                currentUserId = currentUser.Id;

                foreach (AdvertRequestPlayer rq in mainPlayerAdvert.AdvertRequestPlayers)
                {
                    if (rq.UserId == currentUserId)
                    {
                        isRequestEnabled = false;
                        break;
                    }

                }
            }
            return;
        }

        private void Fill()
        {
            lblAdvertInfo.Text = viewModel.AdvertInfo;
            //lblCityAndTown.Text = viewModel.CityAndTown;
            lblCityJust.Text = viewModel.PlayerAdvert.City;
            lblTownJust.Text = viewModel.PlayerAdvert.Town;
            lblMessageToUser.Text = viewModel.OwnerName + " kullanıcısına mesaj at.";
            lblNeededPosition.Text = viewModel.NeededPosition;
            lblPointAndCountPoints.Text = viewModel.PointAndCountPoints;
            lblTime.Text = viewModel.MatchTime;
            lblDate.Text = viewModel.MatchDate;
            lblUserName.Text = viewModel.OwnerName;
            lblFieldBaseAdress.Text = viewModel.AdvertBaseAdress;
            lblFieldName.Text = viewModel.FieldName;
            //lblCityAndTown.Text = viewModel.CityAndTown;
            lblPrice.Text = viewModel.Cost;
            lblFinishTime.Text = viewModel.MatchFinishTime;
            //lbluserAge.Text = viewModel.UserAge;
            //lblVisitProfile.Text = viewModel.OwnerName + " kullanıcısının profilini ziyaret et";

            imgProfilePicture.Source = viewModel.ProfilePicturePath;

            if( isRequestEnabled == false )
            {
                btnAnswerAdvert.IsEnabled = false;
                btnAnswerAdvert.Text = "BU MAÇ İÇİN BAŞVURDUNUZ";
            }
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (mainPlayerAdvert.UserId != currentUser.Id)
            {
                MessagesPageViewModel model = new MessagesPageViewModel()
                {
                    currentUser = currentUser,
                    PlayerAdvert = mainPlayerAdvert,
                    OpponentAdvert = null,
                    channelUser1 = mainPlayerAdvert.User,
                    currentChannel = null,
                    channelUser2 = null,

                };
                await Navigation.PushAsync(new ChatPage(model));
            }
            else
            {
                await DisplayAlert("Upps!", "Kendinizle konuşamazsınız.Felsefi olarak bakıyorsanız başka tabii :)", "OK");
            }

            
        }

        private async void TapVisitProfile_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage(mainPlayerAdvert.User, false)) ;
        }

        private async void BtnAnswerAdvert_Clicked(object sender, EventArgs e)
        {
            btnAnswerAdvert.IsEnabled = false;
            if ( App.Current.Properties.ContainsKey("loggedUser") )
            {
                if (isRequestEnabled == true)
                {
                    AdvertRequestPlayer request = new AdvertRequestPlayer();
                    await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Gönderiliyor..."));
                    string situationMessage = await serviceAdvert.SendPlayertAdvertReques(currentUserId, mainPlayerAdvert.Id);
                    if( situationMessage == "Oluşturuldu" )
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert("BAŞARILI", "Maça katılma isteğiniz gönderildi.İsteğin durumunu, mesajlar>bildirimler sekmesinden kontrol edebilirsiniz.", "Tamam");
                        App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    }
                    else if ( situationMessage == "Kapalı")
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert("UYARI", "Katılmak istediğiniz maçın ilanı kapatılmış.Lütfen başka bir maçı deneyin!", "Tamam");
                    }
                    else if ( situationMessage == "Gönderilmiş" )
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert("UYARI", "Bu maç için zaten istekte bulunmuşsunuz!", "Tamam");
                    }
                    else if( situationMessage == "Fazla")
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert("UYARI", "Aynı anda en fazla 4 adet cevaplanmamış isteğe sahip olabilirsiniz.Lütfen ilan isteklerinizi gözden geçirin.", "Tamam");
                    }
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert("UYARI", "Lütfen giriş yapınız.", "OK");
                btnAnswerAdvert.IsEnabled = false;
                btnAnswerAdvert.Text = "Lütfen giriş yapınız";
            }

            
        }

        
    }
}