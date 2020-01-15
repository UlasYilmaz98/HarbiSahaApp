using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models;
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

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTeamPage : ContentPage
    {
        TeamPlayer tmPlayer = new TeamPlayer();
        TeamServices service = new TeamServices();
        List<User> userList = new List<User>();
        List<TeamPlayer> playerList = new List<TeamPlayer>();
        List<MyTeamPageViewModel> viewModels = new List<MyTeamPageViewModel>();
        List<MyTeamPageViewModel> viewModelSorted = new List<MyTeamPageViewModel>();
        TeamPlayer selectedTeamPlayer = new TeamPlayer();
        int CaptainId;

        

        public MyTeamPage()
        {
            InitializeComponent();
            
            FillList();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            NavigationPage navP = App.Current.MainPage as NavigationPage;
            navP.BarBackgroundColor = Color.FromHex("#2dbefc");
            navP.BarTextColor = Color.White;
        }

        private async void FillList()
        {
            TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
            Team team = currentPlayer.Team;
            lblMyTeam.Text = team.Name;
            lblTeamCreatedOn.Text = "Kuruluş tarihi: " + team.CreatedOn.Year.ToString() + "." + team.CreatedOn.Month.ToString() + "." + team.CreatedOn.Day.ToString();
            CaptainId = team.CaptainId;
            playerList = await service.GetTeamplayers();
            if ( playerList == null || playerList.Count ==0 )
            {
                await DisplayAlert("HATA", "Bir sorun oluştu!", "Tamam");
                App.Current.MainPage = new NavigationPage(new IndexPageMain());
            }
            else
            {
                
                foreach ( TeamPlayer teamPlayer in playerList)
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
                foreach ( MyTeamPageViewModel model in viewModels)
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
            string selectedItemStr;
            User selectedUser = new User();
            MyTeamPageViewModel selectedModel = new MyTeamPageViewModel();
            //TeamPlayer myPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
            string[] options;
            User currentUser = App.Current.Properties["loggedUser"] as User;
            
            if ( colTeamPlayers.SelectedItem != null)
            {
                if ( currentUser.Id != CaptainId)
                {
                    selectedModel = colTeamPlayers.SelectedItem as MyTeamPageViewModel;
                    selectedUser = selectedModel.User;
                    if (selectedUser.Id == currentUser.Id)
                    {
                        options = new string[] { "Profilimi Gör", "Takımdan Ayrıl" };
                    }
                    else
                    {
                        options = new string[] { "Profili Gör" };
                    }
                }
                else
                {
                    if ( playerList.Count <= 1)
                    {
                        selectedModel = colTeamPlayers.SelectedItem as MyTeamPageViewModel;
                        selectedUser = selectedModel.User;
                        if (selectedUser.Id == currentUser.Id)
                        {
                            options = new string[] { "Profilimi Gör","Takımdan Ayrıl" };
                        }
                        else
                        {
                            options = new string[] { "Profili Gör", "Takımdan Çıkar", "Takım kaptanı seç" };
                        }
                    }
                    else
                    {

                        selectedModel = colTeamPlayers.SelectedItem as MyTeamPageViewModel;
                        selectedUser = selectedModel.User;
                        if (selectedUser.Id == currentUser.Id)
                        {
                            options = new string[] { "Profilimi Gör" };
                        }
                        else
                        {
                            options = new string[] { "Profili Gör", "Takımdan Çıkar", "Takım kaptanı seç" };
                        }
                    }


                    
                }
                selectedItemStr = await DisplayActionSheet("Seçenekler", "Vazgeç", "Tamam", options);
                bool _isSure;
                switch (selectedItemStr)
                {
                    case "Profili Gör":await Navigation.PushAsync(new UserProfilePage(selectedUser,true));break;
                    case "Profilimi Gör":await Navigation.PushAsync(new UserProfilePage(selectedUser,true));break;
                    case "Takım kaptanı seç":SwitchCaptain(selectedUser.Id);break;
                    case "Takımdan Ayrıl":LeaveFormTeam();break;
                    case "Takımdan Çıkar":ThrowFromTeam(selectedUser);break;
                       


                }

            }
        }

        private async void ThrowFromTeam(User selectedUser)
        {
            string name = selectedUser.Name;
            bool isSure;
            isSure = await DisplayAlert("Uyarı",name + " kullanıcısını takımdan çıkarmak istediğinize emin misiniz?", "Evet", "Vazgeç");
            if (isSure == true)
            {
                await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor"));
                string response = await service.ThrowFromTeam(selectedUser.Id);
                if (response != "Hata oluştu")
                {
                    //App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    TeamPlayer player = playerList.Where(x => x.UserId == selectedUser.Id).FirstOrDefault();
                    MyTeamPageViewModel model = viewModels.Where(x => x.User.Id == selectedUser.Id).FirstOrDefault();
                    viewModels.Remove(model);
                    playerList.Remove(player);
                    colTeamPlayers.ItemsSource = null;
                    colTeamPlayers.ItemsSource = viewModels;
                    await Navigation.PopPopupAsync();
                }
            }
        }



        private async void LeaveFormTeam()
        {
            bool isSure;
            isSure = await DisplayAlert("Uyarı", "Takımdan ayrılmak istediğinize emin misiniz?", "Evet", "Vazgeç");
            if ( isSure == true)
            {
                await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor"));
                string response = await service.LeaveFromTeam();
                if ( response != "Hata oluştu" )
                {
                    App.Current.Properties["loggedUserTeamPlayer"] = null;
                    App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    await Navigation.PopPopupAsync();
                }
            }
        }

        private async void SwitchCaptain(int newCaptainId )
        {
            bool isSure;
            isSure = await DisplayAlert("Uyarı", "Kaptanlığı devretmek istediğinize emin misiniz?", "Evet", "Vazgeç");
            if (isSure == true)
            {
                await Navigation.PushPopupAsync(new AnimationPages.AnimationPopUpPage1_Waiting("İşleniyor..."));
                string response = await service.SwitchCaptain(newCaptainId);
                if (response != "Hata oluştu")
                {
                    await DisplayAlert("Başarılı", response, "OK");
                    FillList();
                    //App.Current.MainPage = new NavigationPage(new IndexPageMain());
                    await Navigation.PopPopupAsync();
                }
            }
        }
    }
}