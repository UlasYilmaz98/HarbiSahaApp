using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class User
    {
       
        public int Id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ProfileMessage { get; set; }

        public bool isConfirmed { get; set; }

        public DateTime CreatedOn { get; set; }

        public int BirthYear { get; set; }

        public int BirthMonth { get; set; }

        public int BirthDay { get; set; }

        public int Height { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PhotoPath { get; set; }

        public string Position1 { get; set; }

        public string Position2 { get; set; }

        public string Position3 { get; set; }

        public string City { get; set; }

        public string DistrictList { get; set; }

        public string TeamName { get; set; }


        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

        public int CountPlayedMathes { get; set; }

        public int OverallStarPoint { get; set; }

        public int OverallPoint { get; set; }

        public virtual List<RateForm> RateForms { get; set; }

        public virtual List<PlayerAdvert> PlayerAdverts { get; set; }

        public virtual List<Match> OwnedMatches { get; set; }

        public virtual List<AdvertRequestPlayer> AdvertRequestPlayers { get; set; }

        public virtual List<AdvertRequestOpponent> AdvertRequestOpponents { get; set; }
        public virtual List<TeamPlayer> TeamPlayers { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public virtual List<Complain> Complains { get; set; }

        public virtual List<TeamInvite> TeamInvites { get; set; }
        public virtual List<ChatChannelUser> ChatChannelUsers { get; set; }

        //public Team Team { get
        //    {
        //        return TeamPlayers[0].Team;


        //    } }
        public User()
        {
            RateForms = new List<RateForm>();
            OwnedMatches = new List<Match>();
            PlayerAdverts = new List<PlayerAdvert>();
            //AdvertRequests = new List<AdvertRequest>();
            AdvertRequestPlayers = new List<AdvertRequestPlayer>();
            AdvertRequestOpponents = new List<AdvertRequestOpponent>();
            Complains = new List<Complain>();
            TeamInvites = new List<TeamInvite>();
            TeamPlayers = new List<TeamPlayer>();
            ChatChannelUsers = new List<ChatChannelUser>();
        }


    }
}