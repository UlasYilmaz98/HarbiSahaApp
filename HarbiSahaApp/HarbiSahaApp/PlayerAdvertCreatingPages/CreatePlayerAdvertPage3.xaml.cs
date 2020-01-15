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
    public partial class CreatePlayerAdvertPage3 : ContentPage
    {
        CreatePlayerAdvertViewModel mainModel = new CreatePlayerAdvertViewModel();


        public CreatePlayerAdvertPage3(CreatePlayerAdvertViewModel model)
        {
            InitializeComponent();
            mainModel = model;
            FillPickers();
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (datePickerDate.Date != null && timePickerTime.Time != null)
            {
                if (framePositionPicker1.IsVisible == true && (positionPicker1.SelectedItem == null || positionPicker1.SelectedItem == " "))
                    await DisplayAlert("UYARI", "Oyuncu1 için mevki seçin", "Tamam");
                else if (framePositionPicker2.IsVisible == true && (positionPicker2.SelectedItem == null || positionPicker2.SelectedItem == " "))
                    await DisplayAlert("UYARI", "Oyuncu2 için mevki seçin", "Tamam");
                else if (framePositionPicker3.IsVisible == true && (positionPicker3.SelectedItem == null || positionPicker3.SelectedItem == " "))
                    await DisplayAlert("UYARI", "Oyuncu3 için mevki seçin", "Tamam");
                else
                {

                    mainModel.Day = datePickerDate.Date.Day;
                    mainModel.Month = datePickerDate.Date.Month;
                    mainModel.Year = datePickerDate.Date.Year;
                    mainModel.MatchStart = timePickerTime.Time;
                    mainModel.NeededPosition1 = positionPicker1.SelectedItem as string;
                    if ( positionPicker2.IsVisible == true )
                        mainModel.NeededPosition2 = positionPicker2.SelectedItem as string;
                    if ( positionPicker3.IsVisible == true )
                        mainModel.NeededPosition3 = positionPicker3.SelectedItem as string;
                    //layoutStep3.IsVisible = false;
                    await Navigation.PushAsync(new CreatePlayerAdvertPage4(mainModel));
                    
                    



                }


            }
            else
            {
                await DisplayAlert("HATA", "Tarih ve Saat bilgisi doldurulmalıdır", "OK");
            }
        }

        private void FillPickers()
        {
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            DateTime maxDay = DateTime.Today.AddMonths(6);
            datePickerDate.MaximumDate = maxDay;
            positionPicker1.ItemsSource = new List<string>() { "Tüm Mevkiler", "Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            positionPicker2.ItemsSource = new List<string>() { "Tüm Mevkiler", "Kaleci", "Defans", "Orta Saha", "Hücumcu" };
            positionPicker3.ItemsSource = new List<string>() { "Tüm Mevkiler", "Kaleci", "Defans", "Orta Saha", "Hücumcu" };

        }

        private void BtnAddPlayerAdvert_Clicked(object sender, EventArgs e)
        {
            if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == false)
            {
                //position1 = positionPicker1.SelectedItem as string;
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = true;
            }
            else if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true)
            {
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = true;
                framePositionLabel3.IsVisible = true;
                frameAddPlayerAdvert.IsVisible = false;
                frameRemovePlayerAdvert.IsVisible = true;
            }
        }

        private void BtnRemovePlayerAdvert_Clicked(object sender, EventArgs e)
        {
            if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true && framePositionPicker3.IsVisible == true)
            {
                framePositionPicker2.IsVisible = true;
                framePositionLabel2.IsVisible = true;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = true;
            }
            else if (framePositionPicker1.IsVisible == true && framePositionPicker2.IsVisible == true && framePositionPicker3.IsVisible == false)
            {
                framePositionPicker2.IsVisible = false;
                framePositionLabel2.IsVisible = false;
                framePositionPicker3.IsVisible = false;
                framePositionLabel3.IsVisible = false;
                frameAddPlayerAdvert.IsVisible = true;
                frameRemovePlayerAdvert.IsVisible = false;
            }
        }
    }
}