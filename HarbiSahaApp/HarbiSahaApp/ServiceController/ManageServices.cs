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
using Xamarin.Forms;

namespace HarbiSahaApp.ServiceController
{
    public class ManageServices
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

        public async Task<User> Login(string email, string password)
        {
            
            try
            {
                LoginModel loginModel = new LoginModel();
                CrossSecureStorage.Current.DeleteKey("token");
                var input = $"https://www.harbisaha.com/api/Logon/Login?email=" + email + "&password=" + password;
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                loginModel =  JsonConvert.DeserializeObject<LoginModel>((result));
                //string tok = loginModel.token.ToString().Substring(1, 556);
                string tok = loginModel.token.ToString();
                CrossSecureStorage.Current.SetValue("token", tok);
                User loggedUser = loginModel.currentUser;
                loggedUser.TeamPlayers.Add(loginModel.currentTeamPlayer);
                if ( loginModel.currentUser != null)
                    Application.Current.Properties["loggedUser"] = loggedUser;
                Application.Current.Properties["loggedUserTeamPlayer"] = loginModel.currentTeamPlayer;
                CrossSecureStorage.Current.SetValue("currentUserEmail", loggedUser.Email);
                await Application.Current.SavePropertiesAsync();
                return loggedUser;



            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RegisterModel> Register(RegisterModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //model.OwnerId = currentUser.Id;
                    client.BaseAddress = new Uri("https://www.harbisaha.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri("https://www.harbisaha.com/api/Logon/Register");
                    string serializedObject = JsonConvert.SerializeObject(model);
                    HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                    if (response.IsSuccessStatusCode)
                    {
                        RegisterModel modelToReturn = new RegisterModel();
                        var data = await response.Content.ReadAsStringAsync();
                        modelToReturn = JsonConvert.DeserializeObject<RegisterModel>(data);
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

        public async Task<User> GetSingleUser(int userId)
        {
            var input = $"https://www.harbisaha.com/api/User/GetSingleUser?UserId=" + userId;
            //var client = await GetClient();
            var client = await GetClient();
            var result = await client.GetStringAsync(input);
            User response = JsonConvert.DeserializeObject<User>((result));
            return response;
        }

        public async Task<Team> GetSingleTeamWithPlayers(int teamId)
        {
            var input = $"https://www.harbisaha.com/api/User/GetSingleTeamWithPlayers?teamId=" + teamId;
            //var client = await GetClient();
            var client = await GetClient();
            var result = await client.GetStringAsync(input);
            Team response = JsonConvert.DeserializeObject<Team>((result));
            return response;
        }

        public async Task<bool> CheckEmail(string email)
        {
            var input = $"https://www.harbisaha.com/api/User/CheckEmail?email=" + email;
            //var client = await GetClient();
            var client = await GetFirstClient();
            var result = await client.GetStringAsync(input);
            bool response = JsonConvert.DeserializeObject<bool>((result));
            return response;
        }

        public async Task<User> EditUserProfileInfos(User model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //model.OwnerId = currentUser.Id;
                    client.BaseAddress = new Uri("https://www.harbisaha.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string token = CrossSecureStorage.Current.GetValue("token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
                    var uri = new Uri("https://www.harbisaha.com/api/User/EditUserProfileInfos");
                    string serializedObject = JsonConvert.SerializeObject(model);
                    HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                    if (response.IsSuccessStatusCode)
                    {
                        User modelToReturn = new User();
                        var data = await response.Content.ReadAsStringAsync();
                        modelToReturn = JsonConvert.DeserializeObject<User>(data);
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

        public async Task<string> ChangeProfilePhoto(MultipartFormDataContent content,int id)
        {
          

            try
            {
                using (var client = new HttpClient())
                {
                    //model.OwnerId = currentUser.Id;
                    client.BaseAddress = new Uri("https://www.harbisaha.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri("https://www.harbisaha.com/api/User/ChangeProfilePhoto?userId=" + id);
                    //string serializedObject = JsonConvert.SerializeObject(model);
                    //HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Başarılı";
                    }
                    else
                    {
                        return "Başarısız";
                    }

                }
            }
            catch (Exception)
            {
                return "Başarısız";
                throw;
            }
        }

    }
}
