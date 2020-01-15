using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.IndexTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesPage : TabbedPage
    {
        SignalrClient client = new SignalrClient();
        List<ChatChannel> channelsList = new List<ChatChannel>();
        List<PlayerAdvert> PlayerAdverts = new List<PlayerAdvert>();
        List<OpponentAdvert> OpponentAdverts = new List<OpponentAdvert>();
        List<MessagesPageViewModel> viewModels = new List<MessagesPageViewModel>();
        List<ChannelAdvertModel> channelAdvertModels = new List<ChannelAdvertModel>();
        MessagingServices messageService = new MessagingServices();
        User _currentUser = new User();
        //string myName = "Ulaş";
        int MyId = 65;

        public MessagesPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#2dbefc");
            //BarTextColor = Color.White;
            
            
         
        }

        protected override void OnAppearing()
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                User currentUser = App.Current.Properties["loggedUser"] as User;
                _currentUser = currentUser;
                client.Connect(currentUser.Email);
                //client.ConnectionError += Client_ConnectionError;
                client.OnMessageReceived += Client_OnMessageReceived;
                FillChannels();

            }
            base.OnAppearing();
            

        }

        private void Client_OnMessageReceived(SignalrUser user)
        {
            List<MessagesPageViewModel> vModels = new List<MessagesPageViewModel>();


            Device.BeginInvokeOnMainThread(() =>
            {
                if (!user.message.Contains("Connected!") && !user.message.Contains("Reconnected!") && !user.message.Contains("undefined") && !user.message.Contains("Disconnected!"))
                {

                    FillChannels();
        

                }
            });
        }

        public async void FillChannels()
        {
            viewModels.Clear();
            lstChannels.ItemsSource = null;
            channelAdvertModels = await messageService.GetMyChatChannels();
            if (channelAdvertModels == null)
                return;
            foreach (ChannelAdvertModel model in channelAdvertModels)
            {
                if (model.PlayerAdvert != null)
                {
                    viewModels.Add(new MessagesPageViewModel()
                    {
                        ChannelId = model.Channel.Id,
                        currentChannel = model.Channel,
                        PlayerAdvert = model.PlayerAdvert,
                        OpponentAdvert = null,
                        channelUser1 = model.User1,
                        channelUser2 = model.User2,
                        currentUser = _currentUser
                        
                        //NameOtherUser = model.Channel.ChatChannelUsers.Where( x=> x.UserId != currentUser.Id )


                    });
                }
                else
                {
                    viewModels.Add(new MessagesPageViewModel()
                    {
                        ChannelId = model.Channel.Id,
                        currentChannel = model.Channel,
                        PlayerAdvert = null,
                        OpponentAdvert = model.OpponentAdvert,
                        channelUser1 = model.User1,
                        channelUser2 = model.User2,
                        currentUser = _currentUser

                    });
                }
            }
            
            if ( viewModels.Count > 0)
            {
                lstChannels.ItemsSource = viewModels;
            }
            

            //throw new NotImplementedException();
        }

        private async void LstChannels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( lstChannels.SelectedItem != null)
            {
                
                MessagesPageViewModel vm = lstChannels.SelectedItem as MessagesPageViewModel;
                if ( vm.PlayerAdvert != null)
                {
                    //PlayerAdvert advert = vm.PlayerAdvert;
                    //advert.
                    await Navigation.PushAsync(new ChatPage(vm));
                }
                else
                {
                    await Navigation.PushAsync(new ChatPage(vm));
                }
                lstChannels.SelectedItem = null;
                
            }

        }
    }
}