using HarbiSahaApp.CustomControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.Models.ViewModels
{
    public class IndexPageViewModelField
    {
        public string PhotoPath { get; set; }

        public string CityAndTown { get; set; }
        public string PlaceName { get; set; }
        public string Cost { get; set; }
        public string Rate { get; set; }

        public double imageHeight
        {
            get
            {
                if ( DeviceControl.isTablet()  )
                {
                    return 180;
                }
                else
                {
                    return 100;
                }
            }

        }


    }
}
