using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.IndexTabbedPages;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.ServiceController;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTeamPage : ContentPage
    {
        bool isSuccessName = false;
        bool isSuccessShortName = false;
        TeamServices serviceTeam = new TeamServices();
        public CreateTeamPage()
        {
            InitializeComponent();
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.White;

        }

        private void EntryTeamName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( entryTeamName.Text != null || entryTeamName.Text != " "  )
            {
                if (entryTeamName.Text.Length < 4)
                {
                    situationControlTeamName.Text = "Takım adı en az 4 karakter uzunluğunda olmalıdır.";
                    situationControlTeamName.TextColor = Color.Red;
                    situationControlTeamName.FontAttributes = FontAttributes.Bold;
                    frmTeamName.BorderColor = Color.Red;
                    isSuccessName = false;
                }

                else if (entryTeamName.Text.Length > 16)
                {
                    situationControlTeamName.Text = "Takım adı en fazla 16 karakter uzunluğunda olmalıdır.";
                    situationControlTeamName.TextColor = Color.Red;
                    situationControlTeamName.FontAttributes = FontAttributes.Bold;
                    frmTeamName.BorderColor = Color.Red;
                    isSuccessName = false;
                }
                else if ( entryTeamName.Text[0] == ' ' )
                {
                    situationControlTeamName.Text = "İlk karakter boşluk olamaz";
                    situationControlTeamName.TextColor = Color.Red;
                    situationControlTeamName.FontAttributes = FontAttributes.Bold;
                    frmTeamName.BorderColor = Color.Red;
                    isSuccessName = false;
                }

                else
                {
                    situationControlTeamName.Text = "Takım adı uygun!";
                    situationControlTeamName.TextColor = Color.Green;
                    situationControlTeamName.FontAttributes = FontAttributes.None;
                    frmTeamName.BorderColor = Color.Green;
                    isSuccessName = true;
                }                    
            }
            else
            {
                isSuccessName = false;
            }
            
            
        }

        private void EntryTeamShortName_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (entryTeamShortName.Text != null || entryTeamShortName.Text != " ")
            {
                if (entryTeamShortName.Text.Length < 3)
                {
                    situationControlShortName.Text = "Takım kısaltması en az 3 karakter uzunluğunda olmalıdır.";
                    situationControlShortName.TextColor = Color.Red;
                    situationControlShortName.FontAttributes = FontAttributes.Bold;
                    frmTeamShortName.BorderColor = Color.Red;
                    isSuccessShortName = false;
                }

                else if (entryTeamShortName.Text.Length > 4)
                {
                    situationControlShortName.Text = "Takım kısaltması en fazla 4 karakter uzunluğunda olmalıdır.";
                    situationControlShortName.TextColor = Color.Red;
                    situationControlShortName.FontAttributes = FontAttributes.Bold;
                    frmTeamShortName.BorderColor = Color.Red;
                    isSuccessShortName = false;
                }
                else if (entryTeamShortName.Text[0] == ' ')
                {
                    situationControlShortName.Text = "İlk karakter boşluk olamaz";
                    situationControlShortName.TextColor = Color.Red;
                    situationControlShortName.FontAttributes = FontAttributes.Bold;
                    frmTeamShortName.BorderColor = Color.Red;
                    isSuccessShortName = false;
                }

                else
                {
                    situationControlShortName.Text = "Takım kısaltması uygun!";
                    situationControlShortName.TextColor = Color.Green;
                    situationControlShortName.FontAttributes = FontAttributes.None;
                    frmTeamShortName.BorderColor = Color.Green;
                    isSuccessShortName = true;
                }

            }
            else
            {
                isSuccessShortName = false;
            }
        }

        private async void BtnFinish_Clicked(object sender, EventArgs e)
        {
            if ( !App.Current.Properties.ContainsKey("loggedUser") )
            {
                await DisplayAlert("HATA", "Lütfen giriş yapınız", "Tamam");
                await Navigation.PushAsync(new LoginPageMain());
            }
            else if ( App.Current.Properties["loggedUserTeamPlayer"] != null )
            {
                await DisplayAlert("HATA", "Bir hata oluştu", "OK");
                App.Current.Properties.Remove("loggedUserTeamPlayer");
                App.Current.Properties.Remove("loggedUser");
                App.Current.MainPage = new NavigationPage(new IndexTabbedPages.IndexPageMain());

            }
            else
            {
                if (isSuccessName == false || isSuccessShortName == false)
                {
                    await DisplayAlert("UYARI", "Lütfen takım adını ve kısaltmasını kontrol edin", "Tamam");
                }
                else
                {
                    try
                    {
                        await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("Oluşturuluyor.."));
                        CreateTeamModel modelToReturn = await serviceTeam.CreateTeam(entryTeamName.Text, entryTeamShortName.Text, "abc");
                        await Navigation.PopPopupAsync();
                        if (modelToReturn.isSuccesful == true)
                        {
                            await DisplayAlert("Başarılı", modelToReturn.responseMessage, "OK");
                            App.Current.Properties["loggedUserTeamPlayer"] = modelToReturn.TeamPlayer;
                            App.Current.MainPage = new NavigationPage(new IndexPageMain());
                        }
                        else
                        {
                            await DisplayAlert("Hata", modelToReturn.responseMessage, "OK");
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    
                }
            }
            
        }
    }
}