using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class Team
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string LogoPath { get; set; }



        public DateTime CreatedOn { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public int CaptainId { get; set; }

        public virtual List<MatchTeam> MatchTeams { get; set; }

        public virtual List<TeamInvite> TeamInvites { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public virtual List<TeamPlayer> TeamPlayers { get; set; }



        public Team()
        {

            MatchTeams = new List<MatchTeam>();
            TeamInvites = new List<TeamInvite>();
            TeamPlayers = new List<TeamPlayer>();
        }

        //Field



    }
}