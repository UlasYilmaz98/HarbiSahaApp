using HarbiSahaApp.ServiceController;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HarbiSahaApp.ValidationControls
{
    public class RegisterValidation
    {

        ManageServices service = new ManageServices();

        public bool EmailValidation(string email)
        {
            if (!email.Contains("@"))
            {
                return false;
            }
            return true;
        }

        //public async string EmailFinishValidation(string email)
        //{
        //    if (!email.Contains("@"))
        //    {
        //        return "Lütfen geçerli bir E-posta adresi girin";
        //    }
        //    else
        //    {
        //        if (await service.CheckEmail(email))
        //        {
        //            return "Success";
        //        }
        //        else
        //            return "Bu E-posta adresi kullanılıyor";
        //    }
        //}

        public string PasswordValidation(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "Şifre alanı doldurulmalıdır.";
            }          
            else if (password.Length < 6)
            {
                return "Şifre en az 6 karakter içermelidir.";
            }            
            else
            {
                return "Success";
            }
        }

        public bool RePasswordValidation(string password,string rePassword)
        {
            if (password != rePassword)
            {
                return false;
            }
            return true;
        }

        public async Task<string> ReturnSituationStep1(string email, string password,string rePassword)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                return "E-posta ve şifre alanları doldurulmalıdır";
            }
            else if (!email.Contains("@"))
            {
                return "Lütfen geçerli bir e-posta adresi girin";
            }
            else if (password.Length < 6)
            {
                return "Şifre en az 6 karakter içermelidir.";
            }
            else if ( password != rePassword)
            {
                return "Şifreler aynı değil";
            }  
            else if ( await isEmailUsed(email) == true)
            {
                return "E-posta adresi kullanılıyor.";
            }
            else
            {
                return "Success";
            }


        }

        public async Task<bool> isEmailUsed(string email)
        {
            bool isUsed;
            isUsed = await service.CheckEmail(email);
            return isUsed;
        }


    }
}
