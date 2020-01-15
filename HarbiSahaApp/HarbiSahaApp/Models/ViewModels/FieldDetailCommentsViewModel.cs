using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class FieldDetailCommentsViewModel
    {
        public RateFormField Form { get; set; }

        public string Comment
        {
            get
            {
                return Form.Comment;
            }
        }

        public string RateText
        {
            get
            {
                return "Değerlendirme : " + Form.Rate + " puan verdi";
            }
        }
   

        public string DaysAgo {
            get
            {
                DateTime today = DateTime.Now;
                DateTime commentDay = Form.CreatedOn;

                double totalDayDifference = Math.Round((today - commentDay).TotalDays);
                if ( totalDayDifference < 2)
                {
                    return "Bugün";
                }
                else if ( totalDayDifference < 3)
                {
                    return "Dün";

                }
                else if ( totalDayDifference < 30 )
                {
                    return totalDayDifference.ToString() + " gün önce";
                }
                else if ( totalDayDifference < 365 )
                {
                    int month;
                    month = Convert.ToInt32(totalDayDifference) / 30;
                    return month.ToString() + " ay önce";
                }
                else
                {
                    int year;
                    year = Convert.ToInt32(totalDayDifference) / 365;
                    return year.ToString() + " yıl önce";
                }

            }

        }


        public string NameWithStars
        {
            get
            {
                string name;
                string surname;

                name = Form.RandomLine1[0].ToString();
                for (int i = 0; i < Form.RandomLine1.Length-1; i++)
                {
                    name += "*";
                }
                surname = Form.RandomLine2[0].ToString();
                for (int i = 0; i < Form.RandomLine2.Length - 1; i++)
                {
                    surname += "*";
                }

                return name + " " + surname;
            }

        }

        public string imgRate1
        {
            get
            {
                return "fullStar.png";
            }
        }

        public string imgRate2
        {
            get
            {
                if (Convert.ToInt32(Form.Rate) > 1)
                {
                    return "fullStar.png";
                }
                else
                {
                    return "emptyStar.png";
                }
            }
        }

        public string imgRate3
        {
            get
            {
                if (Convert.ToInt32(Form.Rate) > 2)
                {
                    return "fullStar.png";
                }
                else
                {
                    return "emptyStar.png";
                }
            }
        }

        public string imgRate4
        {
            get
            {
                if (Convert.ToInt32(Form.Rate) > 3)
                {
                    return "fullStar.png";
                }
                else
                {
                    return "emptyStar.png";
                }
            }
        }

        public string imgRate5
        {
            get
            {
                if (Convert.ToInt32(Form.Rate) > 4)
                {
                    return "fullStar.png";
                }
                else
                {
                    return "emptyStar.png";
                }
            }
        }
    }
}
