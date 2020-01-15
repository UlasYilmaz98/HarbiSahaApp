using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HarbiSahaApp.ServiceController
{
    public class MessagingServices
    {
        public async Task<HttpClient> GetClient()
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (/*Application.Current.Properties.Keys.Contains("token")*/ CrossSecureStorage.Current.HasKey("token"))
            {
                //var token = Application.Current.Properties["token"];
                string token = CrossSecureStorage.Current.GetValue("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            }
            return client;
        }

        public async Task<HttpClient> GetFirstClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<ChatChannel> GetChatChannel(int advertId, int user1Id, int user2Id, string type)
        {
            try
            {
                ChatChannel model = new ChatChannel();
                var input = $"https://www.harbisaha.com/api/Messaging/GetChatChannel?advertId=" + advertId + "&user1Id=" + user1Id + "&user2Id=" + user2Id + "&type=" + type;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<ChatChannel>((result));
                
                return model;
            }
            catch (Exception EX)
            {
                //return null;
                throw;
            }
        }

        public async Task<ChatChannel> CreateChannelAndSendMessage(int advertId, int ownerUserId, int requesterUserId, string type,string message)
        {
            try
            {
                ChatChannel model = new ChatChannel();
                var input = $"https://www.harbisaha.com/api/Messaging/CreateChannelAndSendMessage?advertId=" + advertId + "&ownerUserId=" + ownerUserId + "&requesterUserId=" + requesterUserId + "&type=" + type + "&message=" + message;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<ChatChannel>((result));

                return model;
            }
            catch (Exception EX)
            {
                //return null;
                throw;
            }
        }

        public async Task<string> SendMessage(ChatMessage message)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //model.OwnerId = currentUser.Id;
                    client.BaseAddress = new Uri("https://www.harbisaha.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri("https://www.harbisaha.com/api/Messaging/SendMessageDB");
                    string serializedObject = JsonConvert.SerializeObject(message);
                    HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseMessage;
                        var data = await response.Content.ReadAsStringAsync();
                        responseMessage  = JsonConvert.DeserializeObject<string>(data);
                        return responseMessage;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }  

        public async Task<List<ChannelAdvertModel>> GetMyChatChannels()
        {
            try
            {
                User currentUser = App.Current.Properties["loggedUser"] as User;
                List<ChannelAdvertModel> model = new List<ChannelAdvertModel>();
                var input = $"https://www.harbisaha.com/api/Messaging/GetMyChatChannels?userId=" + currentUser.Id ;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<List<ChannelAdvertModel>>((result));

                return model;
            }
            catch (Exception EX)
            {
                //return null;
                throw;
            }
        }

        public async Task<ChatChannel> GetChannel(int channelId)
        {
            try
            {
                //User currentUser = App.Current.Properties["loggedUser"] as User;
                ChatChannel model = new ChatChannel();
                var input = $"https://www.harbisaha.com/api/Messaging/GetChannelMessages?channelId=" + channelId;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<ChatChannel>((result));

                return model;
            }
            catch (Exception EX)
            {
                //return null;
                throw;
            }
        }

        

        




    }
}
