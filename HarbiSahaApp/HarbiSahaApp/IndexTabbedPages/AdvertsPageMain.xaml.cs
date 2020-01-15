using HarbiSahaApp.Models;
using HarbiSahaApp.Models.ViewModels;
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
    public partial class AdvertsPageMain : ContentPage
    {
        List<Match> matches = new List<Match>();
        List<PlayerAdvertMenuViewModel> viewModels = new List<PlayerAdvertMenuViewModel>();

        
        public AdvertsPageMain()
        {
            InitializeComponent();
            FillFirst();
            Fill();
        }

        public void FillFirst()
        {
            matches.Add(new Match()
            {
                Id = 1,
                City = "İstanbul",
                District = "Bakırköy",
                Day = 30,
                Month = 5,
                StartOn = 2000,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            matches.Add(new Match()
            {
                Id = 2,
                City = "İstanbul",
                District = "Küçükçekemece",
                Day = 6,
                Month = 9,
                StartOn = 2130,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            matches.Add(new Match()
            {
                Id = 3,
                City = "İstanbul",
                District = "Bahçelievler",
                Day = 22,
                Month = 12,
                StartOn = 1700,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            matches.Add(new Match()
            {
                Id = 4,
                City = "İstanbul",
                District = "Beykoz",
                Day = 16,
                Month = 8,
                StartOn = 200,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            matches.Add(new Match()
            {
                Id = 5,
                City = "İstanbul",
                District = "Sultanbeyli",
                Day = 16,
                Month = 10,
                StartOn = 0,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            matches.Add(new Match()
            {
                Id = 6,
                City = "İstanbul",
                District = "Gaziosmanpaşa",
                Day = 21,
                Month = 11,
                StartOn = 2300,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"


            });
            matches.Add(new Match()
            {
                Id = 7,
                City = "İstanbul",
                District = "Avcılar",
                Day = 8,
                Month = 9,
                StartOn = 0,
                isPlayed = false,
                Year = 2019,
                isOpponentFound = false,
                FieldName = "Öz Kardeşler Spor Kompleksi"

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                
                City = matches[0].City,
                District = matches[0].District,
                Day = matches[0].Day,
                Month = matches[0].Month,
                Year = matches[0].Year,
                Name = "Ulaş",
                Position = "Orta Saha",
                StartOn = matches[0].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[1].City,
                District = matches[1].District,
                Day = matches[1].Day,
                Month = matches[1].Month,
                Year = matches[1].Year,
                Name = "Ebubekir",
                Position = "Defans",
                StartOn = matches[1].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[2].City,
                District = matches[2].District,
                Day = matches[2].Day,
                Month = matches[2].Month,
                Year = matches[2].Year,
                Name = "Rahmi",
                Position = "Kaleci",
                StartOn = matches[2].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[3].City,
                District = matches[3].District,
                Day = matches[3].Day,
                Month = matches[3].Month,
                Year = matches[3].Year,
                Name = "Rahmi",
                Position = "Kaleci",
                StartOn = matches[3].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[4].City,
                District = matches[4].District,
                Day = matches[4].Day,
                Month = matches[4].Month,
                Year = matches[4].Year,
                Name = "Kerem",
                Position = "Kaleci",
                StartOn = matches[4].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[5].City,
                District = matches[5].District,
                Day = matches[5].Day,
                Month = matches[5].Month,
                Year = matches[5].Year,
                Name = "Murat",
                Position = "Forvet",
                StartOn = matches[5].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
            viewModels.Add(new PlayerAdvertMenuViewModel()
            {
                City = matches[6].City,
                District = matches[6].District,
                Day = matches[6].Day,
                Month = matches[6].Month,
                Year = matches[6].Year,
                Name = "Umurcan",
                Position = "Defans",
                StartOn = matches[6].StartOn,
                ProfilePicturePath = "examplePerson2.jpg",
                FieldName = matches[0].FieldName

            });
        }

        private void Fill()
        {
            //colSearchPlayers.ItemsSource = null;
            colSearchPlayers.ItemsSource = viewModels;
        }

        private async void ColSearchPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //await Navigation.PushAsync(new PlayerAdvertDetailsPage(viewModels[0]));

        }
    }
}