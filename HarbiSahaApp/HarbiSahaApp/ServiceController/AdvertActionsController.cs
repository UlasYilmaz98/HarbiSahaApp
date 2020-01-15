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
    public class AdvertActionsController
    {
        public async Task<HttpClient> GetClient()
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (/*Application.Current.Properties.Keys.Contains("token")*/ CrossSecureStorage.Current.HasKey("token"))
            {
                //var token = Application.Current.Properties["token"];
                string token = CrossSecureStorage.Current.GetValue("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic" , token);
            }
            return client;
        }

        public async Task<HttpClient> GetFirstClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<MyAllAdvertsModel> GetMyAdverts()
        {
            try
            {
                MyAllAdvertsModel model = new MyAllAdvertsModel();
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetMyAdverts?userId=" + userId;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<MyAllAdvertsModel>((result));
                return model;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<List<MixedAdvertsViewModel>> GetMyClosedAdverts()
        {
            try
            {
                List<MixedAdvertsViewModel> modelList = new List<MixedAdvertsViewModel>();
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetMyClosedAdverts?userId=" + userId;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                modelList = JsonConvert.DeserializeObject<List<MixedAdvertsViewModel>>((result));
                return modelList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<MyAllRequestsModel> GetMyRequests()
        {
            try
            {
                MyAllRequestsModel model = new MyAllRequestsModel();
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/AdvertRequest/GetMyRequests?userId=" + userId;
                //var client = await GetClient();
                var client = await GetFirstClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<MyAllRequestsModel>((result));
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<MixedRequestsViewModel>> GetMyClosedRequests()
        {
            try
            {
                List<MixedRequestsViewModel> modelList = new List<MixedRequestsViewModel>();
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetMyClosedRequests?userId=" + userId;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                modelList = JsonConvert.DeserializeObject<List<MixedRequestsViewModel>>((result));
                return modelList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        
        public async Task<PlayerAdvert> GetMySinglePlayerAdvert(int advertId)
        {

            try
            {
                PlayerAdvert model = new PlayerAdvert();
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/GetMySinglePlayerAdvertWithRequests?userId=" + userId + "&advertId=" + advertId;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<PlayerAdvert>((result));
                return model;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> AnswerPlayerRequest(int AdvertRequestId, string isAccepted)
        {
            try
            {
                string response;
                User currentUser = new User();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId;
                userId = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/PlayerAdvert/AnswerPlayerRequest?AdvertRequestId=" + AdvertRequestId + "&isAccepted=" + isAccepted;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                response = JsonConvert.DeserializeObject<string>((result));
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> DeletePlayerAdvertRequest(AdvertRequestPlayer request)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                try
                {
                    User currentUser = App.Current.Properties["loggedUser"] as User;

                    var input = $"https://www.harbisaha.com/api/AdvertRequest/DeletePlayerAdvertRequest?playerAdvertRequestId=" + request.Id + " &userId=" + currentUser.Id;
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

        public async Task<string> DeleteOpponentAdvertRequest(AdvertRequestOpponent request)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                try
                {
                    User currentUser = App.Current.Properties["loggedUser"] as User;

                    var input = $"https://www.harbisaha.com/api/AdvertRequest/DeletePlayerAdvertRequest?opponentAdvertRequestId=" + request.Id + " &userId=" + currentUser.Id;
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
