using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class CreateOpponentAdvertViewModel
    {
        public int OwnerId { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public TimeSpan MinTime { get; set; }

        public TimeSpan MaxTime { get; set; }

        public int teamId { get; set; }

        public int MinTimeInt
        {
            get
            {

                return MinTime.Hours * 100 + MinTime.Minutes;
            }
        }

        public int MaxTimeInt
        {
            get
            {

                return MaxTime.Hours * 100 + MaxTime.Minutes;
            }
        }

        public string City { get; set; }

        public string District1 { get; set; }

        public string District2 { get; set; }

        public string District3 { get; set; }
        public string District4 { get; set; }


        public string AdvertInfos { get; set; }
    }
}
