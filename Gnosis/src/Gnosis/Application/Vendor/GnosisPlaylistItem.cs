using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisPlaylistItem
        : IPlaylistItem
    {
        public GnosisPlaylistItem(string title, uint sequence, TimeSpan duration, Uri playlist, string playlistTitle, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(title, sequence, duration, playlist, playlistTitle, creator, creatorName, album, albumTitle, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisPlaylistItem(string title, uint sequence, TimeSpan duration, Uri playlist, string playlistTitle, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (playlist == null)
                throw new ArgumentNullException("playlist");
            if (playlistTitle == null)
                throw new ArgumentNullException("playlistTitle");
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (creatorName == null)
                throw new ArgumentNullException("creatorName");
            if (album == null)
                throw new ArgumentNullException("album");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (user == null)
                throw new ArgumentNullException("user");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.sequence = sequence;
            this.duration = duration;
            this.playlist = playlist;
            this.playlistTitle = playlistTitle;
            this.creator = creator;
            this.creatorName = creatorName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.target = target;
            this.targetType = targetType;
            this.user = user;
            this.userName = userName;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly string title;
        private readonly uint sequence;
        private readonly TimeSpan duration;
        private readonly Uri playlist;
        private readonly string playlistTitle;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri user;
        private readonly string userName;
        private readonly Uri thumbnail;
        private readonly Uri location;

        public string Title
        {
            get { return title; }
        }

        public uint Sequence
        {
            get { return sequence; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public Uri Playlist
        {
            get { return playlist; }
        }

        public string PlaylistTitle
        {
            get { return playlistTitle; }
        }

        public Uri Creator
        {
            get { return creator; }
        }

        public string CreatorName
        {
            get { return creatorName; }
        }

        public Uri Album
        {
            get { return album; }
        }

        public string AlbumTitle
        {
            get { return albumTitle; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IMediaType TargetType
        {
            get { return targetType; }
        }

        public Uri User
        {
            get { return user; }
        }

        public string UserName
        {
            get { return userName; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
        }

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisPlaylistItem; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

    }
}
