using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class Notification
    {
       
        public int Id { get; set; }

     
        public int OwnerId { get; set; }

      
        public string Type { get; set; } // Player,Field,Admin

        public string Header { get; set; }

        public string NotificationContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Paragraph2 { get; set; }

        public string Paragraph3 { get; set; }

        public string PhotoPath { get; set; }

        public bool isOpened { get; set; }

        public string shortVersion { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }


    }
}
