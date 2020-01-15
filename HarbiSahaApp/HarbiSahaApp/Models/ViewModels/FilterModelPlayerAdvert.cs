using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class FilterModelPlayerAdvert
    {

        public string OwnerName { get; set; }

        public string City { get; set; }

        public List<string> Discricts { get; set; }

        public TimeSpan MinimumTime { get; set; }

        public TimeSpan MaximumTime { get; set; }

        public int MinimumTimeInt
        {

            get
            {
                int hour, minute, total;
                hour = MinimumTime.Hours * 100;
                minute = MinimumTime.Minutes;
                total = hour + minute;
                return total;
            }

        }

        public int MaximumTimeInt
        {
            get
            {
                int hour, minute, total;
                hour = MaximumTime.Hours * 100;
                minute = MaximumTime.Minutes;
                total = hour + minute;
                return total;
            }
        }


        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
