using HarbiSahaApp.CustomControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class FielDetailPageViewModel
    {
        public Field Field { get; set; }

        public string CityAndTown {

            get
            {
                return Field.City + "/" + Field.District;
            }
        }

        public string JustTown
        {
            get
            {
                return Field.District;
            }
        }

        public double OverallPoint
        {
            get
            {
                double rounded;
                double realPoint;
                double totalPoints = 0;
                foreach ( RateFormField rateForm in Field.RateFormFields)
                {
                    totalPoints += Convert.ToInt32(rateForm.Rate);
                }
                realPoint = totalPoints / Field.RateFormFields.Count;
                rounded = Math.Round(realPoint, 1);
                return rounded;

            }
        }

        public string PointsOverFive
        {
            get
            {
                return OverallPoint.ToString() + " / " + "5 ";
            }
        }

        public string PhotoPath
        {
            get
            {
                return Field.PhotoPath1;
            }
        }

        public string FieldName
        {
            get
            {
                return Field.FieldName;
            }
        }

        public string PriceHalf
        {
            get
            {
                return Field.FieldZones[0].PaymentModel1SmallCost.ToString() + "₺";
            }
        }

        public string FullPrice
        {
            get
            {
                return Field.FieldZones[0].PaymentModel1FullCost.ToString() + "₺";
            }
        }

        public string star2
        {
            get
            {
                if (OverallPoint < 1.25)
                    return "emptyStar.png";
                else if (OverallPoint < 1.75)
                    return "halfStar.png";
                else
                    return "fullStar.png";
            }
        }

        public string star3
        {
            get
            {
                if (OverallPoint < 2.25)
                    return "emptyStar.png";
                else if (OverallPoint < 2.75)
                    return "halfStar.png";
                else
                    return "fullStar.png";
            }
        }

        public string star4
        {
            get
            {
                if (OverallPoint < 3.25)
                    return "emptyStar.png";
                else if (OverallPoint < 3.75)
                    return "halfStar.png";
                else
                    return "fullStar.png";
            }
        }

        public string star5
        {
            get
            {
                if (OverallPoint < 4.25)
                    return "emptyStar.png";
                else if (OverallPoint < 4.75)
                    return "halfStar.png";
                else
                    return "fullStar.png";
            }
        }

        public double ImageHeight {
            get
            {
                if(DeviceControl.isTablet())
                {
                    return 200;
                }
                else
                {
                    return 120;
                }

            }
        }









    }
}
