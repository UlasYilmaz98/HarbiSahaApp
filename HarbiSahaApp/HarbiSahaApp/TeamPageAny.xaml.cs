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
    public partial class TeamPageAny : ContentPage
    {
        Team mainTeam = new Team();
        TeamPlayer tmPlayer = new TeamPlayer();
        TeamServices service = new TeamServices();
        List<User> userList = new List<User>();
        List<TeamPlayer> playerList = new List<TeamPlayer>();
        List<MyTeamPageViewModel> viewModels = new List<MyTeamPageViewModel>();
        List<MyTeamPageViewModel> viewModelSorted = new List<MyTeamPageViewModel>();
        TeamPlayer selectedTeamPlayer = new TeamPlayer();
        int CaptainId;
        public TeamPageAny(Team team)
        {
            InitializeComponent();
            NavigationPage nav = App.Current.MainPage as NavigationPage;
            nav.BarBackgroundColor = Color.White;
            //this.BarTextColor = Color.White;
            FillList();
        }

        private async void FillList()
        {
            //TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
            //Team team = currentPlayer.Team;
            lblTeam.Text = mainTeam.Name;
            lblTeamCreatedOn.Text = "Kuruluş tarihi: " + mainTeam.CreatedOn.Day.ToString() + "." + mainTeam.CreatedOn.Month.ToString() + "." + mainTeam.CreatedOn.Year.ToString();
            CaptainId = mainTeam.CaptainId;
            foreach ( TeamPlayer player in mainTeam.TeamPlayers)
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

        private async void ColTeamPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( colTeamPlayers.SelectedItem != null)
            {
                User user = new User();
                MyTeamPageViewModel model = colTeamPlayers.SelectedItem as MyTeamPageViewModel;
                user = model.User;
                await Navigation.PushAsync(new UserProfilePage(user, false));

            }
        }
    }
}