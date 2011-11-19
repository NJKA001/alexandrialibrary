using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisPlaylist
        : IPlaylist
    {
        public GnosisPlaylist(string title, DateTime created, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(title, created, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisPlaylist(string title, DateTime created, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (created == null)
                throw new ArgumentNullException("created");
            if (user == null)
                throw new ArgumentNullException("user");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.created = created;
            this.target = target;
            this.targetType = targetType;
            this.user = user;
            this.userName = userName;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly string title;
        private readonly DateTime created;
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

        public DateTime Created
        {
            get { return created; }
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
            get { return MediaType.ApplicationGnosisPlaylist; }
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
