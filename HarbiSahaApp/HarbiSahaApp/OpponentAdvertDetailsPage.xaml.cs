using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
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
    public partial class OpponentAdvertDetailsPage : ContentPage
    {
        OpponentAdvertMenuViewModel mainModel = new OpponentAdvertMenuViewModel();
        ManageServices service = new ManageServices();


        Team mainTeam = new Team();
        TeamPlayer tmPlayer = new TeamPlayer();
        TeamServices serviceTeam = new TeamServices();
        List<User> userList = new List<User>();
        List<TeamPlayer> playerList = new List<TeamPlayer>();
        List<MyTeamPageViewModel> viewModels = new List<MyTeamPageViewModel>();
        List<MyTeamPageViewModel> viewModelSorted = new List<MyTeamPageViewModel>();
        TeamPlayer selectedTeamPlayer = new TeamPlayer();

        public OpponentAdvertDetailsPage(OpponentAdvertMenuViewModel model)
        {
            InitializeComponent();
            NavigationPage nav = App.Current.MainPage as NavigationPage;
            nav.BarBackgroundColor = Color.White;

            mainModel = model;
            lblDate.Text = model.AdvertDate;
            lblTime.Text = model.MinMaxTimeStr;
            lblTeamName.Text = model.Advert.Team.Name;
            lblUserName.Text = model.OwnerName;
            lblVisitProfile.Text = model.OwnerName + " kullanıcısının profilini gör";
            lblMessageToUser.Text = model.OwnerName + " kullanıcısına mesaj at";
            
            imgProfilePicture.Source = model.Advert.RandomLine5;
            if (model.Advert.District1 == "Tüm İlçeler")
                lblCityAndTown.Text = "Tüm İlçeler";
            else
                lblCityAndTown.Text = model.Advert.District1 + "," + model.Advert.District2 + "," + model.Advert.District3;
            if (model.Advert.RandomLine6 != null)
                lblAdvertInfo.Text = model.Advert.RandomLine6;
            else
                lblAdvertInfo.Text = " -İlan bilgisi paylaşılmamış- ";
            FillList();

        }

        private async void FillList()
        {
            //TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
            //Team team = currentPlayer.Team;
            //lblTeam.Text = mainTeam.Name;
            //lblTeamCreatedOn.Text = "Kuruluş tarihi: " + mainTeam.CreatedOn.Day.ToString() + "." + mainTeam.CreatedOn.Month.ToString() + "." + mainTeam.CreatedOn.Year.ToString();
            int CaptainId;
            
            mainTeam = await service.GetSingleTeamWithPlayers(mainModel.Advert.Team.Id);
            CaptainId = mainTeam.CaptainId;
            foreach (TeamPlayer player in mainTeam.TeamPlayers)
            {
                playerList.Add(player);
            }
            //playerList = await service.GetTeamplayers();
            if (playerList == null || playerList.Count == 0)
            {
                await DisplayAlert("HATA", "Bir sorun oluştu!", "Tamam");
                App.Current.MainPage = new NavigationPage(new IndexPageMain());
            }
            else
            {

                foreach (TeamPlayer teamPlayer in playerList)
                {

                    viewModels.Add(new MyTeamPageViewModel()
                    {
                        CaptainId = CaptainId,
                        User = teamPlayer.User
                    });
                }
                int oldIndexNumber;
                MyTeamPageViewModel TEMP = new MyTeamPageViewModel();
                //colTeamPlayers.ItemsSource = viewModels;
                foreach (MyTeamPageViewModel model in viewModels)
                {
                    if (model.User.Id == CaptainId)
                        viewModelSorted.Add(model);
                }
                foreach (MyTeamPageViewModel model in viewModels)
                {
                    if (model.User.Id != CaptainId)
                        viewModelSorted.Add(model);
                }
                colTeamPlayers.ItemsSource = viewModelSorted;

            }
        }

        private async void TapVisitProfile_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfilePage(viewModelSorted[0].User,false));
            //await Navigation.PushAsync(new HarbiSahaApp.AnimationPages.AnimationPopUpPage1_Waiting("Getiriliyor..."));
            //Team selectedTeam = await service.GetSingleTeamWithPlayers(mainModel.Advert.Team.Id);

            //await Navigation.PushAsync(new TeamPageAny(selectedTeam));
        }

        private void TapMessageToUser_Tapped(object sender, EventArgs e)
        {

        }

        private void BtnAnswerAdvert_Clicked(object sender, EventArgs e)
        {

        }

        private void ColTeamPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}