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

            TrackListView.ItemsSource = boundTracks;

            var tracks = repository.Tracks();
            if (tracks.Count() == 0)
            {
                LoadSampleMusic();
            }
            else
            {
                foreach (var track in tracks)
                {
                    LoadPicture(track);
                    boundTracks.Add(track);
                }
            }
        }

        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();
        private readonly ITrackRepository repository = new TrackRepository();

        private void LoadSampleMusic()
        {
            var directory = new DirectoryInfo(@"C:\Users\Public\Music\Sample Music");
            foreach (var file in directory.GetFiles())
            {
                if (file.FullName.EndsWith(".mp3"))
                {
                    var tagFile = GetTagFile(file.FullName);

                    var track = GetTrack(file.FullName, tagFile.Tag);

                    boundTracks.Add(track);
                    repository.Save(track);
                }
            }
        }

        private TagLib.File GetTagFile(string path)
        {
            return TagLib.File.Create(path);
        }

        private void LoadPicture(ITrack track)
        {
            var file = GetTagFile(track.Path);
            if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
            {
                track.ImageData = file.Tag.Pictures[0].Data;
            }
        }

        private ITrack GetTrack(string path, Tag tag)
        {
            var track = new Track() { Path = path };

            if (!string.IsNullOrEmpty(tag.Title))
                track.Title = tag.Title;

            if (!string.IsNullOrEmpty(tag.Album))
                track.Album = tag.Album;

            track.TrackNumber = tag.Track;
            track.DiscNumber = tag.Disc;

            if (!string.IsNullOrEmpty(tag.JoinedGenres))
                track.Genre = tag.JoinedGenres;

            if (!string.IsNullOrEmpty(tag.JoinedPerformers))
                track.Artist = tag.JoinedPerformers;

            if (tag.Year > 0 && tag.Year < int.MaxValue)
                track.ReleaseDate = new DateTime((int)tag.Year, 1, 1);

            if (tag.Pictures.Length > 0)
                track.ImageData = tag.Pictures[0].Data;

            //"http://2.bp.blogspot.com/_mV-M8K3UNgg/SO4eaOKhtcI/AAAAAAAAEHI/a8WtGmHtZSA/s400/00+Mr+Scruff+-+Ninja+Tuna.jpg";

            return track;
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = TrackListView.SelectedItem as ITrack;
            if (track != null)
            {
                TrackPropertiesBorder.Visibility = System.Windows.Visibility.Visible;
                TrackProperties.DataContext = track;
            }
            else
            {
                TrackPropertiesBorder.Visibility = System.Windows.Visibility.Collapsed;
                TrackProperties.DataContext = null;
            }
        }
    }
}
