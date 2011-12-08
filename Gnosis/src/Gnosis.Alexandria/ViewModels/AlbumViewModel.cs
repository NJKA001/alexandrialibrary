using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.ViewModels
{
    public class AlbumViewModel
        : IAlbumViewModel
    {
        public AlbumViewModel(Uri album, string title, Uri artist, string artistName, DateTime date, Uri thumbnail, byte[] thumbnailData, string bio)
        {
            if (album == null)
                throw new ArgumentNullException("album");
            if (title == null)
                throw new ArgumentNullException("title");
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (artistName == null)
                throw new ArgumentNullException("artistName");

            this.album = album;
            this.title = title;
            this.artist = artist;
            this.artistName = artistName;
            this.date = date;
            this.bio = bio;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
        }

        private readonly Uri album;
        private readonly string title;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly DateTime date;
        private readonly string bio;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly ObservableCollection<ITrackViewModel> tracks = new ObservableCollection<ITrackViewModel>();

        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Album
        {
            get { return album; }
        }

        public string Title
        {
            get { return title; }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public string Year
        {
            get { return date.Year.ToString(); }
        }

        public string Bio
        {
            get { return bio; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return tracks; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize(IEnumerable<ITrack> tracks)
        {
            if (tracks == null)
                throw new ArgumentNullException("tracks");

            if (this.tracks.Count > 0)
                return;

            foreach (var track in tracks)
            {
                var trackViewModel = new TrackViewModel(track.Location, track.Name, track.Number, track.Duration, track.FromDate, track.Creator, track.CreatorName, track.Catalog, track.CatalogName, track.Target, track.TargetType, track.Thumbnail, track.ThumbnailData, string.Empty);
                this.tracks.Add(trackViewModel);
            }
        }

        public void AddTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            tracks.Add(track);
        }

        public void RemoveTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (tracks.Contains(track))
                tracks.Remove(track);
        }

        public override string ToString()
        {
            return string.Format("Album: {0}. IsSelected={1}", Title, IsSelected);
        }

        public IPlaylistViewModel ToPlaylist(ISecurityContext securityContext)
        {
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");

            var playlist = new GnosisPlaylist(title, DateTime.Now, 0, TimeSpan.Zero, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, thumbnailData);
            var items = new List<IPlaylistItemViewModel>();
            foreach (var track in tracks)
            {
                var item = track.ToPlaylistItem(securityContext);
                items.Add(item);
            }

            return new PlaylistViewModel(playlist, items);
        }
    }
}
