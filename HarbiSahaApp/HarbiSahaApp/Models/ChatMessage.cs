using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public int ChatChannelUserId { get; set; }

        public virtual ChatChannelUser ChatChannelUser { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isCorrect { get; set; }

        public bool isRead { get; set; }


    }
}
