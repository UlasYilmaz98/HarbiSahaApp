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
    public partial class CreateOpponentAdvertStep2 : ContentPage
    {

        DateTime SelectedDate = new DateTime();
      
        CreateOpponentAdvertViewModel mainModel = new CreateOpponentAdvertViewModel();

        public CreateOpponentAdvertStep2(CreateOpponentAdvertViewModel model)
        {
            InitializeComponent();
            mainModel = model;
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            datePickerDate.MaximumDate = DateTime.Today.AddDays(90);
        }

        private async void BtnForward_Clicked(object sender, EventArgs e)
        {
            if ( datePickerDate.Date == null)
            {
                await DisplayAlert("UYARI", "Tarih alanı boş kalamaz","OK");
                
            }
            else
            {
                SelectedDate = datePickerDate.Date;
                mainModel.Day = SelectedDate.Day;
                mainModel.Month = SelectedDate.Month;
                mainModel.Year = SelectedDate.Year;
                if ( timePicker1.Time <= timePicker2.Time)
                {
                    mainModel.MinTime = timePicker1.Time;
                    mainModel.MaxTime = timePicker2.Time;
                }
                else
                {
                    mainModel.MaxTime = timePicker1.Time;
                    mainModel.MinTime = timePicker2.Time;
                }

                
                await Navigation.PushAsync(new CreateOpponentAdvertStep3(mainModel));
            }
        }
    }
}