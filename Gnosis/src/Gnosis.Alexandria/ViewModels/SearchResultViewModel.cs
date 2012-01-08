using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public class SearchResultViewModel
        : ISearchResultViewModel
    {
        public SearchResultViewModel(IMediaItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.item = item;

            artistsVisibility = item is IAlbumContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            albumsVisibility = item is IAlbumContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            tracksVisibility = item is ITrackContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            clipsVisibility = item is IClipContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            playlistsVisibility = item is IPlaylistContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            playlistItemsVisibility = item is IPlaylistItemContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            feedsVisibility = item is IFeedContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
            feedItemsVisibility = item is IFeedItemContainerViewModel ? Visibility.Visible : Visibility.Collapsed;
        }

        protected readonly IMediaItemViewModel item;

        private readonly Visibility artistsVisibility;
        private readonly Visibility albumsVisibility;
        private readonly Visibility tracksVisibility;
        private readonly Visibility clipsVisibility;
        private readonly Visibility playlistsVisibility;
        private readonly Visibility playlistItemsVisibility;
        private readonly Visibility feedsVisibility;
        private readonly Visibility feedItemsVisibility;

        private string summaryOverride;
        private object imageOverride;

        private bool isClosed;
        private bool isSelected;

        private string currentLinkName;
        private string currentLinkRelationship;
        private string currentLinkTarget;
        private string currentTagValue;

        private readonly IList<Action<ISearchResultViewModel>> closeCallbacks = new List<Action<ISearchResultViewModel>>();

        private void OnClosed()
        {
            foreach (var callback in closeCallbacks)
                callback(this);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public IMediaItemViewModel Item
        {
            get { return item; }
        }

        public Uri Id
        {
            get { return item.Id; }
        }

        public string Name
        {
            get { return item.Name; }
        }

        public string Years
        {
            get { return item.Years; }
        }

        public string Type
        {
            get { return item.Type; }
        }

        public Visibility CreatorVisibility
        {
            get { return item.Creator.IsEmptyUrn() ? Visibility.Collapsed : Visibility.Visible; }
        }

        public string CreatorName
        {
            get { return item.CreatorName; }
        }

        public Visibility CatalogVisibility
        {
            get { return item.Catalog.IsEmptyUrn() ? Visibility.Collapsed : Visibility.Visible; }
        }

        public string CatalogName
        {
            get { return item.CatalogName; }
        }

        public object Icon
        {
            get { return item.Icon; }
        }

        public object Image
        {
            get { return imageOverride != null ? imageOverride : item.Image; }
            private set
            {
                imageOverride = value;
                OnPropertyChanged("Image");
            }
        }

        public string Summary
        {
            get { return summaryOverride != null ? summaryOverride : item.Summary; }
            private set
            {
                summaryOverride = value;
                OnPropertyChanged("Summary");
            }
        }

        public Visibility TracksVisibility
        {
            get { return tracksVisibility; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get
            {
                var trackContainer = item as ITrackContainerViewModel;
                return trackContainer != null ? trackContainer.Tracks : Enumerable.Empty<ITrackViewModel>();
            }
        }

        public Visibility ClipsVisibility
        {
            get { return clipsVisibility; }
        }

        public IEnumerable<IClipViewModel> Clips
        {
            get
            {
                var clipContainer = item as IClipContainerViewModel;
                return clipContainer != null ? clipContainer.Clips : Enumerable.Empty<IClipViewModel>();
            }
        }

        public Visibility AlbumsVisibility
        {
            get { return albumsVisibility; }
        }

        public IEnumerable<IAlbumViewModel> Albums
        {
            get
            {
                var albumContainer = item as IAlbumContainerViewModel;
                return albumContainer != null ? albumContainer.Albums : Enumerable.Empty<IAlbumViewModel>();
            }
        }

        public IEnumerable<ILinkViewModel> Links
        {
            get { return item.Links; }
        }

        public void AddLink(ILinkViewModel link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            item.AddLink(link);
        }

        public void RemoveLink(ILinkViewModel link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            item.RemoveLink(link);
        }

        public IEnumerable<ILink> GetSystemLinks()
        {
            return item.GetSystemLinks();
        }

        public IEnumerable<ITagViewModel> Tags
        {
            get { return item.Tags; }
        }

        public void AddTag(ITagViewModel tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            item.AddTag(tag);
        }

        public void RemoveTag(ITagViewModel tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            item.RemoveTag(tag);
        }

        public IEnumerable<ITag> GetSystemTags()
        {
            return item.GetSystemTags();
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                if (isClosed != value)
                {
                    isClosed = value;
                    OnPropertyChanged("IsClosed");

                    if (isClosed)
                        OnClosed();
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string CurrentLinkName
        {
            get { return currentLinkName; }
            set
            {
                currentLinkName = value;
                OnPropertyChanged("CurrentLinkName");
            }
        }

        public string CurrentLinkRelationship
        {
            get { return currentLinkRelationship; }
            set
            {
                currentLinkRelationship = value;
                OnPropertyChanged("CurrentLinkRelationship");
            }
        }

        public string CurrentLinkTarget
        {
            get { return currentLinkTarget; }
            set
            {
                currentLinkTarget = value;
                OnPropertyChanged("CurrentLinkTarget");
            }
        }

        public string CurrentTagValue
        {
            get { return currentTagValue; }
            set
            {
                currentTagValue = value;
                OnPropertyChanged("CurrentTagValue");
            }
        }

        public void AddCloseCallback(Action<ISearchResultViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            closeCallbacks.Add(callback);
        }

        public void AddAlbum(IAlbumViewModel album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            var albumContainer = item as IAlbumContainerViewModel;
            if (albumContainer != null)
                albumContainer.AddAlbum(album);
        }

        public void AddTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            var trackContainer = item as ITrackContainerViewModel;
            if (trackContainer != null)
                trackContainer.AddTrack(track);
        }

        public void AddClip(IClipViewModel clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            var clipContainer = item as IClipContainerViewModel;
            if (clipContainer != null)
                clipContainer.AddClip(clip);
        }

        public void UpdateThumbnail(IMediaItemController controller, Uri thumbnail, byte[] thumbnailData)
        {
            if (thumbnailData != null && thumbnailData.Length > 0)
            {
                Image = thumbnailData;
            }
            else if (thumbnail != null && !thumbnail.IsEmptyUrn())
            {
                Image = thumbnail;
            }

            if (item is IArtistViewModel)
            {
                controller.UpdateThumbnail<IArtist>(item.Id, thumbnail, thumbnailData);
            }
            else if (item is IAlbumViewModel)
            {
                controller.UpdateThumbnail<IAlbum>(item.Id, thumbnail, thumbnailData);
            }
            else if (item is ITrackViewModel)
            {
                controller.UpdateThumbnail<ITrack>(item.Id, thumbnail, thumbnailData);
            }
            else if (item is IClipViewModel)
            {
                controller.UpdateThumbnail<IClip>(item.Id, thumbnail, thumbnailData);
            }
        }

        public void UpdateSummary(IMediaItemController controller, string summary)
        {
            if (summary == null)
                throw new ArgumentNullException("summary");

            Summary = summary;

            if (item is IArtistViewModel)
            {
                controller.UpdateSummary<IArtist>(item.Id, summary);
            }
            else if (item is IAlbumViewModel)
            {
                controller.UpdateSummary<IAlbum>(item.Id, summary);
            }
            else if (item is ITrackViewModel)
            {
                controller.UpdateSummary<ITrack>(item.Id, summary);
            }
            else if (item is IClipViewModel)
            {
                controller.UpdateSummary<IClip>(item.Id, summary);
            }
        }

        public IPlaylistViewModel ToPlaylist(ISecurityContext securityContext)
        {
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");

            var album = item as IAlbumViewModel;
            if (album != null)
                return album.ToPlaylist(securityContext);

            var playable = item as IPlayableViewModel;
            if (playable != null)
                return playable.ToPlaylist(securityContext);

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
