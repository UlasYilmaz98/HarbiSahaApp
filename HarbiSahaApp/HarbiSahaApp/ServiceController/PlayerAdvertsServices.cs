using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
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
    public class PlayerAdvertsServices
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

        public async Task<List<PlayerAdvert>> GetPlayerAdverts()
        {
            List<PlayerAdvert> advertsList = new List<PlayerAdvert>();
            User currentUser = new User();
            int userId;
            string mainCity;

            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                currentUser = App.Current.Properties["loggedUser"] as User;
                userId = currentUser.Id;
                mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetPlayerAdverts?userId=" + userId + "&mainCity=" + "İstanbul";
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                advertsList = JsonConvert.DeserializeObject<List<PlayerAdvert>>((result));
            }
            else
            {
                userId = 1;
                mainCity = "İstanbul";
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetPlayerAdverts?userId=" + userId + "&mainCity=" + "İstanbul";
                //var client = await GetClient();
                var client = await GetFirstClient();
                var result = await client.GetStringAsync(input);
                advertsList = JsonConvert.DeserializeObject<List<PlayerAdvert>>((result));
            }
            try
            {
                return advertsList;
            }
            catch (Exception)
            {

                return null;
            }


        }

        
        public async Task<CreatePlayerAdvertViewModel> CreatePlayerAdvert(CreatePlayerAdvertViewModel model)
        {
            User currentUser = new User();
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                currentUser = App.Current.Properties["loggedUser"] as User;
                try
                {
                    using (var client = new HttpClient())
                    {
                        model.OwnerId = currentUser.Id;
                        client.BaseAddress = new Uri("http://www.harbisaha.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var uri = new Uri("https://www.harbisaha.com/api/PlayerAdvert/CreatePlayerAdvert");
                        string serializedObject = JsonConvert.SerializeObject(model);
                        HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            CreatePlayerAdvertViewModel modelToReturn = JsonConvert.DeserializeObject<CreatePlayerAdvertViewModel>(data);
                            return modelToReturn;
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
            else
            {
                return null;
            }
        }



        public async Task<List<PlayerAdvert>> FilterPlayerAdverts(FilterModelPlayerAdvert model)
        {
            try
            {
                //oginModel loginModel = new LoginModel();
                //CrossSecureStorage.Current.DeleteKey("token");
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/FilterPlayerAdverts";
                var client = await GetFirstClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string serializedObject = JsonConvert.SerializeObject(model);

                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(input, contentPost);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    List<PlayerAdvert> modelToReturn = JsonConvert.DeserializeObject<List<PlayerAdvert>>(data);
                    return modelToReturn;
                }
                else
                {
                    return null;
                }



            }
            catch (Exception)
            {

                throw;
            }


            
        }

        public async Task<string> SendPlayertAdvertReques(int userId,int playerAdvertId)
        {
            //currentUser = App.Current.Properties["loggedUser"] as User;
            
            var input = $"https://www.harbisaha.com/api/PlayerAdvert/CreatePlayerAdvertRequest?userId=" + userId + "&playerAdvertId=" + playerAdvertId;
            //var client = await GetClient();
            var client = await GetClient();
            var result = await client.GetStringAsync(input);
            string responseMessage = JsonConvert.DeserializeObject<string>((result));
            return responseMessage;
        } 

        public async Task<CreateOpponentAdvertViewModel> CreateOpponentAdvert(CreateOpponentAdvertViewModel model)
        {
            User currentUser = new User();
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                currentUser = App.Current.Properties["loggedUser"] as User;
                TeamPlayer teamPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                model.OwnerId = currentUser.Id;
                model.teamId = teamPlayer.TeamId;
                try
                {
                    using (var client = new HttpClient())
                    {
                        model.OwnerId = currentUser.Id;
                        client.BaseAddress = new Uri("http://www.harbisaha.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var uri = new Uri("http://www.harbisaha.com/api/OpponentAdvert/CreateOpponentAdvert");
                        string serializedObject = JsonConvert.SerializeObject(model);
                        HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            CreateOpponentAdvertViewModel modelToReturn = JsonConvert.DeserializeObject<CreateOpponentAdvertViewModel>(data);
                            return modelToReturn;
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
            else
            {
                return null;
            }
        }

        public async Task<List<OpponentAdvert>> GetMainOpponentAdverts(int number)
        {
            if( App.Current.Properties.ContainsKey("loggedUser"))
            {
                var input = $"https://www.harbisaha.com/api/OpponentAdvert/GetIndexOpponentAdverts?number=" + number;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                List<OpponentAdvert> response = JsonConvert.DeserializeObject<List<OpponentAdvert>>((result));
                return response;
            }
            else
            {
                try
                {
                    var input = $"https://www.harbisaha.com/api/OpponentAdvert/GetIndexOpponentAdverts?number=" + number;
                    //var client = await GetClient();
                    var client = await GetFirstClient();
                    var result = await client.GetStringAsync(input);
                    List<OpponentAdvert> response = JsonConvert.DeserializeObject<List<OpponentAdvert>>((result));
                    return response;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            
        }


        public async Task<List<OpponentAdvert>> GetFilteredOpponentAdverts(FilterModelOpponentAdverts filterModel)
        {
            try
            {
                //oginModel loginModel = new LoginModel();
                //CrossSecureStorage.Current.DeleteKey("token");
                var input = $"https://www.harbisaha.com/api/OpponentAdvert/OpponentAdvertsFilterDetailed";
                var client = await GetFirstClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string serializedObject = JsonConvert.SerializeObject(filterModel);

                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(input, contentPost);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    List<OpponentAdvert> modelToReturn = JsonConvert.DeserializeObject<List<OpponentAdvert>>(data);
                    return modelToReturn;
                }
                else
                {
                    return null;
                }



            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> DeletePlayerAdvert(PlayerAdvert advert)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                try
                {
                    User currentUser = App.Current.Properties["loggedUser"] as User;

                    var input = $"https://www.harbisaha.com/api/PlayerAdvert/DeletePlayerAdvert?playerAdvertId=" + advert.Id + " &userId=" + currentUser.Id;
                    //var client = await GetClient();
                    var client = await GetClient();
                    var result = await client.GetStringAsync(input);
                    string response = JsonConvert.DeserializeObject<string>((result));
                    return "OK";
                }
                catch (Exception)
                {

                    return "Bir sorun oluştu";
                }
            }
            else
            {
                return "Önce giriş yapmalısınız.";
            }
        }
    }
}

