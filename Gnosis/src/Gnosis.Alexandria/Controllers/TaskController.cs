using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;
using Gnosis.Alexandria.Views;
using Gnosis.Audio;
using Gnosis.Audio.Fmod;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.Controllers
{
    public class TaskController
        : ITaskController
    {
        public TaskController(ILogger logger, IMediaFactory mediaFactory, IVideoPlayer videoPlayer, SpiderFactory spiderFactory, IMetadataController metadataController, IMarqueeRepository marqueeRepository, IMetadataRepository mediaItemRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");
            if (spiderFactory == null)
                throw new ArgumentNullException("spiderFactory");
            if (metadataController == null)
                throw new ArgumentNullException("metadataController");
            if (marqueeRepository == null)
                throw new ArgumentNullException("marqueeRepository");
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");

            this.logger = logger;
            this.mediaFactory = mediaFactory;
            this.videoPlayer = videoPlayer;
            this.spiderFactory = spiderFactory;
            this.metadataController = metadataController;
            this.marqueeRepository = marqueeRepository;
            this.mediaItemRepository = mediaItemRepository;
            this.audioStreamFactory = new AudioStreamFactory();
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly IVideoPlayer videoPlayer;
        private readonly SpiderFactory spiderFactory;
        private readonly IMetadataController metadataController;
        private readonly IMarqueeRepository marqueeRepository;
        private readonly IMetadataRepository mediaItemRepository;
        private readonly ObservableCollection<ITaskViewModel> taskViewModels = new ObservableCollection<ITaskViewModel>();
        private readonly IAudioStreamFactory audioStreamFactory;

        public IEnumerable<ITaskViewModel> Tasks
        {
            get { return taskViewModels; }
        }

        public CatalogMediaTaskViewModel GetCatalogViewModel(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var target = new Uri(path);
            if (target.IsFile && !System.IO.Directory.Exists(path))
                throw new ArgumentException("path does not exist");

            var task = new CatalogMediaTask(logger, mediaFactory, spiderFactory.CreateCatalogSpider(), target, TimeSpan.Zero, 0);

            return new CatalogMediaTaskViewModel(logger, task);
        }

        public PlaylistTaskViewModel GetPlaylistViewModel(IPlaylistViewModel playlist)
        {
            if (playlist == null)
                throw new ArgumentNullException("playlist");

            try
            {
                var audioPlayer = new AudioPlayer(audioStreamFactory);
                var first = playlist.GetCurrentTaskItem();
                var task = new PlaylistTask(logger, audioPlayer, videoPlayer, first, TimeSpan.Zero, () => playlist.GetPreviousTaskItem(), () => playlist.GetNextTaskItem());

                var icon = playlist.Icon;

                var iconPath = first.Image as Uri;
                if (iconPath != null && !iconPath.IsEmptyUrn())
                    icon = iconPath;

                var iconData = first.Image as byte[];
                if (iconData != null && iconData.Length > 0)
                    icon = iconData;

                return new PlaylistTaskViewModel(logger, task, playlist.Name, icon);
            }
            catch (Exception ex)
            {
                logger.Error("  TaskController.GetPlaylistViewModel", ex);
                throw;
            }
        }

        public SearchTaskViewModel GetSearchViewModel(string search)
        {
            if (search == null)
                throw new ArgumentNullException("search");

            var pattern = search + "%";
            var task = new SearchTask(logger, pattern, mediaItemRepository);
            return new SearchTaskViewModel(logger, task, search);
        }

        public MarqueeView GetMarqueeView(MetadataCategory category)
        {
            var controller = new MarqueeController(logger, marqueeRepository, category);

            var view = new MarqueeView();
            view.Initialize(logger, controller);
            return view;
        }

        public void AddTaskViewModel(ITaskViewModel taskViewModel)
        {
            taskViewModel.AddCancelCallback(x => RemoveTaskViewModel(x));
            taskViewModels.Add(taskViewModel);
        }

        public void RemoveTaskViewModel(ITaskViewModel taskViewModel)
        {
            if (taskViewModels.Contains(taskViewModel))
                taskViewModels.Remove(taskViewModel);
        }
    }
}
