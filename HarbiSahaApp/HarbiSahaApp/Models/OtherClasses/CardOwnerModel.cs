using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.OtherClasses
{
    public class CardOwnerModel
    {
        //int matchId,int fieldId, string paymentCost, string cardNumber, string cardOwnerName, string cardExpMonth,
        //  string cardExpYear,string cardCVC
        public int matchId { get; set; }

        public int fieldId { get; set; }

        public string paymentCost { get; set; }

        public string cardNumber { get; set; }

        public string cardOwnerName { get; set; }

        public string cardExpMonth { get; set; }

        public string cardExpYear { get; set; }

        public string cardCVC { get; set; }
    }
}
