using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

using TagLib;

namespace Gnosis.Archon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TrackListView.ItemsSource = tracks;

            LoadSampleMusic();
            //AddSampleMusic2();
        }

        private readonly ObservableCollection<ITrack> tracks = new ObservableCollection<ITrack>();

        private void LoadSampleMusic()
        {
            //var path = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";
            //var file = TagLib.File.Create(path);
            //var tag = file.Tag;
            //var x = tag;

            var directory = new DirectoryInfo(@"C:\Users\Public\Music\Sample Music");
            foreach (var file in directory.GetFiles())
            {
                if (file.FullName.EndsWith(".mp3"))
                {
                    var tagFile = TagLib.File.Create(file.FullName);

                    var track = GetTrack(file.FullName, tagFile.Tag);

                    tracks.Add(track);
                }
            }
        }

        private ITrack GetTrack(string path, Tag tag)
        {
            var title = tag.Title;
            var album = tag.Album;
            var number = tag.Track;
            var artist = tag.JoinedPerformers;
            var releaseDate = (tag.Year > 0) ? new DateTime((int)tag.Year, 1, 1) : new DateTime(1900, 1, 1);
            ICollection<byte> image = (tag.Pictures.Length > 0) ? tag.Pictures[0].Data : null;

            return new Track { Title = title, Album = album, Artist = artist, Number = number, ReleaseDate = releaseDate, Image = image};
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = TrackListView.SelectedItem as ITrack;
            if (track != null)
            {
                //MessageBox.Show("Play this track " + track.Title, "TEST");
            }
        }
    }
}
