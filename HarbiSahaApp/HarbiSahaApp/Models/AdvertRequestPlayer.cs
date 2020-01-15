using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class AdvertRequestPlayer
    {
        
        public int Id { get; set; }

        public int PlayerAdvertId { get; set; }

        public virtual PlayerAdvert PlayerAdvert { get; set; }

        public string isApprovedSituation { get; set; } //Waiting,Approved,Denied -- Beklemede,Kabul Edildi,İptal Edildi.

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }



    }
}
