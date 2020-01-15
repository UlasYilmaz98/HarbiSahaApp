using HarbiSahaApp.Models.ViewModels;
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
    public partial class PlayerAdvertCreatedPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        CreatePlayerAdvertViewModel mainModel = new CreatePlayerAdvertViewModel();

        public PlayerAdvertCreatedPopUpPage(CreatePlayerAdvertViewModel model)
        {
            InitializeComponent();
        }
    }
}