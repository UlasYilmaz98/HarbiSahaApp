using HarbiSahaApp.AnimationPages;
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

namespace HarbiSahaApp.PlayerAdvertCreatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlayerAdvertPage4 : ContentPage
    {
        CreatePlayerAdvertViewModel mainModel = new CreatePlayerAdvertViewModel();
        PlayerAdvertsServices advertsServices = new PlayerAdvertsServices();

        public CreatePlayerAdvertPage4(CreatePlayerAdvertViewModel model)
        {
            InitializeComponent();
            mainModel = model;
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            btnNext.IsEnabled = false;
            await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Oluşturuluyor..."));
            if (editorAdvertInfos.Text != null)
            {
                //advert.BaseAdress = editorAdvertInfos.Text;
                mainModel.AdvertInfos = editorAdvertInfos.Text;
            }
            
            CreatePlayerAdvertViewModel _advertmodel = new CreatePlayerAdvertViewModel();
            _advertmodel = await advertsServices.CreatePlayerAdvert(mainModel);
            await Navigation.PopPopupAsync();
            if (_advertmodel != null)
            {
                if ( _advertmodel.FieldName != "Too much adverts")
                {
                    App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    await DisplayAlert("Başarılı", "Oyuncu ilanınız oluşturuldu! Teşekkür ederiz.", "Tamam");
                }
                else
                {
                    await DisplayAlert("UYARI", "Aktif ilan sayınız 12'den fazla olmamalıdır.Lütfen ilanlarınızı kontrol edin.", "Tamam");
                }
                

            }
            btnNext.IsEnabled = false;
        }
    }
}