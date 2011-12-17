using System;
using System.Collections.Generic;
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

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IPlaylistViewModel playlist;
        private IVideoPlayer videoPlayer;
        private VideoPlayerWindow videoPlayerWindow;

        public void Initialize(ILogger logger, IPlaylistViewModel playlist, IVideoPlayer videoPlayer)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (playlist == null)
                throw new ArgumentNullException("playlist");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");

            this.logger = logger;
            
            try
            {
                this.playlist = playlist;
                this.videoPlayer = videoPlayer;
                this.DataContext = playlist;

                var first = playlist.PlaylistItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsPlaying = true;
                    first.IsSelected = true;
                }

                var child = videoPlayer as UIElement;
                if (child != null)
                {
                    videoPlayerWindow = new VideoPlayerWindow();
                    videoPlayerWindow.SetVideoPlayerElement(child);
                    //videoPlayerWindow.Show();
                }
                else
                {
                    logger.Warn("  PlaylistView.Initialize: videoPlayer is not a valid UIEelement");
                }
            }
            catch (Exception ex)
            {
                logger.Error("PlaylistView.Initialize", ex);
            }
        }

        public void OnItemStarted(ITaskViewModel task)
        {
            var existing = playlist.PlaylistItems.Where(x => x.IsPlaying).FirstOrDefault();
            if (existing != null)
            {
                existing.IsPlaying = false;
            }

            var playing = playlist.PlaylistItems.Where(x => x.Id == task.CurrentItem.Id).FirstOrDefault();
            if (playing != null)
            {
                playing.IsPlaying = true;
            }
        }

        public void OnItemChanged(TaskItem item)
        {
            var existing = playlist.PlaylistItems.Where(x => x.IsPlaying).FirstOrDefault();
            if (existing != null)
            {
                existing.IsPlaying = false;
            }

            var playing = playlist.PlaylistItems.Where(x => x.Id == item.Id).FirstOrDefault();
            if (playing != null)
            {
                playing.IsPlaying = true;
            }
        }

        public void OnItemEnded(TaskItem item)
        {
            try
            {
                var playing = playlist.PlaylistItems.Where(x => x.Id == item.Id).FirstOrDefault();
                if (playing != null)
                {
                    playing.IsPlaying = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistView.HandlePlaylistResult", ex);
            }
        }
    }
}
