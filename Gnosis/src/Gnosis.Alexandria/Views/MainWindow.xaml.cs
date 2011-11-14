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
                mediaFactory = new MediaFactory();
                linkTypeFactory = new LinkTypeFactory();
                tagTypeFactory = new TagTypeFactory();

                mediaRepository = new SQLiteMediaRepository(logger);
                mediaRepository.Initialize();

                linkRepository = new SQLiteLinkRepository(logger, linkTypeFactory);
                linkRepository.Initialize();

                tagRepository = new SQLiteTagRepository(logger, tagTypeFactory);
                tagRepository.Initialize();

                artistRepository = new SQLiteArtistRepository(logger);
                artistRepository.Initialize();

                albumRepository = new SQLiteAlbumRepository(logger);
                albumRepository.Initialize();

                trackRepository = new SQLiteTrackRepository(logger);
                trackRepository.Initialize();

                mediaDetailRepository = new SQLiteMediaDetailRepository(logger, tagRepository, linkRepository);

                catalogController = new CatalogController(logger, mediaFactory, mediaRepository, linkRepository, tagRepository, artistRepository, albumRepository, trackRepository);
                mediaController = new MediaController(logger, mediaDetailRepository);
                spiderFactory = new SpiderFactory(logger, mediaFactory, linkRepository, tagRepository, mediaRepository, artistRepository, albumRepository, trackRepository);

                taskController = new TaskController(logger);

                taskManagerView.Initialize(logger, spiderFactory, taskController);
                searchView.Initialize(logger, mediaDetailRepository, tagRepository, taskController);
            }
            catch (Exception ex)
            {
                logger.Error("MainWindow.ctor", ex);
            }
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkTypeFactory linkTypeFactory;
        private readonly ITagTypeFactory tagTypeFactory;

        private readonly IMediaRepository mediaRepository;
        private readonly IMediaDetailRepository mediaDetailRepository;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IArtistRepository artistRepository;
        private readonly IAlbumRepository albumRepository;
        private readonly ITrackRepository trackRepository;

        private readonly SpiderFactory spiderFactory;
        private readonly ICatalogController catalogController;
        private readonly IMediaController mediaController;
        private readonly ITaskController taskController;
    }
}
