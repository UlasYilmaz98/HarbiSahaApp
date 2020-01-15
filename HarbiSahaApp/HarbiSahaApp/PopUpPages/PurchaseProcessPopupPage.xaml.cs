using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseProcessPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        CardOwnerModel mainModel = new CardOwnerModel();
        PurchaseServices service = new PurchaseServices();
        PurchaseModel returnModel = new PurchaseModel();
        public PurchaseProcessPopupPage(CardOwnerModel model)
        {
            InitializeComponent();
            mainModel = model;
            Fill();

        }

        private async void Fill()
        {
            returnModel = await service.PurchaseStart(mainModel.matchId, mainModel.fieldId, mainModel.paymentCost, mainModel.cardNumber, mainModel.cardOwnerName, mainModel.cardExpMonth,
                mainModel.cardExpYear,mainModel.cardCVC);
            if ( returnModel.status != "success")
            {
                await DisplayAlert("HATA", "Bir hata oluştu.Ödeme alınamadı." + returnModel.errorCode, "OK");
                await Navigation.PopPopupAsync();
            }
            else
            {
                webView1.Source = new HtmlWebViewSource() { Html = returnModel.HtmlContent };
            }
        }
    }
}