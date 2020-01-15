using HarbiSahaApp.Models.ViewModels;
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
    public partial class MainCreatingOpponentAdvertPage : ContentPage
    {
        CreateOpponentAdvertViewModel mainModel = new CreateOpponentAdvertViewModel();
        public MainCreatingOpponentAdvertPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CreateOpponentAdvertStep2, CreateOpponentAdvertViewModel>(this, "MainCreatingOpponentAdvertPage", async (sender,value) =>
            {
                mainModel.Day = value.Day;
                mainModel.Month = value.Month;
                mainModel.Year = value.Year;
                mainModel.MinTime = value.MinTime;
                mainModel.MaxTime = value.MaxTime;

                
            });
            MessagingCenter.Subscribe<CreateOpponentAdvertStep1, CreateOpponentAdvertViewModel>(this, "MainCreatingOpponentAdvertPage", async (sender, value) =>
            {
                mainModel.City = value.City;
                mainModel.District1 = value.District1;
                mainModel.District2 = value.District2;
                mainModel.District3 = value.District3;
                mainModel.District4 = value.District4;


            });
            MessagingCenter.Subscribe<CreateOpponentAdvertStep3, string>(this, "MainCreatingOpponentAdvertPage", async (sender, value) =>
            {
                mainModel.AdvertInfos = value;
                MessagingCenter.Send(this, "CreateOpponentAdvertStep3", mainModel);


            });

            await Navigation.PushAsync(new CreateOpponentAdvertStep1());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CreateOpponentAdvertStep1, CreateOpponentAdvertViewModel>(this, "MainCreatingOpponentAdvertPage");
            MessagingCenter.Unsubscribe<CreateOpponentAdvertStep2, CreateOpponentAdvertViewModel>(this, "MainCreatingOpponentAdvertPage");
            MessagingCenter.Unsubscribe<CreateOpponentAdvertStep3, string>(this, "MainCreatingOpponentAdvertPage");
        }

    }
}