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
    public partial class MyAdvertDetailsPage : ContentPage
    {
        PlayerAdvert mainAdvert = new PlayerAdvert();
        PlayerAdvertsServices service = new PlayerAdvertsServices();
        AdvertActionsController mainService = new AdvertActionsController();
        

        class RequestViewModel
        {
            public AdvertRequestPlayer Request { get; set; }

            public bool FrameEnabled
            {
                get
                {
                    if ( Request.isApprovedSituation == "Waiting")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public double FrameOpacity
            {
                get
                {
                    if (Request.isApprovedSituation == "Waiting")
                    {
                        return 1.0;
                    }
                    else
                    {
                        return 0.2;
                    }
                }
            }

            public string PhotoPath
            {
                get
                {
                    return Request.User.PhotoPath;
                }
            }

            public string Name
            {
                get
                {
                    return Request.User.Name;
                }
            }


        }

        List<RequestViewModel> RequestViewModels = new List<RequestViewModel>();

        public MyAdvertDetailsPage( PlayerAdvert advert )
        {
            InitializeComponent();
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.White;
            navPage.BarTextColor = Color.Black;
            mainAdvert = advert;
            lblCityAndTown.Text = advert.City + "/" + advert.Town;
            DateTime matchDay = new DateTime(mainAdvert.Year, mainAdvert.Month, mainAdvert.Day);            
            lblDateAndTime.Text = matchDay.ToString("dddd, dd MMMM yyyy");
            lblNeededPosition.Text = mainAdvert.NeededPosition;            
            if ( mainAdvert.AdvertRequestPlayers != null)
            {
                if ( mainAdvert.AdvertRequestPlayers.Count > 0)
                {
                    foreach ( AdvertRequestPlayer req in mainAdvert.AdvertRequestPlayers)
                    {
                        RequestViewModels.Add(new RequestViewModel()
                        {
                            Request = req
                        });
                    }
                    collectionView0.ItemsSource = RequestViewModels;
                }
                
            }
        }

        private async void BtnRefuse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
            Button btn = sender as Button;
            RequestViewModel model = btn.BindingContext as RequestViewModel;
            RequestViewModel currentModel = new RequestViewModel();
            string response = await mainService.AnswerPlayerRequest(model.Request.Id, "false");
            currentModel = RequestViewModels.Where(x => x.Request.Id == model.Request.Id).FirstOrDefault();
            currentModel.Request.isApprovedSituation = "Denied";
            collectionView0.ItemsSource = null;
            collectionView0.ItemsSource = RequestViewModels;
            await Navigation.PopPopupAsync();

        }

        private async void BtnAccept_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
            Button btn = sender as Button;
            RequestViewModel model = btn.BindingContext as RequestViewModel;
            RequestViewModel currentModel = new RequestViewModel();
            string response = await mainService.AnswerPlayerRequest(model.Request.Id, "true");
            currentModel = RequestViewModels.Where(x => x.Request.Id == model.Request.Id).FirstOrDefault();
            currentModel.Request.isApprovedSituation = "Accepted";
            collectionView0.ItemsSource = null;
            collectionView0.ItemsSource = RequestViewModels;
            await Navigation.PopPopupAsync();
            App.Current.MainPage = new NavigationPage(new IndexPageMain());
            await DisplayAlert("Tebrikler", "Maçın ayarlandı.Maçın için en uygun oyuncuyu bulduğunu temenni ederiz!","Tamam");
        }
    }
}