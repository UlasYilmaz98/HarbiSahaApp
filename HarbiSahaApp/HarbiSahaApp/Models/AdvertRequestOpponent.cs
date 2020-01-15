using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models
{
    public class AdvertRequestOpponent
    {
        
        public int Id { get; set; }

        public int OpponentAdvertId { get; set; }

        public virtual OpponentAdvert OpponentAdvert { get; set; }

        public string isApprovedSituation { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RandomLine1 { get; set; }

        public string RandomLine2 { get; set; }

        public string RandomLine3 { get; set; }

        public string RandomLine4 { get; set; }

        public string RandomLine5 { get; set; }

        public string RandomLine6 { get; set; }

    }
}
