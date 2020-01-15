using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ControlModels;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.ServiceController;
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
    public partial class ChatPage : ContentPage
    {
        ChatChannel mainChannel = new ChatChannel();
       
        PlayerAdvert playerAdvert = new PlayerAdvert();
        OpponentAdvert opponentAdvert = new OpponentAdvert();
        string _username;  // O AN CLIENTTAKİ KULLANICI ADI . UYGULAMADA EMAIL OLACAK
        IList<SignalrUser> modelList = new ObservableCollection<SignalrUser>();
        SignalrClient client = new SignalrClient();
        bool isProperAdvert = false;
        string noMessageStr;
        User currentUser = new User();
        MessagingServices messageService = new MessagingServices();
        MessagesPageViewModel messagesPageViewModel = new MessagesPageViewModel();
        IList<ChatPageViewModel> ChatPageViewModels = new ObservableCollection<ChatPageViewModel>();
        int LastMessageOwnerId;
        ActivityControl activityControl = new ActivityControl();
        string currentMessageText;
        //User otherUser = new User();

        public ChatPage(MessagesPageViewModel model) 
        {
            InitializeComponent();

            NavigationPage navPage = App.Current.MainPage as NavigationPage;
            navPage.BarBackgroundColor = Color.FromHex("#2dbefc");
            navPage.BarTextColor = Color.White;
            activityControl.MakeVisible(ActivityLayout, activity);
            


            //model.currentChannel = messageService.GetChannel(model.currentChannel.Id);


            layoutMain.IsVisible = false;
            GetChannelMessages(model);
            //if (model.ChannelId != 0 && model.currentChannel != null)
            //{
            //    GetChannelMessages(model);
            //}
            //else
            //{
            //    messagesPageViewModel = model;
            //}
            currentUser = App.Current.Properties["loggedUser"] as User;
           
            _username = currentUser.Email;
            //messagesPageViewModel = model;
            this.Title = model.otherUserName;

            //SIGNALR
            client.Connect(_username);
            client.ConnectionError += Client_ConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            this.BindingContext = ChatPageViewModels;
         

           
        }

        private async void GetChannelMessages(MessagesPageViewModel vm)
        {
            if (vm.ChannelId != 0 && vm.currentChannel != null)
            {
                ChatChannel channel = await messageService.GetChannel(vm.ChannelId);
                vm.currentChannel = channel;
                messagesPageViewModel = vm;
                
            }
            else
            {
                messagesPageViewModel = vm;
            }



            if (messagesPageViewModel.ChannelId != 0 && messagesPageViewModel.currentChannel != null)
            {
                if (messagesPageViewModel.PlayerAdvert != null)
                {
                    opponentAdvert = null;
                    playerAdvert = messagesPageViewModel.PlayerAdvert;

                }
                else if (messagesPageViewModel.OpponentAdvert != null)
                {
                    opponentAdvert = messagesPageViewModel.OpponentAdvert;
                    playerAdvert = null;
                }
            }
            else // CHANNEL BOŞ GELMİŞSE
            {
                if (messagesPageViewModel.PlayerAdvert != null)
                {
                    //receiverEmail.Text = model.channelUser1.Email;
                    playerAdvert = messagesPageViewModel.PlayerAdvert;
                    opponentAdvert = null;
                }
                else
                {
                    //receiverEmail.Text = model.channelUser1.Email;
                    playerAdvert = null;
                    opponentAdvert = messagesPageViewModel.OpponentAdvert;
                }
            }

            if (messagesPageViewModel.PlayerAdvert != null && messagesPageViewModel.OpponentAdvert == null)
            {
                lblAdvertFirstLine.Text = messagesPageViewModel.PlayerAdvert.City + " / " + messagesPageViewModel.PlayerAdvert.Town;
                lblAdvertSecondLine.Text = messagesPageViewModel.DateAndTime;
            }
            else if (messagesPageViewModel.PlayerAdvert == null && messagesPageViewModel.OpponentAdvert != null)
            {
                lblAdvertFirstLine.Text = messagesPageViewModel.OpponentAdvert.City + " şehrinde rakip ilanı";
                lblAdvertSecondLine.Text = "";
            }




            // HEM DB HEM sıgnalr ile ilgili

            CheckProperAdvert();
            if (messagesPageViewModel.currentChannel != null)
            {
                if (messagesPageViewModel.currentChannel.Id != 0)
                {
                    mainChannel = messagesPageViewModel.currentChannel;
                    if (mainChannel.ChatChannelUsers.Count > 0)
                    {
                        if (mainChannel.ChatChannelUsers[1].ChatMessages.Count > 0 || mainChannel.ChatChannelUsers[0].ChatMessages.Count > 0)
                        {
                            List<ChatPageViewModel> tempModel = new List<ChatPageViewModel>();



                            foreach (ChatMessage msg in mainChannel.ChatChannelUsers[0].ChatMessages)
                            {
                                //allMessages.Add(msg);
                                if (mainChannel.ChatChannelUsers[0].UserId == currentUser.Id) // EĞER 0 INDEXLI CHATCHANNELUSER BEN İSEM;
                                {
                                    tempModel.Add(new ChatPageViewModel()
                                    {
                                        Message = msg.Text,
                                        isMyMessage = true,
                                        SenderUser = messagesPageViewModel.currentUser,
                                        isUserChangingPoint = false,
                                        SendingTime = msg.CreatedOn
                                    });
                                }
                                else
                                {
                                    tempModel.Add(new ChatPageViewModel()
                                    {
                                        Message = msg.Text,
                                        isMyMessage = false, // BEN OWNER DEĞİLİM
                                        SenderUser = messagesPageViewModel.OtherUser,
                                        isUserChangingPoint = false,
                                        SendingTime = msg.CreatedOn
                                    });
                                }
                                //modelList.Add(new SignalrUser() { message = msg.Text, username = "User1" });
                            }
                            foreach (ChatMessage msg in mainChannel.ChatChannelUsers[1].ChatMessages)
                            {
                                //allMessages.Add(msg);
                                //modelList.Add(new SignalrUser() { message = msg.Text, username = "User2" });
                                if (mainChannel.ChatChannelUsers[1].UserId == currentUser.Id) // EĞER 1 INDEXLI CHATCHANNELUSER  BEN İSEM;
                                {
                                    tempModel.Add(new ChatPageViewModel()
                                    {
                                        Message = msg.Text,
                                        isMyMessage = true,
                                        SenderUser = messagesPageViewModel.currentUser,
                                        isUserChangingPoint = false,
                                        SendingTime = msg.CreatedOn
                                    });
                                }
                                else
                                {
                                    tempModel.Add(new ChatPageViewModel()
                                    {
                                        Message = msg.Text,
                                        isMyMessage = false,
                                        SenderUser = messagesPageViewModel.OtherUser,
                                        isUserChangingPoint = false,
                                        SendingTime = msg.CreatedOn
                                    });
                                }
                            }
                            ChatPageViewModels = tempModel.OrderBy(x => x.SendingTime).ToList();
                            LastMessageOwnerId = ChatPageViewModels[0].SenderUser.Id;
                            ChatPageViewModels[0].isUserChangingPoint = true;
                            foreach (ChatPageViewModel chatPageViewModel in ChatPageViewModels)
                            {
                                if (chatPageViewModel.SenderUser.Id != LastMessageOwnerId)
                                {
                                    chatPageViewModel.isUserChangingPoint = true;
                                    LastMessageOwnerId = chatPageViewModel.SenderUser.Id;
                                }

                            }
                            colMessages.ItemsSource = ChatPageViewModels;
                            colMessages.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
                            colMessages.ScrollTo(ChatPageViewModels.Last(), -1, ScrollToPosition.End, true);
                        }
                    }
                }
            }
            else
            {
                mainChannel = null;
            }
            activityControl.MakeUnVisible(ActivityLayout, activity);
            layoutMain.IsVisible = true;



            return;

            
           
        }

        private async void CheckProperAdvert()
        {
            if (playerAdvert != null && opponentAdvert == null)
            {
                isProperAdvert = CheckIfExists(playerAdvert);
                if (isProperAdvert == false)
                {
                    //txtMessage.IsVisible = false;
                    await DisplayAlert("UYARI", noMessageStr, "OK");
                }
            }
            else if (playerAdvert == null && opponentAdvert != null)
            {
                isProperAdvert = CheckIfExists(opponentAdvert);
                if (isProperAdvert == false)
                {
                    //txtMessage.IsVisible = false;
                    await DisplayAlert("UYARI", noMessageStr, "OK");
                }
            }
        }

        private bool CheckIfExists(PlayerAdvert advert)
        {
            DateTime thatDay = new DateTime(advert.Year, advert.Month, advert.Day);
            if ( advert.isOpen != true)
            {
                noMessageStr = "Bu ilan için artık oyuncu aranmıyor.";
                return false;
            }
            else if ( thatDay < DateTime.Today)
            {
                noMessageStr = "Bu ilanın süresi dolmuş.";
                return false;
            }
            return true;
        }
        private bool CheckIfExists(OpponentAdvert advert)
        {
            DateTime thatDay = new DateTime(advert.Year, advert.Month, advert.Day);
            if (advert.isOpen != true)
            {
                noMessageStr = "Bu ilan için artık rakip aranmıyor.";
                return false;
            }
            else if (thatDay < DateTime.Today)
            {
                noMessageStr = "Bu ilanın süresi dolmuş.";
                return false;
            }
            return true;
        }

        private void Client_OnMessageReceived(SignalrUser user)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if( !user.message.Contains("Connected!") && !user.message.Contains("Reconnected!") && !user.message.Contains("undefined")  && !user.message.Contains("Disconnected!"))
                {
                    if (user.username == currentUser.Email) // EĞER GELEN MESAJ BENİM İSE
                    {
                        if (LastMessageOwnerId == currentUser.Id)
                        {
                            ChatPageViewModel chatPageViewModel = new ChatPageViewModel()
                            {
                                isMyMessage = true,
                                Message = user.message,
                                SendingTime = DateTime.Now,
                                isUserChangingPoint = false,
                                SenderUser = currentUser
                            };
                            //ChatPageViewModels.Add(new ChatPageViewModel()
                            //{
                            //    isMyMessage = true,
                            //    Message = user.message,
                            //    SendingTime = DateTime.Now,
                            //    isUserChangingPoint = false,
                            //    SenderUser = currentUser
                            //});
                            ChatPageViewModels.Add(chatPageViewModel);
                            
                        }
                        else
                        {
                            ChatPageViewModel chatPageViewModel = new ChatPageViewModel()
                            {
                                isMyMessage = true,
                                Message = user.message,
                                SendingTime = DateTime.Now,
                                isUserChangingPoint = true,
                                SenderUser = currentUser
                            };
                            //ChatPageViewModels.Add(new ChatPageViewModel()
                            //{
                            //    isMyMessage = true,
                            //    Message = user.message,
                            //    SendingTime = DateTime.Now,
                            //    isUserChangingPoint = true,
                            //    SenderUser = currentUser
                            //});
                            ChatPageViewModels.Add(chatPageViewModel);
                        }
                        LastMessageOwnerId = currentUser.Id;

                    }
                    else // EĞER GELEN MESAJ BENİM DEĞİL İSE
                    {
                        if (user.username == messagesPageViewModel.otherUserEmail)
                        {
                            if (LastMessageOwnerId == messagesPageViewModel.OtherUser.Id)
                            {
                                ChatPageViewModel chatPageViewModel = new ChatPageViewModel()
                                {
                                    isMyMessage = false,
                                    Message = user.message,
                                    SendingTime = DateTime.Now,
                                    isUserChangingPoint = false,
                                    SenderUser = messagesPageViewModel.OtherUser
                                };
                              
                                ChatPageViewModels.Add(chatPageViewModel);
                            }
                            else
                            {
                                ChatPageViewModel chatPageViewModel = new ChatPageViewModel()
                                {
                                    isMyMessage = false,
                                    Message = user.message,
                                    SendingTime = DateTime.Now,
                                    isUserChangingPoint = true,
                                    SenderUser = messagesPageViewModel.OtherUser
                                };
                               
                                ChatPageViewModels.Add(chatPageViewModel);
                            }
                            LastMessageOwnerId = messagesPageViewModel.OtherUser.Id;
                        }


                    }
                    colMessages.ItemsSource = null;
                    colMessages.ItemsSource = ChatPageViewModels;
                    colMessages.ScrollTo(ChatPageViewModels.Last(), -1, ScrollToPosition.End, true);

                }
            });
        }

        private void Client_ConnectionError()
        {
            DisplayAlert("Connection", "Error", "OK");
        }

        void SendMessage(object sender, EventArgs e)
        {
            //EĞER MESAJLAR SAYFASINDAN ULAŞILIYORSA CHANNEL VAR İSE  ZATEN CHANNEL GELECEK.
            //AKSİ TAKDİRDE, İLAN DETAYINDAN ULAŞILIYOR İSE CHANNEL GELMEYECEĞİ İÇİN CHANNEL YOK İSE,OLUŞTURMADAN ÖNCE KONTROL EDİLMELİ.

            string receiver = messagesPageViewModel.otherUserEmail;
            //if( currentUser.Id == messagesPageViewModel.ot ) 
            //string msgText;

            if ( receiver != currentUser.Email)
            {
                currentMessageText = txtMessage.Text;
                Device.BeginInvokeOnMainThread(() =>
                {
                    client.SendMessage(_username, txtMessage.Text, receiver);
                    txtMessage.Text = "";
                    //txtMessage.Focus();
                });



                CheckChannel();
            }
            else
            {
                DisplayAlert("Upps!", "Kendinizle konuşamazsınız.Felsefi olarak bakıyorsanız başka tabii :)", "OK");
            }

            


            //SIGNALR

            //SIGNALR

        }

        private async void CheckChannel()
        {
            if ( mainChannel != null)
            {
              
                ChatChannelUser channelUser = mainChannel.ChatChannelUsers.Where(x => x.UserId == currentUser.Id).FirstOrDefault();
                ChatMessage message = new ChatMessage();
                message.ChatChannelUserId = channelUser.Id;
                message.CreatedOn = DateTime.Now;
                message.isCorrect = true;
                message.isRead = false;
                message.Text = currentMessageText;
                await messageService.SendMessage(message);


            }
            else
            {
                ChatChannel channel = null;
                if ( playerAdvert!=null && opponentAdvert == null)
                {
                    channel = await messageService.GetChatChannel(playerAdvert.Id, playerAdvert.UserId, currentUser.Id, "Player");
                }
                else if (playerAdvert == null && opponentAdvert != null)
                {
                    channel = await messageService.GetChatChannel(opponentAdvert.Id, Convert.ToInt32(opponentAdvert.ownerId), currentUser.Id, "Opponent");
                }

                if ( channel != null)
                {
                    mainChannel = channel;

                    List<ChatChannelUser> channelUsers = mainChannel.ChatChannelUsers;
                    ChatChannelUser channelUser = mainChannel.ChatChannelUsers.Where(x => x.UserId == currentUser.Id).FirstOrDefault();
                    ChatMessage message = new ChatMessage();
                    message.ChatChannelUserId = channelUser.Id;
                    message.CreatedOn = DateTime.Now;
                    message.isCorrect = true;
                    message.isRead = false;
                    message.Text = currentMessageText;
                    await messageService.SendMessage(message);

                }
                else
                {
                    if (playerAdvert != null && opponentAdvert == null)
                    {
                        channel = await messageService.CreateChannelAndSendMessage(playerAdvert.Id, playerAdvert.UserId, currentUser.Id, "Player", currentMessageText);
                    }
                    else if (playerAdvert == null && opponentAdvert != null)
                    {
                        channel = await messageService.CreateChannelAndSendMessage(opponentAdvert.Id, Convert.ToInt32(opponentAdvert.ownerId), currentUser.Id, "Opponent", currentMessageText);
                    }
                    if ( channel != null)
                    {
                        mainChannel = channel;
                    }
                }

            }
        }

        private async void TapSeeAdvert_Tapped(object sender, EventArgs e)
        {
            if ( messagesPageViewModel.PlayerAdvert != null && messagesPageViewModel.OpponentAdvert == null )
            {
                if ( messagesPageViewModel.PlayerAdvert.UserId == currentUser.Id)
                {

                    messagesPageViewModel.PlayerAdvert.User = currentUser;
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(messagesPageViewModel.PlayerAdvert));
                }
                else
                {
                    messagesPageViewModel.PlayerAdvert.User = messagesPageViewModel.OtherUser;
                    await Navigation.PushAsync(new PlayerAdvertDetailsPage(messagesPageViewModel.PlayerAdvert));
                }

                
            }
            else if (messagesPageViewModel.PlayerAdvert == null && messagesPageViewModel.OpponentAdvert != null)
            {
                OpponentAdvertMenuViewModel vm = new OpponentAdvertMenuViewModel() { Advert = messagesPageViewModel.OpponentAdvert };
                await Navigation.PushAsync(new OpponentAdvertDetailsPage(vm));
            }
            
        }
    }
}