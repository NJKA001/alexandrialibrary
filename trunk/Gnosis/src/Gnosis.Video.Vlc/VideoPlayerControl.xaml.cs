﻿using System;
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
    public partial class VideoPlayerControl : System.Windows.Controls.UserControl, Gnosis.IVideoPlayer
    {
        private ILogger logger;
        private Func<IVideoHost> getHost;
        private IVideoHost currentHost;

        IMediaPlayerFactory m_factory;
        IVlcVideoPlayer m_player;
        IVlcMedia m_media;
        private volatile bool m_isDrag;

        private System.Windows.Forms.Panel panel;

        public VideoPlayerControl()
        {
            InitializeComponent();
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
        }

        private void InitControls()
        {
            slider1.Value = 0;
            label1.Content = "00:00:00";
            label3.Content = "00:00:00";
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                label1.Content = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8);
            }));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (!m_isDrag)
                {
                    slider1.Value = (double)e.NewPosition;
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

            m_media = m_factory.CreateMedia<IVlcMediaFromFile>(fileName);
            m_media.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
            m_media.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);

            m_player.Open(m_media);
            m_media.Parse(true);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            m_player.Play();
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
                label3.Content = TimeSpan.FromMilliseconds(e.NewDuration).ToString().Substring(0, 8);
            }));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            m_player.Pause();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            m_player.Stop();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            m_player.ToggleMute();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_player != null)
            {
                m_player.Volume = (int)e.NewValue;
            }
        }

        private void slider1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            m_player.Position = (float)slider1.Value;
            m_isDrag = false;
        }

        private void slider1_DragStarted(object sender, DragStartedEventArgs e)
        {
            m_isDrag = true;
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

            m_factory = new MediaPlayerFactory(logger);
            m_player = m_factory.CreatePlayer<IVlcVideoPlayer>();

            this.DataContext = m_player;

            m_player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            m_player.WindowHandle = panel.Handle;
            slider2.Value = m_player.Volume;
        }

        public void Load(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                logger.Info("VideoPlayerControl.Load: " + location.LocalPath);

                if (currentHost == null || !currentHost.IsOpen)
                {
                    currentHost = getHost();
                    Action openAction = () => currentHost.Open(this);
                    Dispatcher.Invoke(openAction);
                }

                //if (location.IsFile)
                //{
                
                Open(location.LocalPath);
                m_player.Play();
                //}
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Load", ex);
            }
        }

        public void Stop()
        {
            try
            {
                m_player.Stop();
            }
            catch (Exception ex)
            {
                logger.Error("  VideoPlayerControl.Stop", ex);
            }
        }
    }
}
