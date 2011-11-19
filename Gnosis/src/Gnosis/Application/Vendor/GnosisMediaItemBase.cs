using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public abstract class GnosisMediaItemBase
        : IMediaItem, IApplication
    {
        protected GnosisMediaItemBase(string name, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, IMediaType type, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
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
            if (type == null)
                throw new ArgumentNullException("type");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.creator = creator;
            this.creatorName = creatorName;
            this.catalog = catalog;
            this.catalogName = catalogName;
            this.target = target;
            this.targetType = targetType;
            this.user = user;
            this.userName = userName;
            this.thumbnail = thumbnail;
            this.type = type;
            this.location = location;
        }

        private readonly string name;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri catalog;
        private readonly string catalogName;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri user;
        private readonly string userName;
        private readonly Uri thumbnail;
        private readonly IMediaType type;
        private readonly Uri location;

        public string Name
        {
            get { return name; }
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
            return Enumerable.Empty<ILink>();
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
