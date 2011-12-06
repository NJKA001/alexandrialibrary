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

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.ViewModels;
using Gnosis.Image;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchResultView.xaml
    /// </summary>
    public partial class SearchResultView : UserControl
    {
        public SearchResultView()
        {
            InitializeComponent();

            searchResultControl.DataContext = this;
        }

        private ILogger logger;
        private IMediaItemController mediaItemController;
        private ITaskController taskController;
        private TaskResultView taskResultView;
        private readonly ObservableCollection<ISearchResultViewModel> results = new ObservableCollection<ISearchResultViewModel>();

        private readonly IDictionary<string, ArtistSearchResultViewModel> artistResults = new Dictionary<string, ArtistSearchResultViewModel>();
        private readonly IDictionary<string, AlbumSearchResultViewModel> albumResults = new Dictionary<string, AlbumSearchResultViewModel>();

        private void albumListBoxItem_PreviewLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var element = e.OriginalSource as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var album = listBoxItem.DataContext as IAlbumViewModel;
                if (album == null)
                    return;

                album.IsSelected = true;
            }
            catch (Exception ex)
            {
                logger.Error("  albumListBoxItem_PreviewLeftMouseButtonDown", ex);
            }
        }

        private void albumListBoxItem_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var item = sender as ListBoxItem;
                if (item == null)
                    return;

                //var albumListBox = item.FindName("albumListBox");
                //if (albumListBox != null)
                //{
                //    var selected = albumListBox;
                //}

                var result = item.DataContext as ISearchResultViewModel;
                if (result == null)
                    return;
                
                var album = result.Albums.Where(x => x.IsSelected).FirstOrDefault();
                if (album == null)
                    return;

                //TODO: Get tracks for this album, create a new playlist and start playing it                
            }
            catch (Exception ex)
            {
                logger.Error("  albumListBoxItem_DoubleClick", ex);
            }
        }

        private void itemNamePanel_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var uiElement = sender as UIElement;
                if (uiElement == null)
                    return;

                var listBoxItem = uiElement.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var viewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (viewModel == null)
                    return;
                
                var image = e.Data.GetData(DataFormats.Bitmap) as System.Windows.Controls.Image;
                if (image != null)
                {
                    return;
                }

                var html = e.Data.GetData(DataFormats.Html) as string;
                if (!string.IsNullOrEmpty(html))
                {
                    var regex = new System.Text.RegularExpressions.Regex("src=['\"](?<PATH>[^\"']+)");
                    var match = regex.Match(html);
                    if (match != null)
                    {
                        var path = match.Groups["PATH"].Value;
                        if (!string.IsNullOrEmpty(path))
                        {
                            var request = System.Net.HttpWebRequest.Create(path);
                            var response = request.GetResponse();
                            if (response != null)
                            {
                                using (var stream = response.GetResponseStream())
                                {
                                    var thumbnail = new Uri(path, UriKind.Absolute);
                                    var thumbnailData = stream.ToBuffer();
                                    if (thumbnailData != null)
                                    {
                                        if (viewModel is ArtistSearchResultViewModel)
                                        {
                                            mediaItemController.SetArtistThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else if (viewModel is AlbumSearchResultViewModel)
                                        {
                                            mediaItemController.SetAlbumThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else if (viewModel is TrackSearchResultViewModel)
                                        {
                                            mediaItemController.SetTrackThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else
                                            logger.Warn("  Cannot set thumbnail - unknown search result type: " + viewModel.GetType().Name);
                                    }

                                    //var data = new ByteVector(buffer);
                                    //var picture = new TagLib.Picture(data);
                                    //tagController.AddPicture(track, picture);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("itemImageBorder_Drop", ex);
            }
        }

        private void AddResult(ISearchResultViewModel result)
        {
            Dispatcher.Invoke(new Action(() => AddViewModel(result)), DispatcherPriority.DataBind);
        }

        private void AddAlbum(ArtistSearchResultViewModel artist, AlbumViewModel album)
        {
            Action action = () => artist.AddAlbum(album);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        private void AddTrack(AlbumSearchResultViewModel album, TrackViewModel track)
        {
            Action action = () => album.AddTrack(track);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        public void HandleSearchResult(IMediaItem result)
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
                            AddAlbum(artistResults[artistKey], albumViewModel);
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
                        //var existing = albumResults[albumKey].Tracks.Where(x => x.Album.ToString() == trackViewModel.Album.ToString()).FirstOrDefault();
                        //if (existing == null)
                        //{
                            AddTrack(albumResults[albumKey], trackViewModel);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  HandleSearchResults", ex);
            }
        }

        private void CloseViewModel(ISearchResultViewModel viewModel)
        {
            try
            {
                var key = viewModel.MediaItem.ToString();
                if (viewModel is ArtistSearchResultViewModel && artistResults.ContainsKey(key))
                {
                    artistResults.Remove(key);
                }
                else if (viewModel is AlbumSearchResultViewModel && albumResults.ContainsKey(key))
                {
                    albumResults.Remove(key);
                }

                if (results.Contains(viewModel))
                    results.Remove(viewModel);
            }
            catch (Exception ex)
            {
                logger.Error("  CloseViewModel", ex);
            }
        }

        public IEnumerable<ISearchResultViewModel> Results
        {
            get { return results; }
        }

        public void Initialize(ILogger logger, IMediaItemController mediaItemController, ITaskController taskController, TaskResultView taskResultView)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (taskResultView == null)
                throw new ArgumentNullException("taskResultView");

            this.logger = logger;
            this.mediaItemController = mediaItemController;
            this.taskController = taskController;
            this.taskResultView = taskResultView;
        }

        public void AddViewModel(ISearchResultViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            viewModel.AddCloseCallback(x => CloseViewModel(x));
            results.Add(viewModel);
        }
    }
}
