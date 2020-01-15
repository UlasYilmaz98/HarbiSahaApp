using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HarbiSahaApp.Models.ViewModels
{
    public class MixedAdvertsViewModel
    {
        Dictionary<int, string> monthsDict = new Dictionary<int, string>()
        {
            {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},
            {9,"Eylül" },{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
        };

        Dictionary<string, string> DaysDict = new Dictionary<string, string>()
        {
            { "Sunday","Pzr." },{"Monday","Pzt."},{"Tuesday","Sal."},{"Wednesday","Çrş."},{"Thursday","Prş."},{"Friday","Çrş."},
            {"Saturday","Cts." }
        };
        
        public PlayerAdvert PlayerAdvert { get; set; }

        public OpponentAdvert OpponentAdvert { get; set; }

        public string Type { get; set; }

        public string DateAndTime {

            get
            {
                if ( Type == "Oyuncu İlanı")
                {
                    int Day = PlayerAdvert.Day, Month = PlayerAdvert.Month, Year = PlayerAdvert.Year;
                    string Date;
                    DateTime thatDay = new DateTime(Year, Month,Day);
                    if (DateTime.Now.Day == Day && DateTime.Now.Month == Month && DateTime.Now.Year == Year)
                    {
                        Date = "Bugün";
                    }
                    else
                    {
                        string dayStr = thatDay.DayOfWeek.ToString();
                        Date = DaysDict[dayStr] + " " + Day.ToString() + " " + monthsDict[Month];
                    }


                    int firstPart, secondPart;
                    string firstPartStr, secondPartStr;
                    if (PlayerAdvert.MatchTime < 1000)
                    {
                        if (PlayerAdvert.MatchTime == 0)
                        {
                            firstPartStr = "00";
                            secondPartStr = "00";
                        }
                        else if (PlayerAdvert.MatchTime < 100)
                        {
                            firstPartStr = "00";
                            secondPartStr = PlayerAdvert.MatchTime.ToString();
                        }
                        else
                        {
                            firstPart = PlayerAdvert.MatchTime / 100;
                            secondPart = PlayerAdvert.MatchTime % 100;
                            firstPartStr = "0" + firstPart.ToString();

                            if (secondPart == 0)
                                secondPartStr = "00";
                            else
                                secondPartStr = secondPart.ToString();
                        }

                    }
                    else
                    {
                        firstPart = PlayerAdvert.MatchTime / 100;
                        secondPart = PlayerAdvert.MatchTime % 100;
                        firstPartStr = firstPart.ToString();

                        if (secondPart == 0)
                            secondPartStr = "00";
                        else
                            secondPartStr = secondPart.ToString();
                    }
                    return Date + " " + firstPartStr + "." + secondPartStr;
                }
                else if (Type == "Rakip İlanı")
                {
                    int Day = OpponentAdvert.Day, Month = OpponentAdvert.Month, Year = OpponentAdvert.Year;
                    string Date;
                    DateTime thatDay = new DateTime(Year, Month, Day);
                    if (DateTime.Now.Day == Day && DateTime.Now.Month == Month && DateTime.Now.Year == Year)
                    {
                        Date = "Bugün";
                    }
                    else
                    {
                        string dayStr = thatDay.DayOfWeek.ToString();
                        Date = DaysDict[dayStr] + " " + Day.ToString() + " " + monthsDict[Month];
                    }
                    return Date;

                    
                }
                else
                {
                    return "Belirsiz";
                }
            }

        }

        public string Location
        {
            get
            {
                string LocaStr;
                if ( Type == "Oyuncu İlanı")
                {
                    return PlayerAdvert.City + "/" + PlayerAdvert.Town;

                }
                else if ( Type == "Rakip İlanı")
                {
                    return OpponentAdvert.City;

                }
                else
                {
                    return "Belirsiz";
                }
                //if ( LocaStr.Length > 27)
                //{
                //    return LocaStr.Substring(0, 27) + ".";
                //}
                //else
                //{

                //}
            }
        }

        public string OwnerName
        {
            get
            {
                if ( Type == "Oyuncu İlanı")
                {
                    return PlayerAdvert.User.Name + " tarafından oluşturuldu.";
                }
                else if ( Type == "Rakip İlanı")
                {
                    return OpponentAdvert.ownerUserName + " tarafından oluşturuldu.";
                }
                else
                {
                    return "Belirsiz";
                }
            }
        }

        public string Situation {

            get
            {
                if (Type == "Oyuncu İlanı")
                {
                    if (PlayerAdvert.isOpen == true)
                        return "Aktif İlan";
                    else
                        return "Oyuncu Bulundu";
                }
                else if (Type == "Rakip İlanı")
                {
                    if (OpponentAdvert.isOpen == true)
                        return "Aktif İlan";
                    else
                        return "Oyuncu Bulundu";
                }
                else
                {
                    return "Belirsiz";
                }

            }

        }

        public string ProfilePicture {


            get
            {
                if (Type == "Oyuncu İlanı")
                {
                    return PlayerAdvert.User.PhotoPath;
                }
                else if (Type == "Rakip İlanı")
                {
                    return OpponentAdvert.RandomLine5;
                }
                else
                {
                    return "Belirsiz";
                }
            }

        }

        public string situationICon
        {
            get
            {
                if (Type == "Oyuncu İlanı")
                {
                    if (PlayerAdvert.isOpen == true)
                        return "fullStar.png";
                    else
                        return "emptyStar.png";
                }
                else if (Type == "Rakip İlanı")
                {
                    if (OpponentAdvert.isOpen == true)
                        return "fullStar.png";
                    else
                        return "emptyStar.png";
                }
                else
                {
                    return "Belirsiz";
                }
            }
        }

        public Color TextColorSituation { get
            {

                if (Type == "Oyuncu İlanı")
                {

                    if (PlayerAdvert.isOpen == true)
                        return Color.DarkGreen;
                    else
                        return Color.Crimson;
                }
                else if (Type == "Rakip İlanı")
                {
                    if (OpponentAdvert.isOpen == true)
                        return Color.DarkGreen;
                    else
                        return Color.Crimson;
                }
                else
                {
                    return Color.Crimson;
                }

            } }

        public Color BorderColor
        {
            get
            {
                if (Type == "Oyuncu İlanı")
                {

                    if (PlayerAdvert.isOpen == true)
                        return Color.FromHex("#949494");
                    else
                        return Color.FromHex("#e6e6e6");
                }
                else if (Type == "Rakip İlanı")
                {
                    if (OpponentAdvert.isOpen == true)
                        return Color.FromHex("#949494");
                    else
                        return Color.FromHex("#e6e6e6");
                }
                else
                {
                    return Color.FromHex("#e6e6e6");
                }
            }
        }

        public double Opacity
        {
            get
            {
                if (Type == "Oyuncu İlanı")
                {

                    if (PlayerAdvert.isOpen == true)
                        return 1.0;
                    else
                        return 0.3;
                }
                else if (Type == "Rakip İlanı")
                {
                    if (OpponentAdvert.isOpen == true)
                        return 1.0;
                    else
                        return 0.3;
                }
                else
                {
                    return 0.3;
                }
            }
        }


        //BINDING
        


    }
}
