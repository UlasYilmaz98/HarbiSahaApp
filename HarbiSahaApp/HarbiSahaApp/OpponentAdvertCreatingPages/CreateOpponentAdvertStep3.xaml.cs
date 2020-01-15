using HarbiSahaApp.IndexTabbedPages;
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

namespace HarbiSahaApp.OpponentAdvertCreatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOpponentAdvertStep3 : ContentPage
    {
        CreateOpponentAdvertViewModel mainModel = new CreateOpponentAdvertViewModel();
        PlayerAdvertsServices service = new PlayerAdvertsServices();
        string info;
        public CreateOpponentAdvertStep3(CreateOpponentAdvertViewModel model)
        {
            InitializeComponent();
            mainModel = model;
        }

      

        private async void BtnForward_Clicked(object sender, EventArgs e)
        {
            info = editorAdvertInfos.Text;
            mainModel.AdvertInfos = info;
            //MessagingCenter.Send(this, "MainCreatingOpponentAdvertPage", info);
            //await DisplayAlert("HERE", "Buradayız", "OK");
            await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("Oluşturuluyor..."));
            CreateOpponentAdvertViewModel _model = await service.CreateOpponentAdvert(mainModel);
            if ( _model != null)
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert("BAŞARILI", "Rakip Arama İlanınız Oluşturuldu", "OK");                
                App.Current.MainPage = new NavigationPage(new IndexPageMain());
            }
            else
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert("HATA", "Bir sorun oluştu", "OK");
            }
        }

        
    }
}