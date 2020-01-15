using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HarbiSahaApp.Models.ViewModels
{
    public class ChatPageViewModel
    {

        Dictionary<string, string> daysDict  = new Dictionary<string, string>()
        {
             { "Sunday","Pzr."},{ "Monday","  Pzt."},{"Tuesday","Salı"},{"Wednesday","Çrş."},{"Thursday","Prş."},{"Friday","Cuma"},{"Saturday","Cts."}
        };
        Dictionary<int, string> monthsDict = new Dictionary<int, string>()
        {
            {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},
            {9,"Eylül" },{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
        };

        public User SenderUser { get; set; }

        public string Message { get; set; }

        public DateTime SendingTime { get; set; }

        //public ChatMessage ChatMessage { get; set; }
        public string Name
        {
            get
            {
                return SenderUser.Name ;
            }
        }



        public string Time
        {
            get
            {
                DateTime now = SendingTime;
                return daysDict[now.DayOfWeek.ToString()] + " " + now.Day.ToString() + " " + monthsDict[now.Month] + " " + now.Hour + ":" + now.Minute;

            }
        }

        public bool isMyMessage { get; set; }

        public string Situation
        {
            get
            {
                if (isMyMessage)
                    return "Teslim Edildi";
                else
                    return "Bildir";
            }

        }

        public Color SituationTextColor
        {
            get
            {
                if (isMyMessage)
                    return Color.FromHex("#bfbfbf");
                else
                    return Color.FromHex("#4e98bf");
            }
        }

        public bool isUserChangingPoint { get; set; }

        public string PhotoPath
        {
            get
            {
                if (isUserChangingPoint)
                    return SenderUser.PhotoPath;
                else
                    return "";
            }
        }

        public bool NameTextVisibility
        {
            get
            {
                if (isUserChangingPoint)
                    return true;
                else
                    return false ;
            }
        }

        public LayoutOptions NameTextAlignment
        {
            get
            {
                if (isMyMessage)
                    return LayoutOptions.StartAndExpand;
                else
                    return LayoutOptions.EndAndExpand;
            }
        }

        public Color frameBackgroundColor
        {
            get
            {
                if (isMyMessage)
                    return Color.FromHex("#72cc7d");
                else
                    return Color.FromHex("#ededed");
            }
        }

        public Color TextColor
        {
            get
            {
                if (isMyMessage)
                    return Color.White;
                else
                    return Color.FromHex("#2b4470");
            }
        }

        public int ProfilePictureColumn
        {
            get
            {
                if (isMyMessage)
                    return 0;
                else
                    return 2;
            }
        }


        


    }
}
