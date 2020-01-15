using HarbiSahaApp.Models;
using HarbiSahaApp.ServiceController;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePhoto : ContentPage
    {
        User currentUser = new User();
        Stream imgStream;
        ManageServices service = new ManageServices();
        Plugin.Media.Abstractions.MediaFile mainFile;
        
        public ChangePhoto()
        {
            InitializeComponent();
            
            NavigationPage navpage = App.Current.MainPage as NavigationPage;
            navpage.BarBackgroundColor = Color.FromHex("#4e98bf");
            navpage.BarTextColor = Color.White;
            currentUser = App.Current.Properties["loggedUser"] as User;

            imgProfilePicture.Source = currentUser.PhotoPath;

        }

        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MediaPlugin",
                    Name = DateTime.Now + ".jpg",
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    SaveToAlbum = false,
                    CompressionQuality = 40,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small


                };
                //mainFile = mediaOptions;


                // Take a photo of the business receipt.
                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                mainFile = file;
                if (file == null)
                    return;
                imgProfilePicture.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    //imgStream = stream;
                    //file.Dispose();
                    
                    return stream;

                });
                btnConfirm.IsEnabled = true;
                btnConfirm.BackgroundColor = Color.FromHex("#71d18b");
            }
        }

        private async void BtnPickPhoto_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
                {

                    CompressionQuality = 40,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    
                    



                });
                mainFile = file;

                if (file == null)
                    return;
                imgProfilePicture.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    //imgStream = stream;
                    //file.Dispose();
                    return stream;
                });
                btnConfirm.IsEnabled = true;
                btnConfirm.BackgroundColor = Color.FromHex("#71d18b");
            }
            else
            {
                await DisplayAlert("UYARI", "Uygun kamera mevcut değil!", "OK");
                return;
            }

        }

        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushPopupAsync(new HarbiSahaApp.AnimationPages.AnimationPopUpPage1_Waiting("Yükleniyor.."));
            
            imgStream = mainFile.GetStream();
            
            string userPhotoName = currentUser.Id.ToString() + "_pp";
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(mainFile.GetStream()), "FileName",mainFile.Path);
            //content.Add(new ImageSo)
            string message = await service.ChangeProfilePhoto(content,currentUser.Id);
            User toBeReturned = await service.GetSingleUser(currentUser.Id);
            currentUser.PhotoPath = toBeReturned.PhotoPath;
            App.Current.Properties["loggedUser"] = currentUser;
            await Navigation.PopPopupAsync();
            
            if( message == "Başarılı" )
            {
                await DisplayAlert("Başarılı", "Profil fotoğrafınız gücellendi!", "Tamam");
                mainFile.Dispose();
            }
        }
    }
}