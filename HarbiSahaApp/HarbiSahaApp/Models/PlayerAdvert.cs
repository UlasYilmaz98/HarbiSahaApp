using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class PlayerAdvert
    {
        public int Id { get; set; }

        public string NeededPosition { get; set; }

        public bool isOpen { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        //public virtual List<AdvertRequest> AdvertRequests { get; set; }

        public virtual List<AdvertRequestPlayer> AdvertRequestPlayers { get; set; }

        public string City { get; set; }

        public string Town { get; set; }

        public string BaseAdress { get; set; }

        public string FieldName { get; set; }

        public string Neighborhood { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int MatchTime { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public DateTime DateOfAdvert { get; set; }





        public PlayerAdvert()
        {
            AdvertRequestPlayers = new List<AdvertRequestPlayer>();

        }


    }
}