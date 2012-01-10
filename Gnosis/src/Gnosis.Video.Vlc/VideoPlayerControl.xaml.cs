using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Threading;

using Gnosis.Video.Vlc.Events;
using Gnosis.Video.Vlc.Media;
using Gnosis.Video.Vlc.Players;

namespace Gnosis.Video.Vlc
{
    /// <summary>
    /// Interaction logic for VideoPlayerControl.xaml
    /// </summary>
    public partial class VideoPlayerControl
        : System.Windows.Controls.UserControl, Gnosis.IVideoPlayer, INotifyPropertyChanged
    {
        private ILogger logger;
        private Func<IVideoHost> getHost;
        private IVideoHost currentHost;

        IMediaPlayerFactory playerFactory;
        IVlcVideoPlayer player;
        IVlcMedia media;
        private volatile bool seekIsPending;

        private TimeSpan duration;
        private TimeSpan elapsed;

        private System.Windows.Forms.Panel panel;

        public VideoPlayerControl()
        {
            InitializeComponent();

            playButton.DataContext = this;
            pauseButton.DataContext = this;
            volumeSlider.DataContext = this;
        }

        void Events_PlayerStopped(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                InitControls();
            }));
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                InitControls();
            }));

            OnEnded();
        }

        private void InitControls()
        {
            elapsedSlider.Value = 0;
            elapsedLabel.Content = "00:00:00";
            durationLabel.Content = "00:00:00";
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                var elapsed = TimeSpan.FromMilliseconds(e.NewTime);
                elapsedLabel.Content = elapsed.ToString().Substring(0, 8);
                this.elapsed = elapsed;
            }));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (!seekIsPending)
                {
                    elapsedSlider.Value = (double)e.NewPosition;
                }
            }));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Open(ofd.FileName);
            }
        }

        private void Open(string fileName)
        {
            //Dispatcher.Invoke(new Action(() => textBlock1.Text = fileName), DispatcherPriority.DataBind);

            media = playerFactory.CreateMedia<IVlcMediaFromFile>(fileName);
            media.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
            media.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);

            player.Open(media);
            media.Parse(true);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (playbackState != Gnosis.PlaybackState.Paused)
            {
                Play();
            }
            else
            {
                Resume();
            }
            //m_player.Play();
        }

        void Events_StateChanged(object sender, MediaStateChange e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {

            }));
        }

        void Events_DurationChanged(object sender, MediaDurationChange e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                var duration = TimeSpan.FromMilliseconds(e.NewDuration);
                durationLabel.Content = duration.ToString().Substring(0, 8);
                this.duration = duration;
            }));
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pause();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.pauseButton_Click", ex);
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stop();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.stopButton_Click", ex);
            }
        }

        private void muteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleMute();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.muteButton_Click", ex);
            }
        }

        private void ToggleMute()
        {
            if (player.Mute)
                Unmute();
            else
                Mute();
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (player != null)
            {
                player.Volume = (int)e.NewValue;
            }
        }

        private void elapsedSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            player.Position = (float)elapsedSlider.Value;
            seekIsPending = false;
        }

        private void elapsedSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            seekIsPending = true;
        }

        private void centerLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (playbackPanel.Visibility == Visibility.Collapsed)
                {
                    playbackPanel.Visibility = Visibility.Visible;
                    centerLabel.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.centerLabel_MouseEnter", ex);
            }
        }

        private void leftSideLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (playbackPanel.Visibility == Visibility.Visible)
                {
                    playbackPanel.Visibility = Visibility.Collapsed;
                    centerLabel.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.leftSideLabel_MouseEnter", ex);
            }
        }

        private void rightSideLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (playbackPanel.Visibility == Visibility.Visible)
                {
                    playbackPanel.Visibility = Visibility.Collapsed;
                    centerLabel.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.rightSideLabel_MouseEnter", ex);
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnPreviousItemSelected();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.previousButton_Click", ex);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnNextItemSelected();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.nextButton_Click", ex);
            }
        }

        public void Initialize(ILogger logger, Func<IVideoHost> getHost)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (getHost == null)
                throw new ArgumentNullException("getHost");

            this.logger = logger;
            this.getHost = getHost;

            panel = new System.Windows.Forms.Panel();
            panel.BackColor = System.Drawing.Color.Black;
            formHost.Child = panel;

            playerFactory = new MediaPlayerFactory(logger);
            player = playerFactory.CreatePlayer<IVlcVideoPlayer>();

            this.DataContext = player;

            player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            player.WindowHandle = panel.Handle;
            volumeSlider.Value = player.Volume;
        }

        private PlaybackState playbackState = PlaybackState.None;

        public PlaybackState PlaybackState
        {
            get { return playbackState; }
            private set
            {
                playbackState = value;
                PropertyHasChanged("PlaybackState");
                PropertyHasChanged("PlayVisibility");
                PropertyHasChanged("PauseVisibility");
            }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public TimeSpan Elapsed
        {
            get { return elapsed; }
        }

        public bool SeekIsPending
        {
            get { return seekIsPending; }
        }

        public bool IsMuted
        {
            get { return player != null && player.Mute; }
        }

        private void EnsureHostIsOpen()
        {
            if (currentHost == null || !currentHost.IsOpen)
            {
                currentHost = getHost();
                Action openAction = () => currentHost.Open(this);
                Dispatcher.Invoke(openAction);
            }
        }

        public void Load(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                logger.Info("VideoPlayerControl.Load: " + location.LocalPath);

                EnsureHostIsOpen();

                if (location.IsFile)
                {
                    Open(location.LocalPath);
                    OnLoaded();
                    //m_player.Play();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Load", ex);
            }
        }

        public void Play()
        {
            try
            {
                EnsureHostIsOpen();

                player.Play();
                PlaybackState = Gnosis.PlaybackState.Playing;
                OnPlayed();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Play", ex);
            }
        }

        public void Pause()
        {
            try
            {
                if (playbackState == Gnosis.PlaybackState.Playing)
                {
                    player.Pause();
                    PlaybackState = Gnosis.PlaybackState.Paused;
                    OnPaused();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Pause", ex);
            }
        }

        public void Resume()
        {
            try
            {
                if (playbackState == Gnosis.PlaybackState.Paused)
                {
                    player.Play();
                    PlaybackState = Gnosis.PlaybackState.Playing;
                    OnResumed();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Resume", ex);
            }
        }

        public void Stop()
        {
            try
            {
                player.Stop();
                PlaybackState = Gnosis.PlaybackState.Stopped;
                OnStopped();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Stop", ex);
            }
        }

        public void Mute()
        {
            try
            {
                player.Mute = true;
                PropertyHasChanged("VolumeEnabled");
                OnVolumeChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Mute", ex);
            }
        }

        public void Unmute()
        {
            try
            {
                player.Mute = false;
                PropertyHasChanged("VolumeEnabled");
                OnVolumeChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Unmute", ex);
            }
        }

        public void SetVolume(int volume)
        {
            if (volume < 0)
                throw new ArgumentException("volume cannot be less than zero");
            if (volume > 100)
                throw new ArgumentException("volume cannot be greater than 100");

            try
            {
                player.Volume = volume;
                OnVolumeChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.SetVolume", ex);
            }
        }

        private readonly IList<Action> loadedCallbacks = new List<Action>();
        private readonly IList<Action> playedCallbacks = new List<Action>();
        private readonly IList<Action> pausedCallbacks = new List<Action>();
        private readonly IList<Action> resumedCallbacks = new List<Action>();
        private readonly IList<Action> stoppedCallbacks = new List<Action>();
        private readonly IList<Action> endedCallbacks = new List<Action>();
        private readonly IList<Action> volumeChangedCallbacks = new List<Action>();
        private readonly IList<Action> previousItemCallbacks = new List<Action>();
        private readonly IList<Action> nextItemCallbacks = new List<Action>();

        private void OnLoaded()
        {
            foreach (var callback in loadedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in loaded callback", ex);
                }
            }
        }

        private void OnPlayed()
        {
            foreach (var callback in playedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in played callback", ex);
                }
            }
        }

        private void OnPaused()
        {
            foreach (var callback in pausedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in paused callback", ex);
                }
            }
        }

        private void OnResumed()
        {
            foreach (var callback in resumedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in resumed callback", ex);
                }
            }
        }

        private void OnStopped()
        {
            foreach (var callback in stoppedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in stopped callback", ex);
                }
            }
        }

        private void OnEnded()
        {
            foreach (var callback in endedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in ended callback", ex);
                }
            }
        }

        private void OnVolumeChanged()
        {
            foreach (var callback in volumeChangedCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in volume changed callback", ex);
                }
            }
        }

        private void OnPreviousItemSelected()
        {
            foreach (var callback in previousItemCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in previous item selected callback", ex);
                }
            }
        }

        private void OnNextItemSelected()
        {
            foreach (var callback in nextItemCallbacks)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    logger.Error("  VideoPlayerControl: Error in next item selected callback", ex);
                }
            }
        }

        public void AddLoadedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            loadedCallbacks.Add(callback);
        }

        public void AddPlayedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            playedCallbacks.Add(callback);
        }

        public void AddPausedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            pausedCallbacks.Add(callback);
        }

        public void AddResumedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            resumedCallbacks.Add(callback);
        }

        public void AddStoppedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            stoppedCallbacks.Add(callback);
        }

        public void AddEndedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            endedCallbacks.Add(callback);
        }

        public void AddVolumeChangedCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            volumeChangedCallbacks.Add(callback);
        }

        public void AddPreviousItemCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            previousItemCallbacks.Add(callback);
        }

        public void AddNextItemCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            nextItemCallbacks.Add(callback);
        }

        public Visibility PlayVisibility
        {
            get { return playbackState == Gnosis.PlaybackState.Playing ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility PauseVisibility
        {
            get { return playbackState == Gnosis.PlaybackState.Playing ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool VolumeEnabled
        {
            get { return player != null && player.Mute ? false : true; }
        }

        #region INotifyPropertyChanged Members

        private void PropertyHasChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
