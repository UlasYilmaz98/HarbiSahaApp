using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class ChatChannel
    {


        public int Id { get; set; }
        public bool isChannelActive { get; set; }



        public int ChatChannelUser1Id { get; set; }

        public int ChatChannelUser2Id { get; set; }



        public virtual List<ChatChannelUser> ChatChannelUsers { get; set; }

        public DateTime CreatedOn { get; set; }

        public int playerAdvertId { get; set; }  // If this is not,then its 0

        public int opponentAdvertId { get; set; }

        public ChatChannel()
        {
            ChatChannelUsers = new List<ChatChannelUser>();
        }
    }
}
