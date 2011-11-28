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

using Gnosis.Alexandria.Contexts;
using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using Gnosis.Alexandria.Repositories.Feeds;
using Gnosis.Alexandria.Repositories.Tracks;

using Gnosis.Utilities;
using Gnosis.Audio.Fmod;
using log4net;
using Gnosis.Alexandria.Events;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            try
            {
                InitializeComponent();

                log4net.Config.XmlConfigurator.Configure();
                logger.Info("MainWindow.ctor: started");

                tagController = new TagController();
                trackController = new TrackController(oldTrackRepository, tagController);
                sourceController = new SourceController(sourceRepository, trackController);
                playbackController = new PlaybackController(trackController);
                sourceView.Initialize(sourceController, trackController, tagController);
                searchView.Initialize(trackController, tagController);
                playbackView.Initialize(trackController, playbackController);
                mediaPropertyView.Initialize(trackController, tagController);
                mediaListView.Initialize(trackController, tagController, playbackView, mediaPropertyView);

                sourceView.SourceLoaded += sourceLoaded;
                playbackController.CurrentTrackPlayed += currentTrackPlayed;
                playbackController.CurrentTrackPaused += currentTrackPaused;
                playbackController.CurrentTrackStopped += currentTrackStopped;
                playbackController.CurrentTrackEnded += currentTrackEnded;
                trackController.SourceLoadCompleted += sourceLoadCompleted;

                context = new MultiThreadedContext(this.Dispatcher);

                feedRepository = new FeedRepository();
                feedRepository.Initialize();

                trackRepository = new TrackRepository();
                trackRepository.Initialize();

                //var track = new Models.Tracks.Track();
                //track.Initialize(new EntityInitialState(context, logger));
                //track.Location = new Uri(@"file:///C:/Users/Dan/Music/Tool/Undertow/01%20Intolerance.mp3");
                //var trackId = track.Id;
                //track.AddIdentifier(new Uri("http://allmusic.com"), "R   169347");
                //track.AddIdentifier(new Uri("http://musicbrainz.org/trackId"), "9afffc81-c4b9-46aa-baef-232a45817158");
                //trackRepository.Save(new List<Models.Tracks.ITrack> { track });
                //var x = trackRepository.Lookup(trackId);
                //var y = x;
                //var feeds = feedRepository.Search();
                //var x = feeds.Count();
                //var entityInfo = new EntityInfo(typeof(Models.Tracks.ITrack));
                //var y = entityInfo;

                /*
                var feed = new Models.Feeds.Feed();
                feed.Initialize(new EntityInitialState(context, logger));
                feed.Location = new Uri("http://example.com/feeds/exmaple.xml");
                feed.Authors = "Neal Stephenson; William Gibson";
                var x = feed.AuthorTags.Count();
                var firstIsNew = feed.AuthorTags.FirstOrDefault().IsNew();
                var lastIsNew = feed.AuthorTags.LastOrDefault().IsNew();
                feedRepository.Save(new List<Models.Feeds.IFeed> { feed });
                var lookup = feedRepository.Lookup(feed.Id);
                var y = lookup.AuthorTags.Count();
                 */
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.ctor", ex);
            }
        }

        private static readonly log4net.ILog log = LogManager.GetLogger(typeof(MainView));
        
        private readonly IContext context;
        private readonly ILogger logger = new Log4NetLogger(log);
        private readonly ITrackRepository trackRepository;
        private readonly IFeedRepository feedRepository;

        private readonly IOldRepository<IOldTrack> oldTrackRepository = new OldTrackRepository();
        private readonly IOldRepository<ISource> sourceRepository = new OldSourceRepository();
        private readonly ITagController tagController;
        private readonly ITrackController trackController;
        private readonly ISourceController sourceController;
        private readonly IPlaybackController playbackController;
        private bool currentTrackIsEnding;

        private string GetTitle()
        {
            return "Alexandria" +
                ((playbackController != null && playbackController.CurrentTrack != null) ?
                " - " + playbackController.CurrentTrack.Title : string.Empty);
        }

        private void currentTrackPlayed(object sender, EventArgs args)
        {
            playThumbnailButton.Dispatcher.Invoke((Action)delegate
            {
                Title = GetTitle();
                playThumbnailButton.IsEnabled = false;
                pauseThumbnailButton.IsEnabled = true;
            });
        }

        private void currentTrackPaused(object sender, EventArgs args)
        {
            playThumbnailButton.Dispatcher.Invoke((Action)delegate
            {
                playThumbnailButton.IsEnabled = true;
                pauseThumbnailButton.IsEnabled = false;
            });
        }

        private void currentTrackStopped(object sender, EventArgs args)
        {
            playThumbnailButton.Dispatcher.Invoke((Action)delegate
            {
                playThumbnailButton.IsEnabled = true;
                pauseThumbnailButton.IsEnabled = false;
            });
        }

        private void currentTrackEnded(object sender, EventArgs args)
        {
            if (!currentTrackIsEnding)
            {
                currentTrackIsEnding = true;
                playbackView.PlayNextTrack();
                currentTrackIsEnding = false;
            }
        }

        private void sourceLoaded(object sender, SourceLoadedEventArgs args)
        {
            try
            {
                var source = args.Source;
                if (source is FileSystemSource || source is DirectorySource)
                {
                    sourceController.LoadDirectories(args.Source);
                }

                if (source is YouTubeVideoSource || (source.Path != null && source.Path.StartsWith("http://www.gutenberg.org"))) // || (source.Path != null && !source.Path.EndsWith(".mp3") && !source.Path.EndsWith(".wav")))
                {
                    System.Diagnostics.Process.Start(source.Path);
                }

                trackController.Load(source, this);
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.SourceLoaded", ex);
            }
        }

        private void sourceLoadCompleted(object sender, EventArgs args)
        {
            try
            {
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
                logger.Error("MainWindow.sourceLoadedCompleted", ex);
            }
        }

        private void backCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playbackView.PlayPreviousTrack();
        }

        private void playCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playbackView.PlayCurrentTrack();
        }

        private void pauseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playbackView.PlayCurrentTrack();
        }

        private void forwardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playbackView.PlayNextTrack();
        }

        private void command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (playbackController != null && playbackController.CurrentTrack != null);
        }

        private void pauseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (playbackController != null && playbackController.CurrentTrack != null);
        }
    }
}
