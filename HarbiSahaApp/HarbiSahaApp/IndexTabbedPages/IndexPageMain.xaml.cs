using HarbiSahaApp.AnimationPages;
using HarbiSahaApp.CustomControls;
using HarbiSahaApp.Models;
using HarbiSahaApp.Models.OtherClasses;
using HarbiSahaApp.Models.ViewModels;
using HarbiSahaApp.PlayerAdvertCreatingPages;
using HarbiSahaApp.RegisterPages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace HarbiSahaApp.IndexTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPageMain : Xamarin.Forms.TabbedPage
    {

        List<IndexPageViewModelField> FieldViewModels = new List<IndexPageViewModelField>();
        LoginPageMain loginPage = new LoginPageMain();
        ProfilePage profilePage = new ProfilePage();
        public IndexPageMain()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            


            NavigationPage.SetHasNavigationBar(this, false);
            
            //headerImages1.Source = FileImageSource.FromResource("indexImage1.jpg");
            //headerImages2.Source = FileImageSource.FromResource("indexImage2.jpeg");
            //headerImages3.Source = FileImageSource.FromResource("indexImage3.jpg");
            //headerImages4.Source = FileImageSource.FromResource("indexImage4.jpg");            

            //logonUser = Application.



            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Harbi Saha Halı Saha",
                CityAndTown = "İstanbul/Bakırköy",
                Cost = "Kapora 40 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "DeğirmenTek Gücü Halı Saha",
                CityAndTown = "İstanbul/Şirinevler",
                Cost = "Kapora 35 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Sokrates Kulübü Halı Saha",
                CityAndTown = "İstanbul/Beylikdüzü",
                Cost = "Kapora 50 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Harbi Saha Spor Kompleksi",
                CityAndTown = "İstanbul/Küçükçekmece",
                Cost = "Kapora 60 ₺",
                Rate = "4.2 / 5"
            });

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                collectionView1.HeightRequest = 700;
                frmFindMatch.WidthRequest = 280;
                frmFindMatch.HeightRequest = 310;
                frmFindOpponent.WidthRequest = 280;
                frmFindOpponent.HeightRequest = 310;
                frmFindPlayer.WidthRequest = 280;
                frmFindPlayer.HeightRequest = 310;
                frmRentField.WidthRequest = 280;
                frmRentField.HeightRequest = 310;
                collectionView1.HeightRequest = 600;


            }
            


            Fill();
        }

        private async void Fill()
        {
            collectionView1.ItemsSource = FieldViewModels;
        }

        private void CollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                Navigation.PushAsync(new FieldDetailPage(new Field()));
            }
            else
            {
                Navigation.PushAsync(new LoginPageMain());
            }

            //Navigation.PushAsync(new FieldDetailPage(new Field()));

        }

        private async void BtnShowAllFields_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new UserProfilePage(new User()
            //{
            //    Name = "Ulaş",
            //    BirthDay = 23,
            //    BirthMonth = 3,
            //    BirthYear = 1995,
            //    City = "İstanbul",
            //    Position1 = "Orta Saha",
            //    OverallPoint = 250,
            //    OverallStarPoint = 3,
            //    ProfileMessage = "Paslı oynamayı ve dikine oynamayı severim.Her zaman dost canlısı biriyimdir.",
            //    PhoneNumber = "https://www.biography.com/.image/t_share/MTY2MzU3Nzk2OTM2MjMwNTkx/elon_musk_royal_society.jpg",
            //    CountPlayedMathes = 5,
            //    isConfirmed = true,
            //    RandomLine3 = "OK",

            //},false));
            //IList<Page> children = this.Children;
            //await Navigation.PushAsync(new RegisterPage3(new RegisterModel() { Email = "aa" }));
            //await Navigation.PushAsync(new ChatPage());
            //await Navigation.PushAsync(new ChatPageSample("HarbiSahaApp" ));
            //await Navigation.PushAsync(new TryingPage());
            await Navigation.PushAsync(new AllFields());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            //ContentPage page = new ContentPage();
            //ContentPage pageSelected = new ContentPage();                   
            if ( this.CurrentPage == this.Children[3])
            {
                if ( App.Current.Properties.ContainsKey("loggedUser"))
                {
                    MessagesPage page = this.Children[3] as MessagesPage;
                    page.FillChannels();
                }              
            }
            //
            headerImages3.Source = "SearchOpponent.jpg";
            headerImages4.Source = "rentfield.jpg";
            if (!App.Current.Properties.ContainsKey("loggedUser"))
            {
                lblWelcome.Text = "Hoşgeldin! Şimdi Harbi Saha'da ne yapmak istediğini seç.";
                

            }
            else
            {
                User logonUser = App.Current.Properties["loggedUser"] as User;
                lblWelcome.Text = "Hoşgeldin " + logonUser.Name + ".Şimdi ne yapmak istediğini seç ve maça başla!";
                
            }

        }

        private async void TapGoFilterPlayerAdverts_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilterPlayerAdvertPage());

        }

        private async void TapGoToCreateTeamAdverts_Tapped(object sender, EventArgs e)
        {
            if ( App.Current.Properties.ContainsKey("loggedUser"))
            {
                if (App.Current.Properties["loggedUser"] != null)
                {
                    await Navigation.PushAsync(new CreatePlayerAdvertPage1());
                }
                else
                {
                    await Navigation.PushAsync(new LoginPageMain());
                }
            }
            else
            {
                await Navigation.PushAsync(new LoginPageMain());
            }
            
        }

        private async void TapGoToOpponentAdverts_Tapped(object sender, EventArgs e)
        {
            if (App.Current.Properties.ContainsKey("loggedUser"))
            {
                if (App.Current.Properties["loggedUser"] != null)
                {
                    await Navigation.PushAsync(new OpponentAdvertsDetalFilterPage());
                }
                else
                {
                    await Navigation.PushAsync(new LoginPageMain());
                }
            }
            else
            {
                await Navigation.PushAsync(new LoginPageMain());
            }
        }

        private void TapGoToRentField_Tapped(object sender, EventArgs e)
        {

        }

        public double setFrameWidth()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return 140;
            }
            else
            {
                return 280;
            }

        }

        public double setFrameHeight()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return 175;
            }
            else
            {
                return 350;
            }

        }

        public double setCollectionViewImageHeight()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return collectionView1.WidthRequest = 460;
            }
            else
            {
                return collectionView1.WidthRequest = 700;
            }
        }


    }
}