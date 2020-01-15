using HarbiSahaApp.Models.ViewModels;
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
    public partial class CreatePlayerAdvertPage2 : ContentPage
    {

        string textCost;
        int cost;
        CreatePlayerAdvertViewModel mainModel = new CreatePlayerAdvertViewModel();

        public CreatePlayerAdvertPage2(CreatePlayerAdvertViewModel model)
        {
            InitializeComponent();
            mainModel = model;
            textCost = "10 ₺";
            lblCost.Text = textCost;
            cost = 10;
           
        }

        private void BtnMinusIcon_Clicked(object sender, EventArgs e)
        {
            btnPlusIcon.IsEnabled = true;
            btnPlusIcon.Opacity = 1;
            cost -= 5;
            textCost = cost.ToString() + " ₺";
            lblCost.Text = textCost;
            if (cost <= 0)
            {
                btnMinusIcon.IsEnabled = false;
                btnMinusIcon.Opacity = 0.2;
            }
                

        }

        private void BtnPlusIcon_Clicked(object sender, EventArgs e)
        {
            btnMinusIcon.IsEnabled = true;
            btnMinusIcon.Opacity = 1;
            cost += 5;
            textCost = cost.ToString() + " ₺";
            lblCost.Text = textCost;
            if (cost >= 40)
            {
                btnPlusIcon.IsEnabled = false;
                btnPlusIcon.Opacity = 0.2;
            }
                
            
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            btnNext.IsEnabled = false;
            if( String.IsNullOrWhiteSpace(entryFieldName.Text) )
            {
                await DisplayAlert("UYARI", "Saha adı alanı boş geçilemez.", "Tamam");
            }
            else
            {
                mainModel.FieldName = entryFieldName.Text;
                mainModel.BaseAdress = editorBaseAdress.Text;                
                //layoutStep2.IsVisible = false;              
                mainModel.Cost = cost.ToString();
            }
            btnNext.IsEnabled = true;
            await Navigation.PushAsync(new CreatePlayerAdvertPage3(mainModel));

        }
    }
}