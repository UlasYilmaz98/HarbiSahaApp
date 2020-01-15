using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.OtherClasses
{
    public class FilterModelOpponentAdverts
    {

        public string City { get; set; }

        public TimeSpan Time { get; set; }

        public string Town { get; set; }

        public DateTime Date { get; set; }

        public int GetTime
        {
            get
            {
                int Minutes,Hours;
                Minutes = Time.Minutes;
                Hours = Time.Hours;

                return Hours * 100 + Minutes;
            }
        }

        public int Day
        {
            get
            {
                return Date.Day;
            }
        }

        public int Month
        {
            get
            {
                return Date.Month;
            }
        }

        public int Year
        {
            get
            {
                return Date.Year;
            }
        }

    }
}
