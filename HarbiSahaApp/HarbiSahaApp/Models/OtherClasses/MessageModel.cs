using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.OtherClasses
{
    public class MessageModel
    {
        public ChatChannel Channel { get; set; }

        public string Text { get; set; }

        public int messageOwnerId { get; set; }


    }
}
