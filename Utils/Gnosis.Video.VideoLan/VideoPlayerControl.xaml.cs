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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Medias;
using Vlc.DotNet.Wpf;

namespace Gnosis.Video.VideoLan
{
    /// <summary>
    /// Interaction logic for VideoPlayerControl.xaml
    /// </summary>
    public partial class VideoPlayerControl
        : UserControl, IVideoPlayer
    {
        #region Properties

        /// <summary>
        /// Used to indicate that the user is currently changing the position (and the position bar shall not be updated). 
        /// </summary>
        private bool positionChanging;

        #endregion

        #region Constructor / destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VlcPlayer"/> class.
        /// </summary>
        public VideoPlayerControl()
        {
            // Set libvlc.dll and libvlccore.dll directory path
            VlcContext.LibVlcDllsPath = "."; //@"C:\Program Files\VideoLAN\vlc-1.2.0";

            // Set the vlc plugins directory path
            VlcContext.LibVlcPluginsPath = @".\plugins"; //@"C:\Program Files\VideoLAN\vlc-1.2.0\plugins";

            /* Setting up the configuration of the VLC instance.
             * You can use any available command-line option using the AddOption function (see last two options). 
             * A list of options is available at 
             *     http://wiki.videolan.org/VLC_command-line_help
             * for example. */

            // Ignore the VLC configuration file
            VlcContext.StartupOptions.IgnoreConfig = true;

            // Enable file based logging
            VlcContext.StartupOptions.LogOptions.LogInFile = true;

            // Shows the VLC log console (in addition to the applications window)
#if DEBUG 
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
#endif

            // Set the log level for the VLC instance
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            // Disable showing the movie file name as an overlay
            VlcContext.StartupOptions.AddOption("--no-video-title-show");

            // Pauses the playback of a movie on the last frame
            VlcContext.StartupOptions.AddOption("--play-and-pause");

            VlcContext.StartupOptions.AddOption("--file-caching=2000");
            VlcContext.StartupOptions.AddOption("--ffmpeg-skiploopfilter=4");

            // Initialize the VlcContext
            VlcContext.Initialize();

            InitializeComponent();

            myVlcControl.VideoProperties.Scale = 2.0f;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;
            //myVlcControl.TimeChanged += VlcControlOnTimeChanged;
            //Closing += MainWindowOnClosing;
        }

        /// <summary>
        /// Main window closing event
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            // Close the context. 
            VlcContext.CloseAll();
        }

        public void ShutDown()
        {
            VlcContext.CloseAll();
        }

        #endregion

        #region Control playing

        /// <summary>
        /// Called if the Play button is clicked; starts the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Play();
        }

        /// <summary>
        /// Called if the Pause button is clicked; pauses the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Pause();
        }

        /// <summary>
        /// Called if the Stop button is clicked; stops the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();
            sliderPosition.Value = 0;
        }

        /// <summary>
        /// Called if the Open button is clicked; shows the open file dialog to select a media file to play. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonOpenClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();

            if (myVlcControl.Media != null)
            {
                myVlcControl.Media.ParsedChanged -= MediaOnParsedChanged;
            }

            var openFileDialog = new OpenFileDialog
            {
                Title = "Open media file for playback",
                FileName = "Media File",
                Filter = "All files |*.*"
            };

            // Process open file dialog box results
            if (openFileDialog.ShowDialog() != true)
                return;

            //textBlockOpen.Visibility = Visibility.Collapsed;

            myVlcControl.Media = new PathMedia(openFileDialog.FileName);
            myVlcControl.Media.ParsedChanged += MediaOnParsedChanged;
            myVlcControl.Play();

            /* Instead of opening a file for playback you can also connect to media streams using
             *     myVlcControl.Media = new LocationMedia(@"http://88.190.232.102:6404");
             *     myVlcControl.Play();
             */
        }

        /// <summary>
        /// Volume value changed by the user. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderVolumeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myVlcControl.AudioProperties.Volume = Convert.ToInt32(sliderVolume.Value);
        }

        /// <summary>
        /// Mute audio check changed
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void CheckboxMuteCheckedChanged(object sender, RoutedEventArgs e)
        {
            myVlcControl.AudioProperties.IsMute = checkboxMute.IsChecked == true;
        }

        #endregion

        /// <summary>
        /// Called by <see cref="VlcControl.Media"/> when the media information was parsed. 
        /// </summary>
        /// <param name="sender">Event sending media. </param>
        /// <param name="e">VLC event arguments. </param>
        private void MediaOnParsedChanged(MediaBase sender, VlcEventArgs<int> e)
        {
            Action action = () =>
                {
                    textBlock.Text = string.Format(
                        "Duration: {0:00}:{1:00}:{2:00}",
                        myVlcControl.Media.Duration.Hours,
                        myVlcControl.Media.Duration.Minutes,
                        myVlcControl.Media.Duration.Seconds);

                    sliderVolume.Value = myVlcControl.AudioProperties.Volume;
                    checkboxMute.IsChecked = myVlcControl.AudioProperties.IsMute;
                };

            this.Dispatcher.Invoke(action);
        }

        /// <summary>
        /// Called by the <see cref="VlcControl"/> when the media position changed during playback.
        /// </summary>
        /// <param name="sender">Event sennding control. </param>
        /// <param name="e">VLC event arguments. </param>
        private void VlcControlOnPositionChanged(VlcControl sender, VlcEventArgs<float> e)
        {
            if (positionChanging)
            {
                // User is currently changing the position using the slider, so do not update. 
                return;
            }

            Action action = () =>
                {
                    sliderPosition.Value = e.Data;
                };

            this.Dispatcher.Invoke(action);
        }

        private void VlcControlOnTimeChanged(VlcControl sender, VlcEventArgs<TimeSpan> e)
        {
            var duration = myVlcControl.Media.Duration;

            Action action = () =>
            {
                textBlock.Text = string.Format(
                    "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                    e.Data.Hours,
                    e.Data.Minutes,
                    e.Data.Seconds,
                    duration.Hours,
                    duration.Minutes,
                    duration.Seconds);
            };
            this.Dispatcher.Invoke(action);
        }

        #region Change position

        /// <summary>
        /// Start position changing, prevents updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            positionChanging = true;
            myVlcControl.PositionChanged -= VlcControlOnPositionChanged;
        }

        /// <summary>
        /// Stop position changing, re-enables updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myVlcControl.Position = (float)sliderPosition.Value;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;

            positionChanging = false;
        }

        /// <summary>
        /// Change position when the slider value is updated. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (positionChanging)
            {
                myVlcControl.Position = (float)e.NewValue;
            }
            //Update the current position text when it is in pause
            var duration = myVlcControl.Media == null ? TimeSpan.Zero : myVlcControl.Media.Duration;
            var time = TimeSpan.FromMilliseconds(duration.TotalMilliseconds * myVlcControl.Position);
            textBlock.Text = string.Format(
                "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                time.Hours,
                time.Minutes,
                time.Seconds,
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
        }

        #endregion

        public void Load(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            if (location.IsFile)
            {
                myVlcControl.Stop();
                myVlcControl.Media = new PathMedia(location.LocalPath);
                myVlcControl.Media.ParsedChanged += MediaOnParsedChanged;
                myVlcControl.Play();
            }
        }
    }
}
