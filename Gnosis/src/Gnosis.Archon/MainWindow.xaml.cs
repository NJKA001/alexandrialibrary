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

using Gnosis.Core;
using Gnosis.Fmod;

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

            player.CurrentAudioStreamEnded += new EventHandler<EventArgs>(CurrentTrackEnded);
        }

        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();
        private readonly ITrackRepository repository = new TrackRepository();
        private readonly IAudioPlayer player = new AudioPlayer(new Fmod.AudioStreamFactory()) { PlayToggles = true };
        private ITrack currentTrack;

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

        private ITrack GetTrack(string path, TagLib.Tag tag)
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

            return track;
        }

        private ITrack GetSelectedTrack()
        {
            return TrackListView.SelectedItem as ITrack;
        }

        private string GetPlayButtonContent()
        {
            return player.CurrentAudioStream.PlaybackState == PlaybackState.Playing ? "Pause" : "Play";
        }

        private Uri GetCurrentUri()
        {
            return (currentTrack != null) ? new Uri(currentTrack.Path, UriKind.Absolute) : null;
        }

        private void SetCurrentTrack(ITrack track)
        {
            if (currentTrack != null)
                currentTrack.PlaybackStatus = null;

            currentTrack = track;
            currentTrack.PlaybackStatus = "Now Playing";
            NowPlayingMarquee.DataContext = currentTrack;
            NowPlayingMarquee.Visibility = currentTrack != null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PlayCurrentTrack()
        {
            if (currentTrack != null)
            {
                var currentUri = GetCurrentUri();
                if (player.CurrentAudioStream == null)
                {
                    player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                }
                else
                {
                    var streamUri = new Uri(player.CurrentAudioStream.Path, UriKind.Absolute);
                    if (currentUri != streamUri)
                    {
                        player.Stop();
                        player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                    }
                }

                player.Play();
                PlayButton.Content = GetPlayButtonContent();
            }
        }

        private void PlayPreviousTrack()
        {
            if (currentTrack != null)
            {
                player.Stop();
                var index = boundTracks.IndexOf(currentTrack) - 1;
                if (index == -1)
                    index = boundTracks.Count - 1;

                SetCurrentTrack(boundTracks[index]);
                PlayCurrentTrack();
            }
        }

        private void PlayNextTrack()
        {
            if (currentTrack != null)
            {
                player.Stop();
                var index = boundTracks.IndexOf(currentTrack) + 1;
                if (index == boundTracks.Count)
                    index = 0;

                SetCurrentTrack(boundTracks[index]);
                PlayCurrentTrack();
            }
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = GetSelectedTrack();
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

        private void CurrentTrackEnded(object sender, EventArgs args)
        {
            PlayNextTrack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousTrack();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrack == null)
            {
                var selectedTrack = GetSelectedTrack();
                if (selectedTrack != null)
                {
                    SetCurrentTrack(selectedTrack);
                }
                else if (boundTracks.Count > 0)
                {
                    SetCurrentTrack(boundTracks[0]);
                }
            }

            PlayCurrentTrack();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PlayNextTrack();
        }

        private void TrackListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetCurrentTrack(GetSelectedTrack());
            PlayCurrentTrack();
        }
    }
}
