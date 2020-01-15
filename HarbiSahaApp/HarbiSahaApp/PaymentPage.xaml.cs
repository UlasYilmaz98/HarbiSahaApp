using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.PopUpPages;
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
    public partial class PaymentPage : ContentPage
    {
        List<string> months = new List<string>() { "01", "02", "03", "04", "05", "06", "06", "07", "08", "09", "10", "11", "12" };
        List<string> years = new List<string>();
        PurchaseServices purchaseService = new PurchaseServices();
        Field mainField = new Field();
        FieldTimesPageViewModel mainFieldTimesPageViewModel = new FieldTimesPageViewModel();
        public PaymentPage(FieldTimesPageViewModel model,Field field)
        {
            InitializeComponent();

            if ( model.match != null)
            {
                mainField = field;
                mainFieldTimesPageViewModel = model;
                lblCityAndTown.Text = field.City + "/" + field.District;
                lblDate.Text = model.dayStr + " " + model.date.Year.ToString();
                lblFieldName.Text = field.FieldName;
                lblPriceFromCard.Text = model.match.RandomLine3 + " TL";
                lblPayOnField.Text = (Convert.ToInt32(model.match.RandomLine4) - Convert.ToInt32(model.match.RandomLine3)).ToString() + " TL";
                //lblTotalPrice.Text = model.match.RandomLine4 + " TL";
                lblTime.Text = model.TimeStr;
                NavigationPage mainP = App.Current.MainPage as NavigationPage;
                mainP.BarBackgroundColor = Color.FromHex("#2dbefc");
                mainP.BarTextColor = Color.White;
                
                
            }
            for( int i=2020;i<2033;i++)
            {
                years.Add(i.ToString());
            }
            cardExpDateMonth.ItemsSource = months;
            cardExpDateYear.ItemsSource = years;

        }

        private async void LblAggrement_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new UserAgreementPolicyPage());
        }

        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            if (checkTrue())
            {
                CardOwnerModel newCardModel = new CardOwnerModel();
                newCardModel.cardOwnerName = cardOwnerNameSurname.Text;
                newCardModel.cardNumber = cardNumber.Text;
                newCardModel.cardExpMonth = cardExpDateMonth.SelectedItem.ToString();
                string expYear = cardExpDateYear.SelectedItem.ToString().Substring(2, 2);
                newCardModel.cardExpYear = expYear;
                newCardModel.cardCVC = cardCVCNumber.Text;
                newCardModel.fieldId = mainField.Id;
                newCardModel.paymentCost = mainFieldTimesPageViewModel.match.RandomLine3;
                newCardModel.matchId = mainFieldTimesPageViewModel.match.Id;
                await Navigation.PushPopupAsync(new PurchaseProcessPopupPage(newCardModel));


                

            }
        }


        private bool checkTrue()
        {
            if (String.IsNullOrEmpty(cardOwnerNameSurname.Text) || String.IsNullOrEmpty(cardNumber.Text) || String.IsNullOrEmpty(cardCVCNumber.Text))
            {
                DisplayAlert("UYARI", "Tüm alanlar doldurulmalıdır.", "Tamam");
                return false;
            }
                
            else if (cardExpDateMonth.SelectedItem == null || cardExpDateYear.SelectedItem == null)
            {
                DisplayAlert("UYARI", "Lütfen ay ve yıl seçiniz.", "Tamam");
                return false;
            }
            else if (checkBoxConfirm.IsChecked == false)
            {
                DisplayAlert("UYARI", "Devam etmek için sözleşmeyi kabul etmeniz gerekmektedir.", "Tamam");
                return false;
            }
            return true;

        }
    }
}