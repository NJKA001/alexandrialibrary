using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for VideoPlayerWindow.xaml
    /// </summary>
    public partial class VideoPlayerWindow
        : Window, IVideoHost
    {
        public VideoPlayerWindow()
        {
            InitializeComponent();
        }

        private IVideoPlayer videoPlayer;

        protected override void OnClosed(EventArgs e)
        {
            IsOpen = false;

            if (videoPlayer != null)
            {
                videoPlayer.Stop();
            }

            if (videoHost.Child != null)
            {
                videoHost.Child = null;
            }

            base.OnClosed(e);
        }


        public bool IsOpen
        {
            get;
            private set;
        }

        public void Open(IVideoPlayer videoPlayer)
        {
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");

            this.videoPlayer = videoPlayer;

            videoHost.Child = videoPlayer as UIElement;

            IsOpen = true;

            Show();
        }
    }
}
