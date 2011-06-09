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
using Gnosis.Alexandria.Repositories.Feeds;
using Gnosis.Alexandria.Repositories.Tracks;
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
        private Models.Feeds.IFeed GetTestFeed(IContext context, ILogger logger)
        {
            var feed = new Models.Feeds.Feed();
            feed.Initialize(new EntityInitialState(context, logger));
            feed.Authors = "Bill Simmons";
            feed.Contributors = "Joe House, Marc Stein, John Hollinger";
            feed.Copyright = "c 2009-2011";
            feed.Description = "Sports etc.";
            feed.FeedIdentifier = "12345ABC";
            feed.Generator = "espn.go.com";
            feed.Language = "en-us";
            feed.Location = new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045");
            feed.MediaType = "application/xml+rss";
            feed.OriginalLocation = new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045");
            feed.PublishedDate = new DateTime(2009, 2, 13);
            feed.UpdatedDate = new DateTime(2011, 6, 7);
            feed.Title = "BS Report";
            feed.IconPath = new Uri("http://assets.espn.go.com/i/espnradio/podcast/bsreport_subway_300.jpg"); 
            feed.ImagePath = new Uri("http://assets.espn.go.com/i/espnradio/podcast/bsreport_subway_300.jpg");
            feed.AddCategory(new Models.Feeds.FeedCategory(feed.Id, UriExtensions.EmptyUri, "Sports", "Sports"));
            feed.AddCategory(new Models.Feeds.FeedCategory(feed.Id, UriExtensions.EmptyUri, "Comedy", "Comedy"));
            feed.AddLink(new Models.Feeds.FeedLink(feed.Id, "self", new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045"), "application/xml+rss", 0, "en-us"));
            feed.AddLink(new Models.Feeds.FeedLink(feed.Id, "alt", new Uri("http://espn.go.com/espnradio"), "text/html", 0, "en-us"));
            feed.AddLink(new Models.Feeds.FeedLink(feed.Id, "alt", new Uri("http://espn.go.com/epsnradio?lang=es-mx"), "text/html", 0, "es-mx"));
            feed.AddMetadatum(new Models.Feeds.FeedMetadata(feed.Id, "text/plain", UriExtensions.EmptyUri, "tag", "Bill Simmons"));
            feed.AddMetadatum(new Models.Feeds.FeedMetadata(feed.Id, "application/xml", UriExtensions.EmptyUri, "marquee", "<marquee><title>BS Report</title><subtitle>with Bill Simmons</subtitle></marquee>"));
            
            var item = new Models.Feeds.FeedItem();
            item.Initialize(new EntityInitialState(context, logger, feed.Id, 0));
            item.Authors = "Bill Simmons";
            item.Contributors = "Joe House, Joe Mead";
            item.Copyright = "Copyright ESPN 2011"; 
            item.FeedItemIdentifier = "ZYZ235AMQ3792206";
            item.Summary = "Bill previews the NBA Finals with ESPN experts";
            item.PublishedDate = new DateTime(2011, 6, 5);
            item.UpdatedDate = new DateTime(2011, 6, 5);
            item.Title = "NBA Finals Preview (Part 1)";
            item.TitleMediaType = "text/plain";
            //item.AddCategory(new Models.Feeds.FeedCategory(item.Id, UriExtensions.EmptyUri, "Basketball", "Basketball"));
            item.AddLink(new Models.Feeds.FeedLink(item.Id, "self", new Uri("http://espn.go.com/espnradio/media/xyz1.mp3"), "audio/mpeg", 0, "en-us"));
            item.AddMetadatum(new Models.Feeds.FeedMetadata(item.Id, "text/plain", UriExtensions.EmptyUri, "rating", "4/5"));
            item.AddMetadatum(new Models.Feeds.FeedMetadata(item.Id, "application/xml", UriExtensions.EmptyUri, "rating", "<rating><score>4</score><max>5</max></rating>"));
            feed.AddItem(item);

            var item2 = new Models.Feeds.FeedItem();
            item2.Initialize(new EntityInitialState(context, logger, feed.Id, 0));
            item2.Authors = "Bill Simmons";
            item2.Contributors = "Joe House, Joe Mead";
            item2.Copyright = "Copyright ESPN 2011";
            item2.FeedItemIdentifier = "ZYZ235AMQ38564587";
            item2.Summary = "Bill previews the NBA Finals with ESPN experts";
            item2.PublishedDate = new DateTime(2011, 6, 5);
            item2.UpdatedDate = new DateTime(2011, 6, 5);
            item2.Title = "NBA Finals Preview (Part 2)";
            item2.TitleMediaType = "text/plain";
            item2.AddCategory(new Models.Feeds.FeedCategory(item.Id, UriExtensions.EmptyUri, "Basketball", "Basketball"));
            item2.AddLink(new Models.Feeds.FeedLink(item.Id, "self", new Uri("http://espn.go.com/espnradio/media/xyz2.mp3"), "audio/mpeg", 0, "en-us"));
            item2.AddMetadatum(new Models.Feeds.FeedMetadata(item.Id, "text/plain", UriExtensions.EmptyUri, "rating", "4/5"));
            item2.AddMetadatum(new Models.Feeds.FeedMetadata(item.Id, "application/xml", UriExtensions.EmptyUri, "rating", "<rating><score>4</score><max>5</max></rating>"));
            feed.AddItem(item2);

            return feed;
        }

        private void ModifyTestFeed(Models.Feeds.IFeed feed)
        {
            feed.Title = "Some other title";
            feed.FeedIdentifier = "NEW-ID-5634634636-NEW";
            feed.Generator = "other-generator";

            var firstCategory = feed.Categories.FirstOrDefault();
            feed.RemoveCategory(firstCategory);


            feed.RemoveItem(feed.Items.LastOrDefault());
            feed.Items.FirstOrDefault().RemoveMetadatum(feed.Items.FirstOrDefault().Metadata.FirstOrDefault());
            feed.Items.FirstOrDefault().AddCategory(new Models.Feeds.FeedCategory(feed.Items.FirstOrDefault().Id, new Uri("http://example.com/scheme"), "Misc.", "Misc."));
            feed.AddMetadatum(new Models.Feeds.FeedMetadata(feed.Id, "text/html", new Uri("http://other.com/1/different-scheme"), "review", "<html><body><p>This feed is awesome</p><p>I <b>love</b> it so much!</p></body></html>"));
            feedRepository.Save(new List<Models.Feeds.IFeed> { feed });
        }

        public MainWindow()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();


            try
            {
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

                context = new ModelContext(this.Dispatcher);

                feedRepository = new FeedRepository(context, logger);
                //trackRepository = new TrackRepository(context, factory);

                try
                {
                    var feed = feedRepository.Lookup(new LookupByLocation(new Uri("http://espn.go.com/espnradio/feeds/rss/podcast.xml?id=2864045")));
                    if (feed == null)
                    {
                        feed = GetTestFeed(context, logger);
                        feedRepository.Save(new List<Models.Feeds.IFeed> { feed });
                    }
                    else ModifyTestFeed(feed);
                }
                catch (Exception ex)
                {
                    logger.Error("feedRepository.Save()", ex);
                }

                try
                {
                    var allFeeds = feedRepository.Search();
                    var x = allFeeds.Count();
                }
                catch (Exception ex)
                {
                    logger.Error("feedRepository.Search()", ex);
                }
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.ctor", ex);
            }
        }

        private static readonly log4net.ILog log = LogManager.GetLogger(typeof(MainWindow));
        
        private readonly IContext context;
        private readonly ILogger logger = new Logger(log);
        //private readonly IRepository<Gnosis.Alexandria.Models.Tracks.ITrack> trackRepository;
        private readonly IRepository<Gnosis.Alexandria.Models.Feeds.IFeed> feedRepository;

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
