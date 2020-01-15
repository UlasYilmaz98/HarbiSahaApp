using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.PopUpPages;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldTimesPage : ContentPage
    {
        Field mainField = new Field();
        List<Match> matches = new List<Match>();
        //List<FieldTimesPageViewModel> viewModels = new List<FieldTimesPageViewModel>();
        IList<FieldTimesPageViewModel> viewModels = new ObservableCollection<FieldTimesPageViewModel>();
        ManageMatchServices service = new ManageMatchServices();
        List<string> fieldZoneNames = new List<string>();
        FieldZone currentFieldZone = new FieldZone();
        DateTime currentDay = new DateTime();
        FieldTimesPageViewModel selectedModel = new FieldTimesPageViewModel();
        bool isStarted = false; // for pickers
        public FieldTimesPage(Field field )
        {
            InitializeComponent();
          
            currentDay = DateTime.Now;
            mainField = field;
            foreach (FieldZone zone in mainField.FieldZones)
                fieldZoneNames.Add(zone.FieldZoneName);
            pickerFieldZones.ItemsSource = fieldZoneNames;
            pickerFieldZones.SelectedIndex = 0;
            isStarted = true;
            currentFieldZone = mainField.FieldZones[0];
            datePickerDate.Format = " dddd, dd MMMM ";
            datePickerDate.MinimumDate = DateTime.Today;
            datePickerDate.MaximumDate = DateTime.Today.AddDays(21);
            datePickerDate.Date = currentDay.Date;
            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.Title = mainField.FieldName;
            Fill();
            
        }

        private async void Fill()
        {
            await Navigation.PushPopupAsync(new Base1PopupPage("Yükleniyor..."));
            matches = await service.getFieldMathces(mainField);
            //lblFieldName.Text = mainField.FieldName;
            if ( matches.Count > 0 )
            {
                lblKapora.Text = matches[0].RandomLine3;
                //lblTotal.Text = matches[1].RandomLine4;
                lblTotal.Text = (Convert.ToInt32(matches[1].RandomLine4) - Convert.ToInt32(matches[0].RandomLine3)).ToString();
            }
            if (currentFieldZone.TimeMethod == "exact")
            {
                for (int i = 0; i < 24; i++)
                {
                    viewModels.Add(new FieldTimesPageViewModel()
                    {
                        date = currentDay,
                        isEnabled = false,
                        match = null,
                        TimeInt = i * 100,
                        isSelected = false,
                        

                    });

                }
            }
            else if (currentFieldZone.TimeMethod == "half")
            {
                for (int i = 0; i < 24; i++)
                {
                    viewModels.Add(new FieldTimesPageViewModel()
                    {
                        date = currentDay,
                        isEnabled = false,
                        match = null,
                        TimeInt = i * 100 + 30,


                    });

                }
            }

            foreach (Match match in matches)
            {
                if (match.MatchOwnerId == 1 && match.FieldZoneId == currentFieldZone.Id)
                {

                    FieldTimesPageViewModel model = viewModels.Where(x => x.TimeInt == match.StartOn && x.date.Day == match.Day && x.date.Month == match.Month && x.date.Year == match.Year).FirstOrDefault();
                    if (model != null)
                    {
                        model.isEnabled = true;
                        model.match = match;
                        if (model.date == DateTime.Now.Date && model.TimeInt / 100 <= DateTime.Now.Hour + 1)
                        {
                            model.isEnabled = false;
                        }
                    }
                   
                }
               
            }
            //colMatches.ItemsSource = viewModels;
           
            colMatches.ItemsSource = viewModels;
            //colMatches.BindingContext = viewModels;
            await Navigation.PopPopupAsync();

            
        }

        private void FrmTapping_Tapped(object sender, EventArgs e)
        {

        }

        private void BtnForward_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {

        }

        private void ColMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( colMatches.SelectedItem != null)
            {
                FieldTimesPageViewModel selected = e.CurrentSelection[0] as FieldTimesPageViewModel;
                foreach( FieldTimesPageViewModel model in viewModels)
                {
                    model.isSelected = false;
                }
                selected.isSelected = true;
                colMatches.SelectedItem = null;
                colMatches.ItemsSource = null;
                colMatches.ItemsSource = viewModels;
                lblKapora.Text = selected.match.RandomLine3;
                //lblTotal.Text = matches[1].RandomLine4;
                lblTotal.Text = (Convert.ToInt32(selected.match.RandomLine4) - Convert.ToInt32(selected.match.RandomLine3)).ToString();
                selectedModel = selected;
                btnContinue.IsEnabled = true;
            }
            
        }

        private async void DatePickerDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            currentDay = datePickerDate.Date;
            colMatches.ItemsSource = null;
            viewModels.Clear();
            lblKapora.Text = "";
            lblTotal.Text = "";
            btnContinue.IsEnabled = false;
            //FILLING AGAIN
            FillAgainDateChanged();

        }

        private async void FillAgainDateChanged()
        {
            if (currentFieldZone.TimeMethod == "exact")
            {
                for (int i = 0; i < 24; i++)
                {
                    viewModels.Add(new FieldTimesPageViewModel()
                    {
                        date = currentDay,
                        isEnabled = false,
                        match = null,
                        TimeInt = i * 100,
                        isSelected = false,


                    });

                }
            }
            else if (currentFieldZone.TimeMethod == "half")
            {
                for (int i = 0; i < 24; i++)
                {
                    viewModels.Add(new FieldTimesPageViewModel()
                    {
                        date = currentDay,
                        isEnabled = false,
                        match = null,
                        TimeInt = i * 100 + 30,


                    });

                }
            }

            foreach (Match match in matches)
            {
                if (match.MatchOwnerId == 1 && match.FieldZoneId == currentFieldZone.Id)
                {

                    FieldTimesPageViewModel model = viewModels.Where(x => x.TimeInt == match.StartOn && x.date.Day == match.Day && x.date.Month == match.Month && x.date.Year == match.Year).FirstOrDefault();
                    if (model != null)
                    {
                        model.isEnabled = true;
                        model.match = match;
                        if (model.date == DateTime.Now.Date && model.TimeInt / 100 <= DateTime.Now.Hour + 1)
                        {
                            model.isEnabled = false;
                        }
                    }

                }

            }
            colMatches.ItemsSource = viewModels;
        }

        private void PickerFieldZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isStarted)
                return;
            string fieldZoneName = pickerFieldZones.SelectedItem as string;
            currentFieldZone = mainField.FieldZones.Where(x => x.FieldZoneName == fieldZoneName).FirstOrDefault();
            colMatches.SelectedItem = null;
            lblKapora.Text = "";
            lblTotal.Text = "";
            btnContinue.IsEnabled = false;
            viewModels.Clear();
            FillAgainDateChanged();

        }

        private async void BtnContinue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaymentPage(selectedModel, mainField));
        }
    }
}