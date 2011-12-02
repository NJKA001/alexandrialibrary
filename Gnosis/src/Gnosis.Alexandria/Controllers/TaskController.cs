using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.Controllers
{
    public class TaskController
        : ITaskController
    {
        public TaskController(ILogger logger, SpiderFactory spiderFactory, IMediaItemController mediaItemController, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
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

            this.logger = logger;
            this.spiderFactory = spiderFactory;
            this.mediaItemController = mediaItemController;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
        }

        private readonly ILogger logger;
        private readonly SpiderFactory spiderFactory;
        private readonly IMediaItemController mediaItemController;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly ObservableCollection<ITaskViewModel> taskViewModels = new ObservableCollection<ITaskViewModel>();

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

            var task = new CatalogMediaTask(logger, spiderFactory.CreateCatalogSpider(), target, TimeSpan.Zero, 0);
            return new CatalogMediaTaskViewModel(logger, task);
        }

        public SearchTaskViewModel GetSearchViewModel(string search)
        {
            if (search == null)
                throw new ArgumentNullException("search");

            var pattern = search + "%";
            var task = new MediaItemSearchTask(logger, pattern, artistRepository, albumRepository, trackRepository);
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
