using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HarbiSahaApp.ServiceController
{
    public class SmsServices
    {

        public string GenerateRandomPass(string phoneNumber)
        {
            if (phoneNumber[0] == '0')
            {
                phoneNumber = phoneNumber.Remove(0, 1);
            }
            else if (phoneNumber[0] == '+')
            {
                phoneNumber = phoneNumber.Remove(0, 3);
            }
            Random rnd = new Random();
            string html = string.Empty;
            
            //33
            string code = String.Empty;
            for( int i=0;i<6;i++)
            {
                int current = rnd.Next(10);
                code = code.Insert(i, current.ToString());
            }


            string url = @"https://api.netgsm.com.tr/sms/send/get/?usercode=8503055095&password=ULAS212&gsmno="+ phoneNumber + "&message=Tek%20kullanımlık%20kod:%20 " + code + "  &msgheader=8503055095&dil=TR";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return code;
        }

    }
}
