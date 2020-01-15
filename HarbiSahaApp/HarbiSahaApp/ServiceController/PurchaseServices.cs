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
    public class PurchaseServices
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

        //int userId,int fieldId,int matchId,string paymentCost,string cardNumber,string cardOwnerName,string cardExpMonth,
        //    string cardExpYear,string cardCVC

        public async Task<PurchaseModel> PurchaseStart(int matchId,int fieldId, string paymentCost, string cardNumber, string cardOwnerName, string cardExpMonth,
          string cardExpYear,string cardCVC)
        {

            try
            {
                User currentUser = App.Current.Properties["loggedUser"] as User;
                PurchaseModel model = new PurchaseModel();
                var input = $"https://www.harbisaha.com/api/Purchase/PurchaseOpen?userId=" + currentUser.Id + "&fieldId=" + fieldId +
                    "&matchId=" + matchId + "&paymentCost=" + paymentCost + "&cardNumber=" + cardNumber + "&cardOwnerName=" + cardOwnerName +
                    "&cardExpMonth=" + cardExpMonth + "&cardExpYear=" + cardExpYear + "&cardCVC=" + cardCVC;
                //var client = await GetClient();
                //string input1 = "https://www.harbisaha.com/api/Purchase/PurchaseOpen?userId=2&fieldId=2&matchId=175&paymentCost=4&cardNumber=4543147984578315&cardOwnerName=ULA%C5%9E%20YILMAZ&cardExpMonth=04&cardExpYear=24&cardCVC=635";
                var client = await GetClient();
                var result = await client.GetStringAsync(input);
                model = JsonConvert.DeserializeObject<PurchaseModel>((result));

                return model;
            }
            catch (Exception EX)
            {
                //return null;
                string message = EX.Message;
                throw;
            }

        }
    }


}
