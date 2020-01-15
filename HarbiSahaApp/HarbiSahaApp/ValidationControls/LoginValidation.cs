using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.ValidationControls
{
    public class LoginValidation
    {
        

        public string ReturnSituation(string email,string password)
        {

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                return "E-posta ve şifre alanları doldurulmalıdır";
            }
            else if ( !email.Contains("@"))
            {
                return "Lütfen geçerli bir e-posta adresi girin";
            }
            else if ( password.Length < 6)
            {
                return "Şifre en az 6 karakter içermelidir.";
            }
            else
            {
                return "Success";
            }


        }

        

        //private bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


    }
}
