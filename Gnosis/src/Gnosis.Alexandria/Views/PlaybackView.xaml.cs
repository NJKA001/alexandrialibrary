using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using log4net;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for PlaybackView.xaml
    /// </summary>
    public partial class PlaybackView : UserControl
    {
        public PlaybackView()
        {
            InitializeComponent();
        }

        private static ILog log = LogManager.GetLogger(typeof(PlaybackView));
        private ITrackController trackController;
        private IPlaybackController playbackController;

        private void NowPlayingElapsedSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            try
            {
                playbackController.BeginSeek();
            }
            catch (Exception ex)
            {
                log.Error("PlaybackView.NowPlayingElapsedSlider_DragStarted", ex);
            }
        }

        private void NowPlayingElapsedSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            try
            {
                playbackController.Seek(Convert.ToInt32(NowPlayingElapsedSlider.Value * 1000));
            }
            catch (Exception ex)
            {
                log.Error("PlaybackView.NowPlayingElapsedSlider_DragCompleted", ex);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousTrack();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (playbackController.CurrentTrack == null)
            {
                //TODO: Use a more efficient call than this
                var selectedTrack = trackController.GetSelectedTrack();
                
                if (selectedTrack != null)
                {
                    SetNowPlaying(selectedTrack);
                }
                else if (trackController.TrackCount > 0) //boundTracks.Count > 0)
                {
                    SetNowPlaying(trackController.GetTrackAt(0)); //boundTracks[0]);
                }
            }

            PlayCurrentTrack();
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            PlayNextTrack();
        }

        public void Initialize(ITrackController trackController, IPlaybackController playbackController)
        {
            this.trackController = trackController;
            this.playbackController = playbackController;

            playButtonImage.DataContext = playbackController.Status;
        }

        public void SetNowPlaying(ITrack track)
        {
            playbackController.Load(track);

            NowPlayingMarquee.Dispatcher.Invoke((Action)delegate()
            {
                NowPlayingMarquee.DataContext = playbackController.CurrentTrack;
                NowPlayingMarquee.Visibility = playbackController.CurrentTrack != null ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        public void PlayPreviousTrack()
        {
            if (playbackController.CurrentTrack != null)
            {
                playbackController.Stop();
                var index = trackController.IndexOf(playbackController.CurrentTrack) - 1;  //boundTracks.IndexOf(playbackController.CurrentTrack) - 1;
                if (index == -1)
                    index = trackController.TrackCount - 1; //boundTracks.Count - 1;

                SetNowPlaying(trackController.GetTrackAt(index)); //boundTracks[index]);
                PlayCurrentTrack();
            }
        }

        public void PlayNextTrack()
        {
            if (playbackController.CurrentTrack != null)
            {
                playbackController.Stop();
                var index = trackController.IndexOf(playbackController.CurrentTrack) + 1; //boundTracks.IndexOf(playbackController.CurrentTrack) + 1;
                if (index == trackController.TrackCount) //boundTracks.Count)
                    index = 0;

                SetNowPlaying(trackController.GetTrackAt(index)); //boundTracks[index]);
                PlayCurrentTrack();
            }
        }

        public void PlayCurrentTrack()
        {
            playbackController.Play();
            NowPlayingElapsedSlider.Dispatcher.Invoke((Action)delegate { NowPlayingElapsedSlider.Maximum = playbackController.CurrentDuration.TotalSeconds; });
        }
    }
}
