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
    public partial class IndexPage : TabbedPage
    {
        List<IndexPageViewModelField> FieldViewModels = new List<IndexPageViewModelField>();
        public IndexPage()
        {
            InitializeComponent();
            

            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Beşerler Halı Saha",
                CityAndTown = "İstanbul/Bakırköy",
                Cost = "Kapora 40 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName= "Şirinevler İçgücü Halı Saha",
                CityAndTown = "İstanbul/Şirinevler",
                Cost = "Kapora 35 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Dostluk Spor Kulübü Halı Saha",
                CityAndTown = "İstanbul/Beylikdüzü",
                Cost = "Kapora 50 ₺",
                Rate = "4.2 / 5"
            });
            FieldViewModels.Add(new IndexPageViewModelField()
            {
                PhotoPath = "indexImage4.jpg",
                PlaceName = "Nihat Yüceur Spor Kompleksi",
                CityAndTown = "İstanbul/Küçükçekmece",
                Cost = "Kapora 60 ₺",
                Rate = "4.2 / 5"
            });



            Fill();
        }

        private async void Fill()
        {
            collectionView1.ItemsSource = FieldViewModels;
        }
    }
}