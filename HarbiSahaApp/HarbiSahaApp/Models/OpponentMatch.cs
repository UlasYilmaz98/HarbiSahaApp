using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class OpponentMatch
    {
     
        public int Id { get; set; }

        public virtual List<MatchTeam> MatchTeams { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public OpponentMatch()
        {
            MatchTeams = new List<MatchTeam>();
        }


    }
}
