using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class MyTeamPageViewModel
    {

        public User User { get; set; }

        public int CaptainId { get; set; }

        public bool isCaptain {
            get
            {
                if (User.Id == CaptainId)
                    return true;
                else
                    return false;

            }

        }

        public int Age { get
            {
                int Age;
                
                DateTime today = DateTime.Now;
                DateTime birthDay = new DateTime(User.BirthYear, User.BirthMonth, User.BirthDay);
                TimeSpan timeDiff = today.Subtract(birthDay);
                Age = timeDiff.Days / 365;
                return Age;


            } }

        public string Name
        {
            get
            {
                return User.Name;
            }
        }

        public string Surname
        {
            get
            {
                return User.Surname;
            }
        }

        public string ProfilePicturePath
        {
            get
            {
                return User.PhotoPath;
            }
        }

        public string FontSizeSurname
        {
            get
            {
                if (User.Surname.Length <= 10)
                    return "Large";            
                else
                    return "Medium";
            }
        }
        public string FontSizeName
        {
            get
            {
                if (User.Name.Length <= 18)
                    return "Small";
                else
                    return "Micro";
            }
        }

    }
}
