using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.OtherClasses
{
    public class LoginModel
    {
        public User currentUser { get; set; }

        public string token { get; set; }

        public TeamPlayer currentTeamPlayer { get; set; }


    }
}
