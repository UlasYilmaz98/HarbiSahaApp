using HarbiSahaApp.Models.OtherClasses;
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
    public partial class ChatPageSample : ContentPage
    {
        string _username;
        IList<SignalrUser> model = new ObservableCollection<SignalrUser>();
        SignalrClient client = new SignalrClient();

        public ChatPageSample(string username)
        {
            InitializeComponent();
            _username = username;
            client.Connect(_username);
            client.ConnectionError += Client_ConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;

            this.BindingContext = model;

        }

        private void Client_OnMessageReceived(SignalrUser user)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                model.Add(user);
            });
        }

        private void Client_ConnectionError()
        {
            DisplayAlert("Connection", "Error", "Ok");
        }

        void SendMessage(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                client.SendMessage(_username, txtMessage.Text,"chrome");
                txtMessage.Text = "";
                txtMessage.Focus();
            });
        }
    }
}