using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.ViewModels;
using Gnosis.Image;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchResultView.xaml
    /// </summary>
    public partial class SearchResultView : UserControl
    {
        public SearchResultView()
        {
            InitializeComponent();

            searchResultListBox.DataContext = this;

            //Radiohead http://userserve-ak.last.fm/serve/252/7856747.jpg
            //Pablo+Honey http://ecx.images-amazon.com/images/I/61ddvFn%2BwRL._SS500_.jpg
            //The+Bends http://ecx.images-amazon.com/images/I/41JZQ2VqdjL._SL500_AA300_.jpg
            //OK+Computer http://ecx.images-amazon.com/images/I/51ycLXtgGTL._SS400_.jpg
            //Kid+A http://ecx.images-amazon.com/images/I/51ZWr2hIj8L._SL500_AA300_.jpg
            //Amnesiac http://ecx.images-amazon.com/images/I/51asaTXwMNL._SS400_.jpg
            //Hail+to+the+Thief http://ecx.images-amazon.com/images/I/613LIFtOT8L._SL500_AA280_.jpg
            //In+Rainbows http://ecx.images-amazon.com/images/I/61EROeqAf-L._SS500_.jpg
            //The+King+of+Limbs http://ecx.images-amazon.com/images/I/51ztVeVxk0L._SS500_.jpg
            //MP3 pack://application:,,,/Images/File Audio MP3-01.png

            var album1 = new AlbumViewModel("Pablo Honey", new DateTime(1993, 1, 1), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/61ddvFn%2BwRL._SS500_.jpg")));
            var album2 = new AlbumViewModel("The Bends", new DateTime(1995, 1, 1), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/41JZQ2VqdjL._SL500_AA300_.jpg")));
            var album3 = new AlbumViewModel("OK Computer", new DateTime(1997, 1, 1), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/51ycLXtgGTL._SS400_.jpg")));
            var album4 = new AlbumViewModel("Kid A", new DateTime(2000, 1, 1), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/51ZWr2hIj8L._SL500_AA300_.jpg")));
            var album5 = new AlbumViewModel("Amnesiac", new DateTime(2001, 6, 5), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/51asaTXwMNL._SS400_.jpg")));
            var album6 = new AlbumViewModel("Hail to the Thief", new DateTime(2003, 6, 10), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/613LIFtOT8L._SL500_AA280_.jpg")));
            var album7 = new AlbumViewModel("In Rainbows", new DateTime(2008, 1, 1), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/61EROeqAf-L._SS500_.jpg")));
            var album8 = new AlbumViewModel("The King of Limbs", new DateTime(2011, 3, 29), new JpegImage(new Uri("http://ecx.images-amazon.com/images/I/51ztVeVxk0L._SS500_.jpg")));
            var artist = new ArtistViewModel("Radiohead", new DateTime(1985, 1, 1), DateTime.MaxValue, new JpegImage(new Uri("http://userserve-ak.last.fm/serve/252/7856747.jpg")), "Radiohead are an English rock band from Abingdon, Oxfordshire, formed in 1985. The band consists of Thom Yorke (vocals, guitars, keyboards), Jonny Greenwood (guitars, keyboards, other instruments), Ed O'Brien (guitars, backing vocals), Colin Greenwood (bass) and Phil Selway (drums, percussion).");
            artist.AddAlbum(album1);
            artist.AddAlbum(album2);
            artist.AddAlbum(album3);
            artist.AddAlbum(album4);
            artist.AddAlbum(album5);
            artist.AddAlbum(album6);
            artist.AddAlbum(album7);
            artist.AddAlbum(album8);
            var vm = new ArtistSearchResultViewModel(artist);
            results.Add(vm);
        }

        private readonly ObservableCollection<ISearchResultViewModel> results = new ObservableCollection<ISearchResultViewModel>();

        public IEnumerable<ISearchResultViewModel> Results
        {
            get { return results; }
        }

        private void audioFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void videoFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imageFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
