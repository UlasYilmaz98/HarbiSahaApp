using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class OpponentAdvert
    {
      
        public int Id { get; set; }

        public string ownerId { get; set; }

        public string ownerUserName { get; set; }

        //public int Team_Id { get; set; }

        public virtual Team Team { get; set; }

        public bool isOpen { get; set; }

        public DateTime CreatedOn { get; set; }

        //public int MatchId { get; set; }

        //public virtual Match Match { get; set; }

        public string City { get; set; }

        public string District1 { get; set; }

        public string District2 { get; set; }

        public string District3 { get; set; }

        public string District4 { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int AvailableMinTime { get; set; }

        public int AvailableMaxTime { get; set; }

        public int MinDay { get; set; } //minDay

        public int MaxDay { get; set; } // maxDay

        public int MinMonth { get; set; } //minDayMonth

        public int MaxMonth { get; set; } //maxDayMonth

        public string RandomLine5 { get; set; } //ProfilePicturePath

        public string RandomLine6 { get; set; }  //Info

        public DateTime DateOfAdvert { get; set; }

        public virtual List<AdvertRequestOpponent> AdvertRequestOpponents { get; set; }


        public OpponentAdvert()
        {

            AdvertRequestOpponents = new List<AdvertRequestOpponent>();
        }

    }
}