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
using System.Windows.Threading;

//using Gnosis.Tags;
//using Gnosis.Tags.Id3.Id3v2;
using Gnosis.Tasks;
using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private ITagRepository tagRepository;
        private IMediaItemRepository<IArtist> artistRepository;
        private IMediaItemRepository<IAlbum> albumRepository;
        private IMediaItemRepository<ITrack> trackRepository;
        private ITaskController taskController;
        private IMediaItemController mediaItemController;
        private readonly IDictionary<string, ArtistSearchResultViewModel> artistResults = new Dictionary<string, ArtistSearchResultViewModel>();
        private readonly IDictionary<string, AlbumSearchResultViewModel> albumResults = new Dictionary<string, AlbumSearchResultViewModel>();

        private void AddResult(ISearchResultViewModel result)
        {
            Dispatcher.Invoke(new Action(() => searchResultView.AddViewModel(result)), DispatcherPriority.DataBind);
        }

        private void HandleSearchResult(IMediaItem result)
        {
            try
            {
                if (result == null)
                    return;

                if (result is IArtist)
                {
                    var artistKey = result.Location.ToString();
                    if (!artistResults.ContainsKey(artistKey))
                    {
                        var artistViewModel = new ArtistViewModel(result.Location, result.Name, result.FromDate, result.ToDate, result.Thumbnail, result.ThumbnailData, string.Empty);
                        var resultViewModel = new ArtistSearchResultViewModel(artistViewModel);
                        artistResults.Add(artistKey, resultViewModel);
                        AddResult(resultViewModel);
                    }
                }
                else if (result is IAlbum)
                {
                    var albumViewModel = new AlbumViewModel(result.Location, result.Name, result.Creator, result.CreatorName, result.FromDate, result.Thumbnail, result.ThumbnailData, string.Empty);

                    var artistKey = result.Creator.ToString();
                    var albumKey = result.Location.ToString();
                    if (!artistResults.ContainsKey(artistKey))
                    {
                        var resultViewModel = new AlbumSearchResultViewModel(albumViewModel);
                        if (!albumResults.ContainsKey(albumKey))
                        {
                            albumResults.Add(albumKey, resultViewModel);
                        }

                        AddResult(resultViewModel);
                    }
                    else
                    {
                        var existing = artistResults[artistKey].Albums.Where(x => x.Album.ToString() == albumViewModel.Album.ToString()).FirstOrDefault();
                        if (existing == null)
                        {
                            artistResults[artistKey].AddAlbum(albumViewModel);
                        }
                    }
                }
                else if (result is ITrack)
                {
                    var trackViewModel = new TrackViewModel(result.Location, result.Name, result.Number, result.Duration, result.FromDate, result.Creator, result.CreatorName, result.Catalog, result.CatalogName, result.Thumbnail, result.ThumbnailData, string.Empty);

                    var albumKey = result.Catalog.ToString();
                    if (!albumResults.ContainsKey(albumKey))
                    {
                        var resultViewModel = new TrackSearchResultViewModel(trackViewModel);

                        AddResult(resultViewModel);
                    }
                    else
                    {
                        var existing = albumResults[albumKey].Tracks.Where(x => x.Album.ToString() == trackViewModel.Album.ToString()).FirstOrDefault();
                        if (existing == null)
                        {
                            albumResults[albumKey].AddTrack(trackViewModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  HandleSearchResults", ex);
            }
        }

        private void DoSearch()
        {
            try
            {
                var search = searchTextBox.Text;
                if (string.IsNullOrEmpty(search))
                    return;

                var pattern = search + "%";
                var task = new MediaItemSearchTask(logger, pattern, artistRepository, albumRepository, trackRepository);
                task.AddResultsCallback(result => HandleSearchResult(result));
                
                var taskViewModel = new SearchTaskViewModel(logger, task, search);
                taskController.AddTask(taskViewModel);

                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  DoSearch", ex);
            }
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return || e.Key == Key.Enter)
                {
                    e.Handled = true;
                    DoSearch();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  searchTextBox_KeyUp", ex);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DoSearch();
        }

        public void Initialize(ILogger logger, ITagRepository tagRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, ITaskController taskController, IMediaItemController mediaItemController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository:");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");

            this.logger = logger;
            this.tagRepository = tagRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.taskController = taskController;
            this.mediaItemController = mediaItemController;

            try
            {
                searchResultView.Initialize(logger, mediaItemController);
            }
            catch (Exception ex)
            {
                logger.Error("  SearchView.Initialize", ex);
            }
        }
    }
}
