using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;
using Gnosis.Audio;
using Gnosis.Audio.Fmod;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.Controllers
{
    public class TaskController
        : ITaskController
    {
        public TaskController(ILogger logger, IMediaTypeFactory mediaTypeFactory, IVideoPlayer videoPlayer, SpiderFactory spiderFactory, IMediaItemController mediaItemController, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IClip> clipRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");
            if (spiderFactory == null)
                throw new ArgumentNullException("spiderFactory");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");
            if (clipRepository == null)
                throw new ArgumentNullException("clipRepository");

            this.logger = logger;
            this.mediaTypeFactory = mediaTypeFactory;
            this.videoPlayer = videoPlayer;
            this.spiderFactory = spiderFactory;
            this.mediaItemController = mediaItemController;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.clipRepository = clipRepository;
            this.audioStreamFactory = new AudioStreamFactory();
        }

        private readonly ILogger logger;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly IVideoPlayer videoPlayer;
        private readonly SpiderFactory spiderFactory;
        private readonly IMediaItemController mediaItemController;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;
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

            var task = new CatalogMediaTask(logger, mediaTypeFactory, spiderFactory.CreateCatalogSpider(), target, TimeSpan.Zero, 0);

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
            var task = new SearchTask(logger, pattern, artistRepository, albumRepository, trackRepository, clipRepository);
            return new SearchTaskViewModel(logger, task, search);
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
