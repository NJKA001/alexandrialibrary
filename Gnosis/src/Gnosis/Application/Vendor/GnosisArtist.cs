using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisArtist
        : IArtist
    {
        public GnosisArtist(string name, DateTime activeFrom, DateTime activeTo, Uri target, IMediaType targetType, Uri thumbnail)
            : this(name, activeFrom, activeTo, target, targetType, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisArtist(string name, DateTime activeFrom, DateTime activeTo, Uri target, IMediaType targetType, Uri thumbnail, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.activeFrom = activeFrom;
            this.activeTo = activeTo;
            this.target = target;
            this.targetType = targetType;

            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string name;
        private readonly DateTime activeFrom;
        private readonly DateTime activeTo;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisArtist; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime ActiveFrom
        {
            get { return activeFrom; }
        }

        public DateTime ActiveTo
        {
            get { return activeTo; }
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

        public static readonly IArtist Unknown = new GnosisArtist("Unknown", DateTime.MinValue, DateTime.MaxValue, null, null, null, Guid.Empty.ToUrn());
    }
}
