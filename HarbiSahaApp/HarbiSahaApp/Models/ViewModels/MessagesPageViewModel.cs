using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class MessagesPageViewModel
    {
        Dictionary<int, string> monthsDict = new Dictionary<int, string>()
        {
            {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},
            {9,"Eylül" },{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
        };


        public int ChannelId { get; set; }

        

        public User currentUser { get; set; }

        public User channelUser1 { get; set; } // CHANNEL BOŞ İSE DOLDURULMAYACAK.

        public User channelUser2 { get; set; } // CHANNEL BOŞ İSE DOLDURULMAYACAK.

        

        public ChatChannel currentChannel { get; set; }

        public PlayerAdvert PlayerAdvert { get; set; }

        public OpponentAdvert OpponentAdvert { get; set; }

        //public User ownerUser { get; set; }

        public User OtherUser
        {
            get
            {
                if (PlayerAdvert != null)
                {
                    if (PlayerAdvert.UserId == currentUser.Id)
                    {
                        return channelUser2;
                    }
                    else
                    {
                        return channelUser1;
                    }

                }
                else
                {

                    if (Convert.ToInt32(OpponentAdvert.ownerId) == currentUser.Id)
                    {
                        return channelUser2;
                    }
                    else
                    {
                        return channelUser1;
                    }
                }
            }
        }

        public string otherUserName
        {

            get
            {
                if ( PlayerAdvert!= null)
                {
                    if ( PlayerAdvert.UserId == currentUser.Id)
                    {
                        return channelUser2.Name;
                    }
                    else
                    {
                        return channelUser1.Name;
                    }
                        
                }
                else
                {

                    if (Convert.ToInt32(OpponentAdvert.ownerId) == currentUser.Id)
                    {
                        return channelUser2.Name;
                    }
                    else
                    {
                        return channelUser1.Name;
                    }
                }
            }
        }

        public string otherUserEmail
        {
            get
            {
                if (PlayerAdvert != null)
                {
                    if (PlayerAdvert.UserId == currentUser.Id)
                    {
                        return channelUser2.Email;
                    }
                    else
                    {
                        return channelUser1.Email;
                    }

                }
                else
                {

                    if (Convert.ToInt32(OpponentAdvert.ownerId) == currentUser.Id)
                    {
                        return channelUser2.Email;
                    }
                    else
                    {
                        return channelUser1.Email;
                    }
                }
            }
        }

        
        public string FieldName {
            get
            {
                if (PlayerAdvert != null)
                    return PlayerAdvert.FieldName;
                else
                    return null;
            }

        }

        public string DateOfAdv {
            get
            {
                if ( PlayerAdvert != null )
                {
                    if (DateTime.Now.Day == PlayerAdvert.Day && DateTime.Now.Month == PlayerAdvert.Month && DateTime.Now.Year == PlayerAdvert.Year)
                    {
                        return "Bugün";
                    }
                    else
                    {
                        return PlayerAdvert.Day.ToString() + " " + monthsDict[PlayerAdvert.Month];
                    }
                }
                else
                {
                    return null;
                }
            }

        }

        public string TimeOfAdv
        {
            get
            {
                if (PlayerAdvert != null)
                {
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
                    return firstPartStr + "." + secondPartStr;
                }
                else if( OpponentAdvert != null )
                {
                    return OpponentAdvert.Team.Name;
                }
                else
                {
                    return null;
                }
            }

        }



        public string DateAndTime
        {
            get
            {
                if ( PlayerAdvert != null)
                {
                    return DateOfAdv + " " + TimeOfAdv;
                }
                else
                {
                    return null;
                }
            }
        }






    }
}
