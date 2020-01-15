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
    public partial class UserAgreementPolicyPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public UserAgreementPolicyPage()
        {
            InitializeComponent();
           
        }
    }
}