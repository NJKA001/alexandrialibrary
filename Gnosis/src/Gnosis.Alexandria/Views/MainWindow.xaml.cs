using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.ViewModels;

using Gnosis.Audio;
using Gnosis.Audio.Fmod;
using Gnosis.Data.SQLite;
using Gnosis.Links;
using Gnosis.Tags;

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

            try
            {
                this.logger = Gnosis.Utilities.Log4NetLogger.GetDefaultLogger(typeof(MainWindow));
            }
            catch (Exception loggerEx)
            {
                throw new ApplicationException("Could not initialize logger", loggerEx);
            }

            try
            {
                logger.Info("Initializing Alexandria");

                mediaTypeFactory = new MediaTypeFactory(logger);
                contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory);
                securityContext = new SecurityContext(mediaTypeFactory);
                tagTypeFactory = new TagTypeFactory();

                mediaRepository = new SQLiteMediaRepository(logger, mediaTypeFactory);
                mediaRepository.Initialize();

                linkRepository = new SQLiteLinkRepository(logger);
                linkRepository.Initialize();

                tagRepository = new SQLiteTagRepository(logger, tagTypeFactory);
                tagRepository.Initialize();

                albumRepository = new SQLiteAlbumRepository(logger, mediaTypeFactory);
                albumRepository.Initialize();

                artistRepository = new SQLiteArtistRepository(logger, mediaTypeFactory);
                artistRepository.Initialize();

                clipRepository = new SQLiteClipRepository(logger, mediaTypeFactory);
                clipRepository.Initialize();

                docRepository = new SQLiteDocRepository(logger, mediaTypeFactory);
                docRepository.Initialize();

                feedRepository = new SQLiteFeedRepository(logger, mediaTypeFactory);
                feedRepository.Initialize();

                feedItemRepository = new SQLiteFeedItemRepository(logger, mediaTypeFactory);
                feedItemRepository.Initialize();

                picRepository = new SQLitePicRepository(logger, mediaTypeFactory);
                picRepository.Initialize();

                playlistRepository = new SQLitePlaylistRepository(logger, mediaTypeFactory);
                playlistRepository.Initialize();

                playlistItemRepository = new SQLitePlaylistItemRepository(logger, mediaTypeFactory);
                playlistItemRepository.Initialize();

                programRepository = new SQLiteProgramRepository(logger, mediaTypeFactory);
                programRepository.Initialize();

                trackRepository = new SQLiteTrackRepository(logger, mediaTypeFactory);
                trackRepository.Initialize();

                audioStreamFactory = new AudioStreamFactory();

                videoPlayer = new Gnosis.Video.Vlc.VideoPlayerControl();
                videoPlayer.Initialize(logger, () => GetVideoHost());

                catalogController = new CatalogController(logger, securityContext, contentTypeFactory, mediaTypeFactory, mediaRepository, linkRepository, tagRepository, artistRepository, albumRepository, trackRepository, clipRepository, audioStreamFactory);
                spiderFactory = new SpiderFactory(logger, securityContext, contentTypeFactory, mediaTypeFactory, linkRepository, tagRepository, mediaRepository, artistRepository, albumRepository, trackRepository, clipRepository, audioStreamFactory);

                mediaItemController = new MediaItemController(logger, linkRepository, tagRepository, albumRepository, artistRepository, clipRepository, docRepository, feedRepository, feedItemRepository, picRepository, playlistRepository, playlistItemRepository, programRepository, trackRepository);
                taskController = new TaskController(logger, mediaTypeFactory, videoPlayer, spiderFactory, mediaItemController, artistRepository, albumRepository, trackRepository, clipRepository);
                tagController = new TagController(logger, tagRepository);
                commandController = new CommandController(logger);

                taskResultView.Initialize(logger, securityContext, mediaTypeFactory, mediaItemController, taskController, tagController, videoPlayer);
                taskManagerView.Initialize(logger, taskController, taskResultView);
                searchView.Initialize(logger, taskController, taskResultView);
                commandView.Initialize(logger, commandController, taskController, taskResultView);

                ScreenSaver.Disable();
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.ctor", ex);
            }
        }

        private readonly ILogger logger;

        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly IContentTypeFactory contentTypeFactory;
        private readonly ISecurityContext securityContext;
        private readonly ITagTypeFactory tagTypeFactory;

        private readonly IMediaRepository mediaRepository;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;
        private readonly IMediaItemRepository<IDoc> docRepository;
        private readonly IMediaItemRepository<IFeed> feedRepository;
        private readonly IMediaItemRepository<IFeedItem> feedItemRepository;
        private readonly IMediaItemRepository<IPic> picRepository;
        private readonly IMediaItemRepository<IPlaylist> playlistRepository;
        private readonly IMediaItemRepository<IPlaylistItem> playlistItemRepository;
        private readonly IMediaItemRepository<IProgram> programRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        private readonly IAudioStreamFactory audioStreamFactory;
        private readonly IVideoPlayer videoPlayer;

        private readonly SpiderFactory spiderFactory;
        private readonly ICatalogController catalogController;
        private readonly IMediaItemController mediaItemController;
        private readonly ITaskController taskController;
        private readonly ITagController tagController;
        private readonly ICommandController commandController;

        private IVideoHost GetVideoHost()
        {
            Func<IVideoHost> func = () => new VideoPlayerWindow();
            return Dispatcher.Invoke(func) as IVideoHost;
        }
    }
}
