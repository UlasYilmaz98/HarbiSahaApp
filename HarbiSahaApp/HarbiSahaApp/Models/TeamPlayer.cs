using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class TeamPlayer
    {
       
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int UserId { get; set; }

        public virtual Team Team { get; set; }

        public virtual User User { get; set; }



    }
}