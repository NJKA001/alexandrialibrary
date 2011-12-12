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
using Gnosis.Video.VideoLan;

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

                securityContext = new SecurityContext();

                mediaFactory = new MediaFactory();
                tagTypeFactory = new TagTypeFactory();

                mediaRepository = new SQLiteMediaRepository(logger);
                mediaRepository.Initialize();

                linkRepository = new SQLiteLinkRepository(logger);
                linkRepository.Initialize();

                tagRepository = new SQLiteTagRepository(logger, tagTypeFactory);
                tagRepository.Initialize();

                artistRepository = new SQLiteArtistRepository(logger);
                artistRepository.Initialize();

                albumRepository = new SQLiteAlbumRepository(logger);
                albumRepository.Initialize();

                trackRepository = new SQLiteTrackRepository(logger);
                trackRepository.Initialize();

                clipRepository = new SQLiteClipRepository(logger);
                clipRepository.Initialize();

                audioStreamFactory = new AudioStreamFactory();

                videoPlayer = new VideoPlayerControl();

                catalogController = new CatalogController(logger, securityContext, mediaFactory, mediaRepository, linkRepository, tagRepository, artistRepository, albumRepository, trackRepository, clipRepository, audioStreamFactory);
                spiderFactory = new SpiderFactory(logger, securityContext, mediaFactory, linkRepository, tagRepository, mediaRepository, artistRepository, albumRepository, trackRepository, clipRepository, audioStreamFactory);

                mediaItemController = new MediaItemController(logger, artistRepository, albumRepository, trackRepository, clipRepository);
                taskController = new TaskController(logger, videoPlayer, spiderFactory, mediaItemController, artistRepository, albumRepository, trackRepository, clipRepository);

                taskResultView.Initialize(logger, securityContext, taskController, mediaItemController, videoPlayer);
                taskManagerView.Initialize(logger, taskController, taskResultView);
                searchView.Initialize(logger, taskController, taskResultView);
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.ctor", ex);
            }
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IMediaFactory mediaFactory;
        private readonly ITagTypeFactory tagTypeFactory;

        private readonly IMediaRepository mediaRepository;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;

        private readonly IAudioStreamFactory audioStreamFactory;
        private readonly IVideoPlayer videoPlayer;

        private readonly SpiderFactory spiderFactory;
        private readonly ICatalogController catalogController;
        private readonly ITaskController taskController;
        private readonly IMediaItemController mediaItemController;
    }
}
