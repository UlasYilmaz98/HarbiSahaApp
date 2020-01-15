using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class PlayerAdvertDetailsViewModel
    {
        public PlayerAdvert PlayerAdvert { get; set; }

        public string MatchDate
        {
            
            get
            {
                DateTime matchDay = new DateTime(PlayerAdvert.Year, PlayerAdvert.Month, PlayerAdvert.Day);
                
                return matchDay.ToString("dddd, dd MMMM ");
            }

        }

        public string MatchTime
        {
            get
            {
                int StartOn = PlayerAdvert.MatchTime;

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

        public string MatchFinishTime
        {
            get
            {
                int StartOn = PlayerAdvert.MatchTime;
                if( StartOn > 2300)
                {
                    StartOn = StartOn + 100;
                    StartOn = StartOn % 100;
                }
                else
                {
                    StartOn += 100;
                }

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
        

        public string CityAndTown { get { return PlayerAdvert.City + "/" + PlayerAdvert.Town; } }

        public string FieldName
        {
            get
            {
                return PlayerAdvert.FieldName;
            }
        }

        public string NeededPosition
        {
            get
            {
                return  PlayerAdvert.NeededPosition; 
            }
        }

        public string OwnerName
        {
            get {
                string name = PlayerAdvert.User.Name;
                string firstLetter = name[0].ToString().ToUpper();
                string rest = PlayerAdvert.User.Name.Substring(1, name.Length - 1);
                return firstLetter + rest;
            }

        }

        public string PointAndCountPoints
        {
            get
            {
                int totalPoints = 0;
                double averagePoint;
                if (PlayerAdvert.User.RateForms.Count <= 0)
                    return " Puanlama yapılmamış ";
                else if (PlayerAdvert.User.RateForms == null)
                    return " Puanlama yapılmamış";
                else
                {
                    foreach (RateForm form in PlayerAdvert.User.RateForms)
                    {
                        totalPoints += Convert.ToInt32(form.Rate);
                    }
                    averagePoint = totalPoints / PlayerAdvert.User.RateForms.Count;
                    averagePoint = Math.Round(averagePoint, 1);
                    return averagePoint.ToString() + "/5, " + PlayerAdvert.User.RateForms.Count.ToString() + " Puanlama";
                }


                //if ( PlayerAdvert.User.RateForms != null || PlayerAdvert.User.RateForms  )
                //{
                    
                //}
                //else
                //{
                //    return " Puanlama yapılmamış ";
                //}
            }


        }

        public string UserAge
        {
            get
            {
                DateTime birthday = new DateTime(PlayerAdvert.User.BirthYear, PlayerAdvert.User.BirthMonth, PlayerAdvert.User.BirthDay);
                TimeSpan difference = DateTime.Now - birthday;

                double Age = difference.TotalDays / 365;
                int _age = Convert.ToInt32(Age);
                return _age.ToString() + " yaşında";
            }
        }

        public string AdvertInfo
        {
            get
            {
                if ( PlayerAdvert.RandomLine5 != null )
                {
                    return PlayerAdvert.RandomLine5;
                }
                else
                {
                    return "( İlan detayı yok )";
                }
            }
        }

        public string AdvertBaseAdress
        {
            get
            {
                if ( PlayerAdvert.BaseAdress != null)
                {
                    return PlayerAdvert.BaseAdress;
                }
                else
                {
                    return "( Sahanın açık adresi paylaşılmamış. )";
                }
            }
        }

        public string ProfilePicturePath
        {
            get
            {
                return PlayerAdvert.User.PhotoPath;
            }
        }

        public string Cost
        {
            get
            {
                return PlayerAdvert.Neighborhood + " ₺";
            }
        }



    }
}
