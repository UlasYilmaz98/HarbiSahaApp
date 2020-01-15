using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class MatchTeam
    {
       
        public int Id { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int OpponentMatchId { get; set; }

        public virtual OpponentMatch OpponentMatch { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public MatchTeam()
        {
            //MatchPlayers = new List<MatchPlayer>();
            //PlayerAdverts = new List<PlayerAdvert>();
            //OpponentAdverts = new List<OpponentAdvert>();
        }

    }
}