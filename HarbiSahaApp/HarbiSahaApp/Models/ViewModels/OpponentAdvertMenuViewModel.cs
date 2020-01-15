using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class OpponentAdvertMenuViewModel
    {
        Dictionary<int, string> monthsDict = new Dictionary<int, string>()
        {
            {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},
            {9,"Eylül" },{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
        };

        public OpponentAdvert Advert { get; set; }

        public string TeamName {
            get
            {
                return Advert.Team.Name;
            }

        }

        public string ProfilePicturePath {
            get
            {
                return Advert.RandomLine5;
            }

        }

        //public User MatchOwner { get; set; } 

        //public Field Field { get; set; }

        public string AdvertMinDate {
            get
            {
                int minDay = Advert.MinDay, minMonth = Advert.MinMonth;
                string _minMonth;
               
                _minMonth = monthsDict[minMonth];
                return minDay.ToString() + " " + _minMonth;

            }

        }

        public string AdvertDate
        {
            get
            {
                DateTime adv = new DateTime(Advert.Year, Advert.Month, Advert.Day);
                //return adv.DayOfWeek + ", " + Advert.Day.ToString() + " " + monthsDict[Advert.Month];
                return adv.ToString("dddd, dd MMMM yyyy");
            }
        }

        public string AdvertMaxDate
        {
            get
            {
                int maxDay = Advert.MaxDay, maxMonth = Advert.MaxMonth;
                string _maxMonth;
                
                _maxMonth = monthsDict[maxMonth];
                return maxDay.ToString() + " " + _maxMonth;
            }

        }

        public string Location
        {
            get
            {
                if ( Advert.District1 == "Tüm İlçeler")
                {
                    return Advert.City + " / " + " Tüm ilçeler";
                }
                else if (Advert.District2 == null || Advert.District2 == "NO")
                {
                    return Advert.City + " / " + Advert.District1;
                }
                else if (Advert.District3 == null || Advert.District3 == "NO")
                {
                    return Advert.City + " / " + Advert.District1 + ", " + Advert.District2 ;
                }
                else if (Advert.District4 == null || Advert.District4 == "NO")
                {
                    return Advert.City + " / " + Advert.District1 + " +2 ilçeler ";
                }
                else 
                {
                    return Advert.City + " / " + Advert.District1 + " +3 ilçeler ";
                }
            }
        }

  

        public int StartOn {
            get
            {
                return Advert.AvailableMinTime;
            }

        }

        public int FinishOn
        {
            get
            {
                return Advert.AvailableMaxTime;
            }
        }

       

        

        public string OwnerName {

            get {
                return Advert.ownerUserName;

            }
        }

    

        public string NameText
        {
            get
            {
                return OwnerName;
            }
        }

       
       
        public string MinTime
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

        public string MaxTime
        {
            get
            {
                int firstPart, secondPart;
                string firstPartStr, secondPartStr;
                if (FinishOn < 1000)
                {
                    if (FinishOn == 0)
                    {
                        firstPartStr = "00";
                        secondPartStr = "00";
                    }
                    else if (FinishOn < 100)
                    {
                        firstPartStr = "00";
                        secondPartStr = FinishOn.ToString();
                    }
                    else
                    {
                        firstPart = FinishOn / 100;
                        secondPart = FinishOn % 100;
                        firstPartStr = "0" + firstPart.ToString();

                        if (secondPart == 0)
                            secondPartStr = "00";
                        else
                            secondPartStr = secondPart.ToString();
                    }

                }
                else
                {
                    firstPart = FinishOn / 100;
                    secondPart = FinishOn % 100;
                    firstPartStr = firstPart.ToString();

                    if (secondPart == 0)
                        secondPartStr = "00";
                    else
                        secondPartStr = secondPart.ToString();
                }
                return firstPartStr + "." + secondPartStr;

            }



        }

        public string MinMaxTimeStr
        {
            get
            {
                return MinTime + " - " + MaxTime + " arası uygun";
            }
        }

        public string MinMaxDateStr
        {
            get
            {
                return AdvertMinDate + " - " + AdvertMaxDate;
            }
        }
    }
}
