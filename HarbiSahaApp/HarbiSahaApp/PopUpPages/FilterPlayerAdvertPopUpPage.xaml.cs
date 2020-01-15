using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPlayerAdvertPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public FilterPlayerAdvertPopUpPage()
        {
            InitializeComponent();
            datePickerDate.Date = DateTime.Now;
            datePickerDate.Format = "dd/MM/yyyy dddd";
            datePickerDate.MinimumDate = DateTime.Today;
            
        }
    }
}