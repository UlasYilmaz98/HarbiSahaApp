﻿using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.RegisterPages;
using HarbiSahaApp.ServiceController;
using HarbiSahaApp.ValidationControls;
using Plugin.SecureStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.IndexTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        RegisterModel model = new RegisterModel();
        RegisterValidation validation = new RegisterValidation();
        ManageServices service = new ManageServices();
        bool isValidMail = false;
        bool isEmailChecked = false;


        public ProfilePage()
        {
            //Navigate();
            InitializeComponent();
            //Navigate();
        }

        

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if ( Application.Current.Properties.ContainsKey("loggedUser"))
            {
                if ( App.Current.Properties["loggedUser"] != null)
                {
                    User currentUser = new User();
                    currentUser = App.Current.Properties["loggedUser"] as User;
                    //imgProfilePicture.Source = currentUser.PhotoPath;
                    //imgProfilePicture.Source = UriImageSource.FromUri(new Uri(currentUser.PhotoPath));
                    imgProfilePicture.Source = new UriImageSource()
                    {
                        Uri = new Uri(currentUser.PhotoPath),
                        CachingEnabled = false
                    };
                    
                    lblName.Text = currentUser.Name.ToUpper();
                    //lblAveragePoints.Text = currentUser.
                    lblHarbiSahaPoints.Text = currentUser.OverallPoint.ToString();
                    lblCompletedMatches.Text = currentUser.CountPlayedMathes.ToString();
                }
                else
                {
                    layoutProfilePage.IsVisible = false;
                    layoutNotUser.IsVisible = true;
                    //imgLogo.Source = "logo_transparent.png";
                }
                
                
            }
            else
            {
                layoutProfilePage.IsVisible = false;
                layoutNotUser.IsVisible = true;
                //imgLogo.Source = "logo_transparent.png";
            }


        }

        

        private async void MyTeamTapped_Tapped(object sender, EventArgs e)
        {
            if ( App.Current.Properties["loggedUserTeamPlayer"] == null)
            {
                await Navigation.PushAsync(new CreateTeamPage());
            }
            else
            {
                await Navigation.PushAsync(new MyTeamPage());
            }
        }

        private async void TapLookAdverts_Tapped(object sender, EventArgs e)
        {
            if ( App.Current.Properties["loggedUser"] != null)
            {
                await Navigation.PushAsync(new MyAdvertsPage());
            }
        }

        //private async void BtnGetStarted_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LoginPageMain());
        //}

        private async void TapPersonalInfos_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserInfosPage());
        }


        //LOGIN OR REGISTER ACTIONS

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!validation.EmailValidation(entryEmail.Text))
            {
                //frameEntryEmail.BorderColor = Color.Red;
                boxViewMail.Color = Color.Red;
                lblWarningMail.TextColor = Color.Red;
                lblWarningMail.IsVisible = true;
                lblWarningMail.Text = "Lütfen geçerli bir E-posta adresi girin";
                isValidMail = false;
            }
            else
            {
                //frameEntryEmail.BorderColor = Color.Gray;
                boxViewMail.Color = Color.Gray;
                lblWarningMail.TextColor = Color.Gray;
                lblWarningMail.IsVisible = true;
                isValidMail = true;
                lblWarningMail.Text = "Kontrol Ediliyor...";
            }
        }

        private void EntryPassword_Completed(object sender, EventArgs e)
        {
            if (validation.PasswordValidation(entryPassword.Text) == "Success")
                entryRePassword.Focus();
        }

        private async void EntryEmail_Completed(object sender, EventArgs e)
        {
            bool _isValid;
            if (isValidMail)
            {
                _isValid = await service.CheckEmail(entryEmail.Text);
                if (!_isValid)
                {
                    //frameEntryEmail.BorderColor = Color.Green;
                    boxViewMail.Color = Color.Green;
                    lblWarningMail.TextColor = Color.Green;
                    lblWarningMail.IsVisible = true;
                    //isValidMail = true;
                    lblWarningMail.Text = "E-posta adresi geçerli";
                    isEmailChecked = true;
                    entryPassword.Focus();
                }
                else
                {
                    //frameEntryEmail.BorderColor = Color.Red;
                    boxViewMail.Color = Color.Red;
                    lblWarningMail.TextColor = Color.Red;
                    lblWarningMail.IsVisible = true;
                    lblWarningMail.Text = "Bu E-posta adresi kullanılıyor.";
                    isValidMail = false;
                    //isValidMail = false;
                }
            }

        }

        private void EntryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string message = validation.PasswordValidation(entryPassword.Text);
            if (message != "Success")
            {
                //framePassword.BorderColor = Color.Red;
                boxViewPassword.Color = Color.Red;
                lblWarningPass.TextColor = Color.Red;
                lblWarningPass.IsVisible = true;
                lblWarningPass.Text = message;
            }
            else
            {
                //framePassword.BorderColor = Color.Green;
                boxViewPassword.Color = Color.Green;
                lblWarningPass.TextColor = Color.Green;
                lblWarningPass.IsVisible = true;
                lblWarningPass.Text = "Şifre geçerli";
            }
        }

        private void EntryRePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validation.RePasswordValidation(entryPassword.Text, entryRePassword.Text))
            {
                //frameRePassword.BorderColor = Color.Green;
                lblWarningRePass.TextColor = Color.Green;
                boxViewRePassword.Color = Color.Green;
                lblWarningRePass.IsVisible = true;
                lblWarningRePass.Text = "Şifreler aynı";
            }
            else
            {
                //frameRePassword.BorderColor = Color.Red;
                boxViewRePassword.Color = Color.Red;
                lblWarningRePass.TextColor = Color.Red;
                lblWarningRePass.IsVisible = true;
                lblWarningRePass.Text = "Şifreler aynı değil";
            }
        }

        private async void EntryEmail_Unfocused(object sender, FocusEventArgs e)
        {
            bool _isValid;
            if (isValidMail)
            {
                _isValid = await service.CheckEmail(entryEmail.Text);
                if (!_isValid)
                {
                    //frameEntryEmail.BorderColor = Color.Green;
                    boxViewMail.Color = Color.Green;
                    lblWarningMail.TextColor = Color.Green;
                    lblWarningMail.IsVisible = true;
                    //isValidMail = true;
                    lblWarningMail.Text = "E-posta adresi geçerli";
                    isEmailChecked = true;
                    entryPassword.Focus();
                }
                else
                {
                    //frameEntryEmail.BorderColor = Color.Red;
                    boxViewMail.Color = Color.Red;
                    lblWarningMail.TextColor = Color.Red;
                    lblWarningMail.IsVisible = true;
                    lblWarningMail.Text = "Bu E-posta adresi kullanılıyor.";
                    isValidMail = false;
                    //isValidMail = false;
                }
            }
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            btnNext.IsEnabled = false;
            if (isEmailChecked == false)
            {
                await DisplayAlert("UYARI", "Bu posta adresi kullanılıyor", "OK");
                btnNext.IsEnabled = true;
            }
            else
            {
                await Navigation.PushPopupAsync(new AnimationPopUpPage1_Waiting("İşleniyor..."));
                string message = await validation.ReturnSituationStep1(entryEmail.Text, entryPassword.Text, entryRePassword.Text);
                if (message != "Success")
                {
                    await DisplayAlert("UYARI", message, "OK");
                    await Navigation.PopPopupAsync();
                    btnNext.IsEnabled = true;

                }
                else
                {
                    model.Email = entryEmail.Text;
                    model.Password = entryPassword.Text;
                    await Navigation.PushAsync(new RegisterPage2(model));
                    await Navigation.PopPopupAsync();
                    btnNext.IsEnabled = true;
                }
            }
        }


        private async void BtnHasAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPageMain());
        }

        private async void TapExit_Tapped(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("loggedUser");
            CrossSecureStorage.Current.DeleteKey("loggedUserId");
            CrossSecureStorage.Current.DeleteKey("loggedUserEmail");
            CrossSecureStorage.Current.DeleteKey("loggedUserPassword");
            App.Current.MainPage = new NavigationPage(new IndexPageMain());
        }

        private async void TapProfilePicture_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePhoto());
        }

        //private async void GridLookAdverts_Tapped(object sender, EventArgs e)
        //{
        //    Color color = this.BackgroundColor;
        //    this.BackgroundColor = Color.FromHex("#d4d4d4");
        //    if (App.Current.Properties["loggedUser"] != null)
        //    {
        //        this.BackgroundColor = color;
        //        await Navigation.PushAsync(new MyAdvertsPage());
        //    }
        //    this.BackgroundColor = color;
        //}
    }
}