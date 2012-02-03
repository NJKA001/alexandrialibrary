using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Links;
using Gnosis.Metadata;
using Gnosis.Tags;

namespace Gnosis.Metadata
{
    public abstract class MediaItemBase
        : IMediaItem
    {
        protected MediaItemBase(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            this.name = identityInfo.Name;
            this.summary = identityInfo.Summary;
            this.fromDate = identityInfo.FromDate;
            this.toDate = identityInfo.ToDate;
            this.number = identityInfo.Number;
            this.duration = sizeInfo.Duration;
            this.height = sizeInfo.Height;
            this.width = sizeInfo.Width;
            this.creator = creatorInfo.Location;
            this.creatorName = creatorInfo.Name;
            this.catalog = catalogInfo.Location;
            this.catalogName = catalogInfo.Name;
            this.target = targetInfo.Location;
            this.targetType = targetInfo.Type;
            this.user = userInfo.Location;
            this.userName = userInfo.Name;
            this.thumbnail = thumbnailInfo.Location;
            this.thumbnailData = thumbnailInfo.Data;
            this.type = identityInfo.Type;
            this.location = identityInfo.Location;
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
