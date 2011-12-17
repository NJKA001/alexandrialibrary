using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.ViewModels
{
    public class AlbumViewModel
        : MediaItemViewModel, IAlbumViewModel
    {
        public AlbumViewModel(IMediaItemController controller, IAlbum album)
            : base(controller, album, "ALBUM", "pack://application:,,,/Images/cd.png")
        {
        }

        private readonly ObservableCollection<ITrackViewModel> tracks = new ObservableCollection<ITrackViewModel>();
        private readonly ObservableCollection<IClipViewModel> clips = new ObservableCollection<IClipViewModel>();

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return tracks; }
        }

        public IEnumerable<IClipViewModel> Clips
        {
            get { return clips; }
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

        public void AddClip(IClipViewModel clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            clips.Add(clip);
        }

        public void RemoveClip(IClipViewModel clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            if (clips.Contains(clip))
                clips.Remove(clip);
        }

        public override string ToString()
        {
            return string.Format("Album: {0}. IsSelected={1}", Name, IsSelected);
        }

        public IPlaylistViewModel ToPlaylist(ISecurityContext securityContext)
        {
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");

            var playlist = new GnosisPlaylist(Name, Summary, DateTime.Now, 0, TimeSpan.Zero, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, item.Thumbnail, item.ThumbnailData);
            var playlistItems = new List<IPlaylistItemViewModel>();
            foreach (var track in tracks)
            {
                var playlistItem = track.ToPlaylistItem(securityContext);
                playlistItems.Add(playlistItem);
            }
            foreach (var clip in clips)
            {
                var playlistItem = clip.ToPlaylistItem(securityContext);
                playlistItems.Add(playlistItem);
            }

            return new PlaylistViewModel(controller, playlist, playlistItems);
        }
    }
}
