using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class MatchPlayer
    {
        
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public bool isPlayed { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public MatchPlayer()
        {
            //OwnedRateForms = new List<RateForm>();
        }



    }
}