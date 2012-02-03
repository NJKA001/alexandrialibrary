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

                characterSetFactory = new CharacterSetFactory();
                contentTypeFactory = new ContentTypeFactory(logger, characterSetFactory);
                securityContext = new SecurityContext(contentTypeFactory);
                tagTypeFactory = new TagTypeFactory();

                mediaRepository = new SQLiteMediaRepository(logger, contentTypeFactory);
                mediaRepository.Initialize();

                linkRepository = new SQLiteLinkRepository(logger);
                linkRepository.Initialize();

                tagRepository = new SQLiteTagRepository(logger, tagTypeFactory);
                tagRepository.Initialize();

                mediaItemRepository = new SQLiteMediaItemRepository(logger, securityContext, contentTypeFactory);
                mediaItemRepository.Initialize();

                audioStreamFactory = new AudioStreamFactory();

                videoPlayer = new Gnosis.Video.Vlc.VideoPlayerControl();
                videoPlayer.Initialize(logger, () => GetVideoHost());

                catalogController = new CatalogController(logger, securityContext, contentTypeFactory, mediaRepository, linkRepository, tagRepository, mediaItemRepository, audioStreamFactory);
                spiderFactory = new SpiderFactory(logger, securityContext, contentTypeFactory, linkRepository, tagRepository, mediaRepository, mediaItemRepository, audioStreamFactory);

                mediaItemController = new MediaItemController(logger, securityContext, contentTypeFactory, linkRepository, tagRepository, mediaItemRepository);
                taskController = new TaskController(logger, contentTypeFactory, videoPlayer, spiderFactory, mediaItemController, mediaItemRepository);
                tagController = new TagController(logger, tagRepository);
                commandController = new CommandController(logger);

                taskResultView.Initialize(logger, securityContext, contentTypeFactory, mediaItemController, taskController, tagController, videoPlayer);
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

        private readonly ICharacterSetFactory characterSetFactory;
        private readonly IContentTypeFactory contentTypeFactory;
        private readonly ISecurityContext securityContext;
        private readonly ITagTypeFactory tagTypeFactory;

        private readonly IMediaRepository mediaRepository;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaItemRepository mediaItemRepository;

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
