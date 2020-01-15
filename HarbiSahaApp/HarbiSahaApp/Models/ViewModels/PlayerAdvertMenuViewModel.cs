using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class PlayerAdvertMenuViewModel
    {

        Dictionary<int, string> monthsDict = new Dictionary<int, string>()
        {
            {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},
            {9,"Eylül" },{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
        };

        public PlayerAdvert Advert { get; set; }

        public string ProfilePicturePath { get; set; }

        //public User MatchOwner { get; set; } 

        public Field Field { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int StartOn { get; set; }

        public string FieldName { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Cost { get; set; }

        public string GoalKeeperPath
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Kaleci")
                    return "gkGlovesRedEdited.png";
                else
                    return "gkGlovesGrayEdited.png";
            }
        }

        public double GoalKeeperOpacity
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Kaleci")
                    return 1.0;
                else
                    return 0.2;
            }
        }

        public string DefencePath
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Defans")
                    return "defenceIconEdited.png";
                else
                    return "defenceIconGrayEdited.png";
            }
        }

        public double DefenceOpacity
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Defans")
                    return 1.0;
                else
                    return 0.2;
            }
        }

        public string MidfieldPath
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Orta Saha")
                    return "midfielderBlueEdited.png";
                else
                    return "midfielderIconGrayEdited.png";
            }
        }

        public double MidfieldOpacity
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Orta Saha")
                    return 1.0;
                else
                    return 0.2;
            }
        }

        public string ForwardPath
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Hücumcu")
                    return "forwardIconGreenEdited.png";
                else
                    return "forwardIconGrayEdited.png";
            }
        }

        public double ForwardOpacity
        {
            get
            {
                if (Position == "Tüm Mevkiler" || Position == "Hücumcu")
                    return 1.0;
                else
                    return 0.2;
            }
        }

        public string Date
        {
            get
            {
                if ( DateTime.Now.Day == Day && DateTime.Now.Month == Month && DateTime.Now.Year == Year)
                {
                    return "Bugün";
                }
                else
                {
                    return Day.ToString() + " " + monthsDict[Month];
                }
            }
        }

        public string Time
        {
            get
            {
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


    }
}
