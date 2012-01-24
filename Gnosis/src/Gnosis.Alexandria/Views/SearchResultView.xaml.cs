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
using Gnosis.Application.Vendor;
using Gnosis.Image;
using Gnosis.Links;
using Gnosis.Metadata;
using Gnosis.Tags;

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
        private ISecurityContext securityContext;
        private IMediaTypeFactory mediaTypeFactory;
        private IMediaItemController mediaItemController;
        private ITaskController taskController;
        private ITagController tagController;
        private TaskResultView taskResultView;
        private readonly ObservableCollection<ISearchResultViewModel> results = new ObservableCollection<ISearchResultViewModel>();

        private readonly IDictionary<string, ISearchResultViewModel> artistResults = new Dictionary<string, ISearchResultViewModel>();
        private readonly IDictionary<string, ISearchResultViewModel> albumResults = new Dictionary<string, ISearchResultViewModel>();
        private readonly IDictionary<string, ISearchResultViewModel> trackResults = new Dictionary<string, ISearchResultViewModel>();
        private readonly IDictionary<string, ISearchResultViewModel> clipResults = new Dictionary<string, ISearchResultViewModel>();

        private void addTagButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var resultViewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (resultViewModel == null)
                    return;

                if (string.IsNullOrEmpty(resultViewModel.CurrentTagValue))
                {
                    MessageBox.Show("Tag value is required.", "Cannot Add Tag");
                    return;
                }

                var tagValue = resultViewModel.CurrentTagValue;
                var tag = new Tag(resultViewModel.Id, TagType.DefaultString, tagValue);
                mediaItemController.SaveTags(new List<ITag> { tag });
                resultViewModel.AddTag(new TagViewModel(tag));
                resultViewModel.CurrentTagValue = null;
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.addTagButton_Click", ex);
            }
        }

        private void addLinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var resultViewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (resultViewModel == null)
                    return;

                if (string.IsNullOrEmpty(resultViewModel.CurrentLinkName))
                {
                    MessageBox.Show("Link name is required.", "Cannot Save Link");
                    return;
                }

                if (string.IsNullOrEmpty(resultViewModel.CurrentLinkTarget))
                {
                    MessageBox.Show("Link URL is required.", "Cannot Save Link");
                    return;
                }

                Uri linkTarget = null;
                if (!Uri.TryCreate(resultViewModel.CurrentLinkTarget, UriKind.Absolute, out linkTarget))
                {
                    MessageBox.Show("Link URL is not valid.", "Cannot Save Link");
                    return;
                }

                var name = resultViewModel.CurrentLinkName;
                var relationship = resultViewModel.CurrentLinkRelationship ?? string.Empty;
                var link = new Link(resultViewModel.Id, linkTarget, relationship, name);
                mediaItemController.SaveLinks(new List<ILink> { link });
                resultViewModel.AddLink(new LinkViewModel(link));
                resultViewModel.CurrentLinkName = null;
                resultViewModel.CurrentLinkRelationship = null;
                resultViewModel.CurrentLinkTarget = null;
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.addLinkButton_Click", ex);
            }
        }

        private void linksTab_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var resultViewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (resultViewModel == null)
                    return;

                mediaItemController.SaveLinks(resultViewModel.GetSystemLinks());
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.linksTab_GotFocus", ex);
            }
        }

        private void tagsTab_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var resultViewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (resultViewModel == null)
                    return;

                mediaItemController.SaveTags(resultViewModel.GetSystemTags());
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.tagsTab_GotFocus", ex);
            }
        }

        private void summaryPasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem == null)
                    return;

                var menu = menuItem.Parent as ContextMenu;
                if (menu == null)
                    return;

                var listBoxItem = menu.PlacementTarget.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var resultViewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (resultViewModel == null)
                    return;

                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    var text = Clipboard.GetText(TextDataFormat.Text);
                    if (text != null)
                    {
                        resultViewModel.UpdateSummary(mediaItemController, text);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.summaryPasteMenuItem_Click", ex);
            }
        }

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
                logger.Error("  SearchResultView.albumListBoxItem_PreviewLeftMouseButtonDown", ex);
            }
        }

        private void searchResultItem_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var element = e.OriginalSource as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var viewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (viewModel == null)
                    return;

                var playlist = viewModel.ToPlaylist(securityContext, mediaTypeFactory);
                if (playlist == null)
                    return;

                var taskViewModel = taskController.GetPlaylistViewModel(playlist);
                taskResultView.Playlist(taskViewModel, playlist);
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.searchResultItem_DoubleClick", ex);
            }
        }

        private void clipListBoxItem_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var element = e.OriginalSource as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var clipViewModel = listBoxItem.DataContext as IClipViewModel;
                if (clipViewModel == null)
                    return;

                var summary = string.Empty;
                var thumbnail = Guid.Empty.ToUrn();
                var thumbnailData = new byte[0];

                var album = mediaItemController.GetAlbum(clipViewModel.Id);
                if (album != null)
                {
                    summary = "Based on: " + album.Name;
                    thumbnail = album.Thumbnail;
                    thumbnailData = album.ThumbnailData;
                }

                var date = DateTime.Now.ToUniversalTime();

                var builder = new MediaItemBuilder<IPlaylist>(securityContext, mediaTypeFactory)
                    .Identity(clipViewModel.Name, summary, date, date, 1)
                    .Size(clipViewModel.Duration)
                    .Thumbnail(thumbnail, thumbnailData);

                var playlist = builder.ToMediaItem();
                var playlistViewModel = new PlaylistViewModel(mediaItemController, playlist, new List<IPlaylistItemViewModel> { clipViewModel.ToPlaylistItem(securityContext, mediaTypeFactory, 1) });

                var taskViewModel = taskController.GetPlaylistViewModel(playlistViewModel);
                taskResultView.Playlist(taskViewModel, playlistViewModel);
            }
            catch (Exception ex)
            {
                logger.Error("  SearchResultView.clipListBoxItem_DoubleClick", ex);
            }
        }

        private void albumListBoxItem_DoubleClick(object sender, MouseEventArgs e)
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

                if (album.Tracks.Count() == 0)
                {
                    var tracks = mediaItemController.GetTracks(album.Id);
                    foreach (var track in tracks)
                    {
                        album.AddTrack(new TrackViewModel(mediaItemController, track));
                    }
                }

                var playlist = album.ToPlaylist(securityContext, mediaTypeFactory);

                var taskViewModel = taskController.GetPlaylistViewModel(playlist);
                taskResultView.Playlist(taskViewModel, playlist);
            }
            catch (Exception ex)
            {
                logger.Error("  albumListBoxItem_DoubleClick", ex);
            }
        }

        private void itemImage_Drop(object sender, DragEventArgs e)
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
                                        viewModel.UpdateThumbnail(mediaItemController, thumbnail, thumbnailData);
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

        private void AddAlbum(ISearchResultViewModel artist, AlbumViewModel album)
        {
            Action action = () => artist.AddAlbum(album);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        private void AddTrack(ISearchResultViewModel album, TrackViewModel track)
        {
            var existing = album.Tracks.Where(x => x.Id == track.Id).FirstOrDefault();
            if (existing != null)
                return;

            Action action = () => album.AddTrack(track);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        private void AddClip(ISearchResultViewModel album, ClipViewModel clip)
        {
            var existing = album.Clips.Where(x => x.Id == clip.Id).FirstOrDefault();
            if (existing != null)
                return;

            Action action = () => album.AddClip(clip);
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
                        var artistViewModel = new ArtistViewModel(mediaItemController, result as IArtist); //result.Location, result.Name, result.Summary, result.FromDate, result.ToDate, result.Thumbnail, result.ThumbnailData);
                        var resultViewModel = new SearchResultViewModel(artistViewModel);
                        artistResults.Add(artistKey, resultViewModel);
                        AddResult(resultViewModel);
                    }
                }
                else if (result is IAlbum)
                {
                    var albumViewModel = new AlbumViewModel(mediaItemController, result as IAlbum); //result.Location, result.Name, result.Summary, result.Creator, result.CreatorName, result.FromDate, result.Thumbnail, result.ThumbnailData);

                    var artistKey = result.Creator.ToString();
                    var albumKey = result.Location.ToString();
                    if (!artistResults.ContainsKey(artistKey))
                    {
                        if (!albumResults.ContainsKey(albumKey))
                        {
                            var resultViewModel = new SearchResultViewModel(albumViewModel);
                            AddResult(resultViewModel);
                            albumResults.Add(albumKey, resultViewModel);
                        }
                    }
                    else
                    {
                        if (!albumResults.ContainsKey(albumKey))
                        {
                            var existing = artistResults[artistKey].Albums.Where(x => x.Id.ToString() == albumViewModel.Id.ToString()).FirstOrDefault();
                            if (existing == null)
                            {
                                AddAlbum(artistResults[artistKey], albumViewModel);
                            }
                        }
                    }
                }
                else if (result is ITrack)
                {
                    var trackViewModel = new TrackViewModel(mediaItemController, result as ITrack); //result.Location, result.Name, result.Summary, result.Number, result.Duration, result.FromDate, result.Creator, result.CreatorName, result.Catalog, result.CatalogName, result.Target, result.TargetType, result.Thumbnail, result.ThumbnailData);

                    var albumKey = result.Catalog.ToString();
                    var trackKey = result.Location.ToString();
                    if (!albumResults.ContainsKey(albumKey))
                    {
                        if (!trackResults.ContainsKey(trackKey))
                        {
                            var resultViewModel = new SearchResultViewModel(trackViewModel);
                            trackResults.Add(trackKey, resultViewModel);
                            AddResult(resultViewModel);
                        }
                    }
                    else
                    {
                        if (!trackResults.ContainsKey(trackKey))
                        {
                            //var existing = albumResults[albumKey].Tracks.Where(x => x.Album.ToString() == trackViewModel.Album.ToString()).FirstOrDefault();
                            //if (existing == null)
                            //{
                            AddTrack(albumResults[albumKey], trackViewModel);
                            //}
                        }
                    }
                }
                else if (result is IClip)
                {
                    var clipViewModel = new ClipViewModel(mediaItemController, result as IClip); //result.Location, result.Name, result.Summary, result.Number, result.Duration, result.Height, result.Width, result.FromDate, result.Creator, result.CreatorName, result.Catalog, result.CatalogName, result.Target, result.TargetType, result.Thumbnail, result.ThumbnailData);

                    var albumKey = result.Catalog.ToString();
                    var clipKey = result.Location.ToString();
                    if (!albumResults.ContainsKey(albumKey))
                    {
                        if (!clipResults.ContainsKey(clipKey))
                        {
                            var resultViewModel = new SearchResultViewModel(clipViewModel);
                            clipResults.Add(clipKey, resultViewModel);
                            AddResult(resultViewModel);
                        }
                    }
                    else
                    {
                        if (!clipResults.ContainsKey(clipKey))
                        {
                            //var existing = albumResults[albumKey].Tracks.Where(x => x.Album.ToString() == trackViewModel.Album.ToString()).FirstOrDefault();
                            //if (existing == null)
                            //{
                            AddClip(albumResults[albumKey], clipViewModel);
                            //}
                        }
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
                var key = viewModel.Id.ToString();
                if (artistResults.ContainsKey(key))
                {
                    artistResults.Remove(key);
                }
                else if (albumResults.ContainsKey(key))
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

        public void Initialize(ILogger logger, ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemController mediaItemController, ITaskController taskController, ITagController tagController, TaskResultView taskResultView)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (tagController == null)
                throw new ArgumentNullException("tagController");
            if (taskResultView == null)
                throw new ArgumentNullException("taskResultView");

            this.logger = logger;
            this.securityContext = securityContext;
            this.mediaTypeFactory = mediaTypeFactory;
            this.mediaItemController = mediaItemController;
            this.taskController = taskController;
            this.tagController = tagController;
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
