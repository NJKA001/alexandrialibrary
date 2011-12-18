using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Links;
using Gnosis.Tags;

namespace Gnosis.Application.Vendor
{
    public abstract class GnosisMediaItemBase
        : IMediaItem, IApplication
    {
        protected GnosisMediaItemBase(string name, string summary, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, IMediaType type, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (summary == null)
                throw new ArgumentNullException("summary");
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (creatorName == null)
                throw new ArgumentNullException("creatorName");
            if (catalog == null)
                throw new ArgumentNullException("catalog");
            if (catalogName == null)
                throw new ArgumentNullException("catalogName");
            if (target == null)
                throw new ArgumentNullException("target");
            if (targetType == null)
                throw new ArgumentNullException("targetType");
            if (user == null)
                throw new ArgumentNullException("user");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");
            if (type == null)
                throw new ArgumentNullException("type");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.summary = summary;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.number = number;
            this.duration = duration;
            this.height = height;
            this.width = width;
            this.creator = creator;
            this.creatorName = creatorName;
            this.catalog = catalog;
            this.catalogName = catalogName;
            this.target = target;
            this.targetType = targetType;
            this.user = user;
            this.userName = userName;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
            this.type = type;
            this.location = location;
        }

        private readonly string name;
        private readonly string summary;
        private readonly DateTime fromDate;
        private readonly DateTime toDate;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly uint height;
        private readonly uint width;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri catalog;
        private readonly string catalogName;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri user;
        private readonly string userName;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly IMediaType type;
        private readonly Uri location;

        public string Name
        {
            get { return name; }
        }

        public string Summary
        {
            get { return summary; }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
        }

        public DateTime ToDate
        {
            get { return toDate; }
        }

        public uint Number
        {
            get { return number; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public uint Height
        {
            get { return height; }
        }

        public uint Width
        {
            get { return width; }
        }

        public Uri Creator
        {
            get { return creator; }
        }

        public string CreatorName
        {
            get { return creatorName; }
        }

        public Uri Catalog
        {
            get { return catalog; }
        }

        public string CatalogName
        {
            get { return catalogName; }
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

        public byte[] ThumbnailData
        {
            get { return thumbnailData; }
        }

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public virtual void Load()
        {
        }

        public virtual IEnumerable<ILink> GetLinks()
        {
            var links = new List<ILink>();

            if (target != null && !target.IsEmptyUrn())
            {
                links.Add(new Link(location, target, "enclosure", targetType.ToString()));
            }

            return links;
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            var tags = new List<ITag>();

            var americanized = Name.ToAmericanizedString();
            if (americanized != Name.ToUpper())
                tags.Add(new Tag(Location, TagType.DefaultString, Name.ToAmericanizedString(), Algorithm.Americanized));

            var tokens = Name.Split(' ');
            if (tokens.Length == 1)
                return tags;

            foreach (var token in tokens)
            {
                if (token == null)
                    continue;
                
                var tokenTrimmed = token.Trim();
                if (string.IsNullOrEmpty(tokenTrimmed))
                    continue;

                tags.Add(new Tag(Location, TagType.MediaName, tokenTrimmed));

                var tokenAmericanized = tokenTrimmed.ToAmericanizedString();
                if (!string.IsNullOrEmpty(tokenAmericanized.Trim()) && tokenAmericanized != tokenTrimmed.ToUpper())
                    tags.Add(new Tag(Location, TagType.MediaName, tokenAmericanized));
            }

            return tags;
        }
    }
}
