using HarbiSahaApp.Models.OtherClasses;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HarbiSahaApp
{
    public class SignalrClient
    {
        string url = "https://www.harbisaha.com/";
        HubConnection Connection;
        IHubProxy ChatHubProxy;

        public delegate void Error();
        public delegate void MessageReceived(SignalrUser user);

        public event Error ConnectionError;
        public event MessageReceived OnMessageReceived;



        public void Connect(string _username)
        {
            Connection = new HubConnection(url, new Dictionary
                <string, string>
            {
                { "username", _username }
            });

            Connection.StateChanged += Connection_StateChanged;

            ChatHubProxy = Connection.CreateHubProxy("ChatHub");

            ChatHubProxy.On<string, string>("MessageReceived",
                (username, message) =>
                {
                    var user = new SignalrUser
                    {
                        username = username,
                        message = message
                    };
                    OnMessageReceived?.Invoke(user);
                });

            Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    ConnectionError?.Invoke();
            });
        }

        public void SendMessage(string username, string message,string receiver)
        {
            ChatHubProxy.Invoke("SendMessage", username, message,receiver);
        }

        private Task Start()
        {
            return Connection.Start();
        }

        private void Connection_StateChanged(StateChange obj)
        {

        }


        //public void Connect(string _username)
        //{
        //    connection = new HubConnection(url,new Dictionary<string, string> {
        //        {"username",_username }
        //    });

        //    connection.StateChanged += Connection_StateChanged;

        //    ChatHubProxy = connection.CreateHubProxy("ChatHub");

        //    ChatHubProxy.On<string, string>("MessageReceived", (username, message) =>
        //    {
        //        var user = new SignalrUser()
        //        {
        //            Username = username,
        //            Message = message
        //        };
        //        OnMessageReceived?.Invoke(user);
        //    });

        //    Start().ContinueWith(task =>
        //   {
        //       if (task.IsFaulted)
        //       {
        //           var expection = task.Exception;
        //           ConnectionError?.Invoke();
        //       }


        //   });

        //}

        //public void SendMessage(string username,string message,string receiver)
        //{
        //    ChatHubProxy.Invoke("SendMessage",username,message,receiver);
        //}

        //private Task Start()
        //{
        //    return connection.Start();

        //}

        //private void Connection_StateChanged(StateChange obj)
        //{

        //}
    }
}
