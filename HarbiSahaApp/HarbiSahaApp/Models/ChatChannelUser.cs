using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class ChatChannelUser
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ChatChannelId { get; set; }

        public virtual ChatChannel ChatChannel { get; set; }

        public virtual User User { get; set; }

        public virtual List<ChatMessage> ChatMessages { get; set; }

        public ChatChannelUser()
        {
            ChatMessages = new List<ChatMessage>();
        }

    }
}
