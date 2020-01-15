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
    public class TeamServices
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

        public async Task<CreateTeamModel> CreateTeam(string fullName,string shortName,string inviteEmail)
        {
            User currentUser = new User();
            try
            {
                CreateTeamModel model = new CreateTeamModel();
                Team teamToRetruned = new Team();
                TeamPlayer tmPlayer = new TeamPlayer();
                currentUser = App.Current.Properties["loggedUser"] as User;
                int userId  = currentUser.Id;
                //mainCity = currentUser.City;
                var input = $"https://www.harbisaha.com/api/OpponentAdvert/CreateNewTeam?userId=" + userId + "&fullName=" + fullName + "&shortName=" + shortName + "&inviteEmail=" + inviteEmail;
                //var client = await GetClient();
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<CreateTeamModel>((result));
                if ( tmPlayer.Team != null)
                {
                    App.Current.Properties["loggedUserTeamPlayer"] = model.TeamPlayer;
                }
                return model;
            }
            catch (Exception EX)
            {
                //return null;
                throw;
            }
            


        }

        public async Task<List<TeamPlayer>> GetTeamplayers()
        {
            User currentUser = new User();
            if ( App.Current.Properties["loggedUserTeamPlayer"] == null)
            {
                return null;
            }
            else
            {
                try
                {
                    List<TeamPlayer> playerList = new List<TeamPlayer>();
                    TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                    var input = $"https://www.harbisaha.com/api/OpponentAdvert/GetTeamPlayers?teamId=" + currentPlayer.TeamId;
                    var client = await GetClient();
                    var result = await client.GetStringAsync(input);
                    playerList = JsonConvert.DeserializeObject<List<TeamPlayer>>((result));
                    return playerList;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<string> LeaveFromTeam()
        {
            User currentUser = App.Current.Properties["loggedUser"] as User;
            TeamPlayer currentTeamPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
            if ( currentTeamPlayer != null)
            {
                try
                {
                    string responseMessage;
                    //List<TeamPlayer> playerList = new List<TeamPlayer>();
                    //TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                    var input = $"https://www.harbisaha.com/api/OpponentAdvert/LeaveFromTeam?teamPlayerId=" + currentTeamPlayer.Id;
                    var client = await GetClient();
                    var result = await client.GetStringAsync(input);
                    responseMessage = JsonConvert.DeserializeObject<string>((result));
                    if (responseMessage == "Başarılı")
                        return "Takımdan Ayrıldınız.";
                    else
                        return "Hata oluştu";


                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return "Hata oluştu";
        }

        public async Task<string> ThrowFromTeam(int userId)
        {

            try
            {
                string responseMessage;
                //List<TeamPlayer> playerList = new List<TeamPlayer>();
                //TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                var input = $"https://www.harbisaha.com/api/OpponentAdvert/ThrowFromTeam?userId=" + userId;
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                responseMessage = JsonConvert.DeserializeObject<string>((result));
                if (responseMessage == "Başarılı")
                    return "Takımdan Ayrıldınız.";
                else
                    return "Hata oluştu";


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> SwitchCaptain(int newCaptainId)
        {
            
            try
            {
                TeamPlayer tmPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                Team team = tmPlayer.Team;
                string responseMessage;
                //List<TeamPlayer> playerList = new List<TeamPlayer>();
                //TeamPlayer currentPlayer = App.Current.Properties["loggedUserTeamPlayer"] as TeamPlayer;
                var input = $"https://www.harbisaha.com/api/OpponentAdvert/SwitchCaptain?teamId=" + team.Id + "&newCaptainId=" + newCaptainId;
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                responseMessage = JsonConvert.DeserializeObject<string>((result));
                if (responseMessage == "Başarılı")
                    return "Kaptan Değişti!";
                else
                    return "Hata oluştu";

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
