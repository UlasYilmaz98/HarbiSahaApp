using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarbiSahaApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HarbiSahaApp.Models.ControlModels;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAdvertsPage : TabbedPage
    {
        ActivityControl activityControl = new ActivityControl();
        AdvertActionsController service = new AdvertActionsController();
        List<MixedAdvertsViewModel> advertsModelListCurrent = new List<MixedAdvertsViewModel>();
        List<MixedAdvertsViewModel> advertsModelListPast = new List<MixedAdvertsViewModel>();
        List<MixedRequestsViewModel> requestsModelListCurrent = new List<MixedRequestsViewModel>();
        List<MixedRequestsViewModel> requestsModelListPast = new List<MixedRequestsViewModel>();
        MyAllAdvertsModel model = new MyAllAdvertsModel();
        MyAllRequestsModel reqModel = new MyAllRequestsModel();

        public MyAdvertsPage()
        {
            InitializeComponent();
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
            navPage.BarTextColor = Color.White;
            Fill();
            
            
        }

        private async void Fill()
        {
            //await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Getiriliyor..."));
            activityControl.MakeVisible(ActivityLayout, activity);
            model = await service.GetMyAdverts();
            if( model.OpponentAdverts.Count ==0 && model.PlayerAdverts.Count ==0)
            {
                noOpenAdvert.IsVisible = true;
                activityControl.MakeUnVisible(ActivityLayout, activity);
                FillReqs();
            }
            else
            {
                DateTime nowT = DateTime.Now;
                DateTime today = new DateTime(nowT.Year, nowT.Month, nowT.Day);

                foreach (PlayerAdvert advert in model.PlayerAdverts)
                {
                    DateTime thatDay = new DateTime(advert.Year, advert.Month, advert.Day);
                    if (today <= thatDay)
                    {
                        advertsModelListCurrent.Add(new MixedAdvertsViewModel()
                        {
                            PlayerAdvert = advert,
                            Type = "Oyuncu İlanı"
                        });
                    }
                    else
                    {
                        advertsModelListPast.Add(new MixedAdvertsViewModel()
                        {
                            PlayerAdvert = advert,
                            Type = "Oyuncu İlanı"
                        });
                    }
                }
                foreach (OpponentAdvert advert in model.OpponentAdverts)
                {

                    DateTime thatDay = new DateTime(advert.Year, advert.Month, advert.Day);
                    if (today <= thatDay)
                    {
                        advertsModelListCurrent.Add(new MixedAdvertsViewModel()
                        {
                            OpponentAdvert = advert,
                            Type = "Rakip İlanı"
                        });
                    }
                    else
                    {
                        advertsModelListPast.Add(new MixedAdvertsViewModel()
                        {
                            OpponentAdvert = advert,
                            Type = "Rakip İlanı"
                        });
                    }
                }
                FillList();
            }
            

        }

        private async void FillReqs()
        {
            activityControl.MakeVisible(ActivityLayout1, activity1);
            reqModel = await service.GetMyRequests();
            if (reqModel.AdvertRequestPlayers.Count == 0 && reqModel.AdvertRequestOpponents.Count == 0)
                noOpenRequest.IsVisible = true;
            else
            {
                DateTime nowT = DateTime.Now;
                DateTime today = new DateTime(nowT.Year, nowT.Month, nowT.Day);

                foreach (AdvertRequestPlayer advertReq in reqModel.AdvertRequestPlayers)
                {
                    PlayerAdvert advert = advertReq.PlayerAdvert;
                    DateTime thatDay = new DateTime(advert.Year, advert.Month, advert.Day);
                    if (today <= thatDay)
                    {
                        requestsModelListCurrent.Add(new MixedRequestsViewModel()
                        {
                            PlayerAdvert = advert,
                            Type = "Oyuncu İlanı",
                            AdvertRequestPlayer = advertReq
                            
                        });
                    }
                    else
                    {
                        requestsModelListPast.Add(new MixedRequestsViewModel()
                        {
                            PlayerAdvert = advert,
                            Type = "Oyuncu İlanı",
                            AdvertRequestPlayer = advertReq
                        });
                    }
                }
                foreach (AdvertRequestOpponent opponentReq in reqModel.AdvertRequestOpponents)
                {
                    OpponentAdvert opponentAdvert = opponentReq.OpponentAdvert;
                    DateTime thatDay = new DateTime(opponentAdvert.Year, opponentAdvert.Month, opponentAdvert.Day);
                    if (today <= thatDay)
                    {
                        requestsModelListCurrent.Add(new MixedRequestsViewModel()
                        {
                            OpponentAdvert = opponentAdvert,
                            Type = "Rakip İlanı",
                            AdvertRequestOpponent = opponentReq
                        });
                    }
                    else
                    {
                        requestsModelListPast.Add(new MixedRequestsViewModel()
                        {
                            OpponentAdvert = opponentAdvert,
                            Type = "Rakip İlanı",
                            AdvertRequestOpponent = opponentReq
                        });
                    }
                }

                collectionView1.ItemsSource = requestsModelListCurrent;
            }
            
            activityControl.MakeUnVisible(ActivityLayout1, activity1);

            //DataTemplate tmp = new DataTemplate();
            //collectionView0.ItemTemplate.Values

        }

        

        private async void FillList()
        {

            collectionView0.ItemsSource = advertsModelListCurrent;
            activityControl.MakeUnVisible(ActivityLayout, activity);
            FillReqs();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BarBackgroundColor = Color.FromHex("#2dbefc");
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
        }

        private void CollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton imgButton = (ImageButton)sender;
            //MixedAdvertsViewModel mod = imgButton.Parent as MixedAdvertsViewModel;
            MixedAdvertsViewModel model = imgButton.BindingContext as MixedAdvertsViewModel;
            string selected;

            if ( model.Type == "Oyuncu İlanı")
            {
                if (model.PlayerAdvert.isOpen == true)
                {
                    selected = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör", "İlanı sil", "İsteklere gözat" });
                }
                else
                {
                    selected = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör" });
                }
                if (selected == "İlanı gör")
                {
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(model.PlayerAdvert));
                }
                else if (selected == "İlanı sil")
                {
                    await Navigation.PushAsync(new DeleteAdvertPage("Oyuncu İlanı", model.PlayerAdvert, null));
                }
                else if (selected == "İsteklere gözat")
                {
                    AdvertActionsController controller = new AdvertActionsController();
                    PlayerAdvert adv = new PlayerAdvert();
                    await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Yükleniyor..."));
                    adv = await controller.GetMySinglePlayerAdvert(model.PlayerAdvert.Id);
                    await Navigation.PushAsync(new MyAdvertDetailsPage(adv));
                    await Navigation.PopPopupAsync();

                }
            }
            else
            {

            }


            
        }

        private async void FrameReqTapped_Tapped(object sender, EventArgs e)
        {
            Frame frm = (Frame)sender;
            MixedRequestsViewModel model = frm.BindingContext as MixedRequestsViewModel;
            if ( model.Type == "Oyuncu İlanı")
            {
                string option;
                if (model.AdvertRequestPlayer.isApprovedSituation != "Denied")
                    option = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör", "İsteği sil" });
                else
                    option = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör" });
                if (option == "İlanı gör")
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(model.PlayerAdvert));
                else if( option == "İsteği sil")
                    await Navigation.PushAsync(new DeleteAdvertRequestPage("Oyuncu İlanı", model.AdvertRequestPlayer, null));
            }
            else
            {
                string option;
                if ( model.AdvertRequestOpponent.isApprovedSituation != "Denied" )
                    option = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör", "İsteği sil" });
                else
                    option = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör" });

                if (option == "İlanı gör")
                {
                    OpponentAdvertMenuViewModel _model = new OpponentAdvertMenuViewModel() { Advert = model.OpponentAdvert };
                    await Navigation.PushAsync(new OpponentAdvertDetailsPage(_model));
                }
                    
                else
                    await Navigation.PushAsync(new DeleteAdvertRequestPage("Rakip İlanı", null, model.AdvertRequestOpponent));
            }
        }

        private async void FrameAdvTapped_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            //MixedAdvertsViewModel mod = imgButton.Parent as MixedAdvertsViewModel;
            MixedAdvertsViewModel model = frame.BindingContext as MixedAdvertsViewModel;
            string selected;

            if (model.Type == "Oyuncu İlanı")
            {
                if (model.PlayerAdvert.isOpen == true)
                {
                    selected = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör", "İlanı sil", "İsteklere gözat" });
                }
                else
                {
                    selected = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", new string[] { "İlanı gör" });
                }
                if (selected == "İlanı gör")
                {
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(model.PlayerAdvert));
                }
                else if (selected == "İlanı sil")
                {
                    await Navigation.PushAsync(new DeleteAdvertPage("Oyuncu İlanı", model.PlayerAdvert, null));
                }
                else if (selected == "İsteklere gözat")
                {
                    AdvertActionsController controller = new AdvertActionsController();
                    PlayerAdvert adv = new PlayerAdvert();
                    await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Yükleniyor..."));
                    adv = await controller.GetMySinglePlayerAdvert(model.PlayerAdvert.Id);
                    await Navigation.PushAsync(new MyAdvertDetailsPage(adv));
                    await Navigation.PopPopupAsync();

                }
            }
            else
            {

            }

        }
    }
}