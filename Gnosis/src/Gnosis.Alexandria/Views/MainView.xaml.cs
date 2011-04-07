using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using ControlPrimatives=System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TagLib;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using Gnosis.Core;
using Gnosis.Fmod;
using log4net;
using Gnosis.Alexandria.Events;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();

            try
            {
                log.Info("MainWindow.ctor: started");

                tagController = new TagController();
                trackController = new TrackController(trackRepository, tagController);
                sourceController = new SourceController(sourceRepository, trackController);
                playbackController = new PlaybackController(trackController);
                sourceView.Initialize(sourceController, trackController, tagController);
                searchView.Initialize(trackController, tagController);
                playbackView.Initialize(trackController, playbackController);
                mediaPropertyView.Initialize(trackController, tagController);
                mediaListView.Initialize(trackController, tagController, playbackView, mediaPropertyView);

                sourceView.SourceLoaded += new EventHandler<SourceLoadedEventArgs>(SourceLoaded);
                playbackController.CurrentTrackEnded += CurrentTrackEnded;
            }
            catch (Exception ex)
            {
                log.Error("MainWindow.ctor", ex);
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));
        private readonly IRepository<ITrack> trackRepository = new TrackRepository();
        private readonly IRepository<ISource> sourceRepository = new SourceRepository();
        private readonly ITagController tagController;
        private readonly ITrackController trackController;
        private readonly ISourceController sourceController;
        private readonly IPlaybackController playbackController;

        private void CurrentTrackEnded(object sender, EventArgs args)
        {
            playbackView.PlayNextTrack();
        }

        private void SourceLoaded(object sender, SourceLoadedEventArgs args)
        {
            try
            {
                var source = args.Source;
                if (source is FileSystemSource || source is DirectorySource)
                {
                    sourceController.LoadDirectories(args.Source);
                }

                trackController.Load(source);

                if (trackController.TrackCount > 0)
                {
                    var track = trackController.GetTrackAt(0);
                    track.IsSelected = true;
                    playbackController.Reset();
                    playbackView.SetNowPlaying(track);
                    playbackView.PlayCurrentTrack();
                }
            }
            catch (Exception ex)
            {
                log.Error("MainWindow.SourceLoaded", ex);
            }
        }
    }
}
