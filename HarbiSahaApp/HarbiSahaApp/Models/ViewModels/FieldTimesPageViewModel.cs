using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HarbiSahaApp.Models.ViewModels
{
    public class FieldTimesPageViewModel
    {
        public Match match { get; set; }

        public int TimeInt { get; set; }

        public bool isEnabled {

            get;set;
        }

        public double Opacity
        {
            get
            {
                if (isEnabled)
                    return 1;
                else
                    return 0.3;
            }
        }

        public bool isSelected { get; set; }

        public Color BgColor {
            get
            {
                if (isSelected)
                {
                    return Color.FromHex("#004708");
                }
                else if ( isEnabled)
                {
                    return Color.White;
                }
                else
                {
                    return Color.FromHex("#e6e6e6");
                }

                //if ( isSelected )
                //{
                //    return Color.FromHex("#2dbefc");
                //}
                //else if ( match != null && match.MatchOwnerId == 1)
                //{
                //    return Color.FromHex("#97fca8");
                //}
                //else
                //{
                //    return Color.Transparent;
                //}

            }
        }

      
        public string TimeStr
        {

            get
            {
                int StartOn = TimeInt;

                int firstPart, secondPart;
                string firstPartStr, secondPartStr;
                if (StartOn < 1000)
                {
                    if (StartOn == 0)
                    {
                        firstPartStr = "00";
                        secondPartStr = "00";
                    }
                    else if (StartOn < 100)
                    {
                        firstPartStr = "00";
                        secondPartStr = StartOn.ToString();
                    }
                    else
                    {
                        firstPart = StartOn / 100;
                        secondPart = StartOn % 100;
                        firstPartStr = "0" + firstPart.ToString();

                        if (secondPart == 0)
                            secondPartStr = "00";
                        else
                            secondPartStr = secondPart.ToString();
                    }

                }
                else
                {
                    firstPart = StartOn / 100;
                    secondPart = StartOn % 100;
                    firstPartStr = firstPart.ToString();

                    if (secondPart == 0)
                        secondPartStr = "00";
                    else
                        secondPartStr = secondPart.ToString();
                }
                return firstPartStr + "." + secondPartStr;
            }

        }

        public DateTime date { get; set; }

        public string dayStr
        {
            get
            {
                //DateTime matchDay = new DateTime(PlayerAdvert.Year, PlayerAdvert.Month, PlayerAdvert.Day);

                return date.ToString("dddd, dd MMMM ");
            }
        }

        public Color BorderColor {

            get
            {
                if (isSelected)
                {
                    return Color.FromHex("#004708");
                }
                else if (isEnabled)
                {
                    return Color.FromHex("#cfcfcf");
                }
                else
                {
                    return Color.FromHex("#cfcfcf");
                }

            }

        }

        public Color TextColor
        {

            get
            {
                if (isSelected)
                {
                    return Color.White;
                }
                else if (isEnabled)
                {
                    return Color.FromHex("#cfcfcf");
                }
                else
                {
                    return Color.FromHex("#8f8f8f");
                }

            }

        }





    }
}
