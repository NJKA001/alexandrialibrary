using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisTrack
        : ITrack
    {
        public GnosisTrack(string title, uint number, TimeSpan duration, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail)
            : this(title, number, duration, creator, creatorName, album, albumTitle, target, targetType, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisTrack(string title, uint number, TimeSpan duration, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (creatorName == null)
                throw new ArgumentNullException("creatorName");
            if (album == null)
                throw new ArgumentNullException("album");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (target == null)
                throw new ArgumentNullException("target");
            if (targetType == null)
                throw new ArgumentNullException("targetType");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.number = number;
            this.duration = duration;
            this.creator = creator;
            this.creatorName = creatorName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.target = target;
            this.targetType = targetType;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string title;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisTrack; }
        }

        public string Title
        {
            get { return title; }
        }

        public uint Number
        {
            get { return number; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
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

        public Uri Thumbnail
        {
            get { return thumbnail; }
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
