using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class CreatePlayerAdvertViewModel
    {
        public int OwnerId { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public TimeSpan MatchStart { get; set; }

        public int MatchStartInt {
            get
            {
                
                return MatchStart.Hours * 100 + MatchStart.Minutes;
            }
        }

        public string City { get; set; }

        public string Town { get; set; }

        public string FieldName { get; set; }

        public string BaseAdress { get; set; }
     
        public int  NumberOfAdverts { get; set; }

        public string NeededPosition1 { get; set; }

        public string NeededPosition2 { get; set; }

        public string NeededPosition3 { get; set; }

        public string AdvertInfos { get; set; }

        public string Cost { get; set; }


    }
}
