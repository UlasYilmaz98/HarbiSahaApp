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
    public class ManageMatchServices
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

        public async Task<List<Field>> GetFields()
        {

            try
            {
                var input = $"https://www.harbisaha.com/api/Field/getFields";
                //var client = await GetClient();
                var client = await GetFirstClient();
                var result = await client.GetStringAsync(input);
                List<Field> response = JsonConvert.DeserializeObject<List<Field>>((result));
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<Match>> getFieldMathces(Field field)
        {
            Field currentField = field;
            int fz1Id = currentField.FieldZones[0].Id;
            int fz2Id = 0, fz3Id = 0, fz4Id = 0, fz5Id = 0;
            if (currentField.FieldZones.Count > 1)
                fz2Id = currentField.FieldZones[1].Id;
            if (currentField.FieldZones.Count > 2)
                fz3Id = currentField.FieldZones[2].Id;
            if (currentField.FieldZones.Count > 3)
                fz4Id = currentField.FieldZones[3].Id;
            if (currentField.FieldZones.Count > 4)
                fz5Id = currentField.FieldZones[4].Id;
            try
            {
                var input = $"https://www.harbisaha.com/api/Field/getFieldMathces?fz1Id="+fz1Id+"&fz2Id="+fz2Id+"&fz3Id="+fz3Id+"&fz4Id="+
                    fz4Id+"&fz5Id="+fz5Id;
                //var client = await GetClient();
                var client = await GetFirstClient();
                var result = await client.GetStringAsync(input);
                List<Match> response = JsonConvert.DeserializeObject<List<Match>>((result));
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


    }
}
