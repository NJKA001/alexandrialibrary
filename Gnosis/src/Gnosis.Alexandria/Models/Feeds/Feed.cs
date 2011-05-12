using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class Feed
        : EntityBase, IFeed
    {

        public Feed(IContext context, Uri location)
            : base(context)
        {
            this.location = location;
            
            this.categories = new OrderedSet<IFeedCategory>(context);
            this.links = new OrderedSet<IFeedLink>(context);
            this.metadata = new OrderedSet<IFeedMetadata>(context);
            this.items = new OrderedSet<IFeedItem>(context);
        }

        public Feed(IContext context, Guid id, ITimeStamp timeStamp, Uri location, string mediaType, string title, string authors, string contributors, string description, string language, Uri originalLocation, string copyright, DateTime publishedDate, DateTime updatedDate, string generator, Uri imagePath, Uri iconPath, string feedIdentifier)
            : base(context, id, timeStamp)
        {
            this.location = location;
            this.mediaType = mediaType;
            this.title = title;
            this.authors = authors;
            this.contributors = contributors;
            this.description = description;
            this.language = language;
            this.originalLocation = originalLocation;
            this.copyright = copyright;
            this.publishedDate = publishedDate;
            this.updatedDate = updatedDate;
            this.generator = generator;
            this.imagePath = imagePath;
            this.iconPath = iconPath;
            this.feedIdentifier = feedIdentifier;

            this.categories = new OrderedSet<IFeedCategory>(context);
            this.links = new OrderedSet<IFeedLink>(context);
            this.metadata = new OrderedSet<IFeedMetadata>(context);
            this.items = new OrderedSet<IFeedItem>(context);
        }

        private readonly Uri location;
        private string mediaType;
        private string title;
        private string authors;
        private string contributors;
        private string description;
        private string language;
        private Uri originalLocation;
        private string copyright;
        private DateTime publishedDate;
        private DateTime updatedDate;
        private string generator;
        private Uri imagePath;
        private Uri iconPath;
        private string feedIdentifier;

        private readonly IOrderedSet<IFeedCategory> categories;
        private readonly IOrderedSet<IFeedLink> links;
        private readonly IOrderedSet<IFeedMetadata> metadata;
        private readonly IOrderedSet<IFeedItem> items;

        public Uri Location
        {
            get { return location; }
        }

        public string MediaType
        {
            get { return mediaType; }
            set
            {
                if (value != null && value != mediaType)
                {
                    OnEntityChanged(() => mediaType = value, "MediaType");
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value != null && value != title)
                {
                    OnEntityChanged(() => title = value, "Title");
                }
            }
        }

        public string Authors
        {
            get { return authors; }
            set
            {
                if (value != null && value != authors)
                {
                    OnEntityChanged(() => authors = value, "Authors");
                }
            }
        }

        public string Contributors
        {
            get { return contributors; }
            set
            {
                if (value != null && value != contributors)
                {
                    OnEntityChanged(() => contributors = value, "Contributors");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value != null && value != description)
                {
                    OnEntityChanged(() => description = value, "Description");
                }
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                if (value != null && value != language)
                {
                    OnEntityChanged(() => language = value, "Language");
                }
            }
        }

        public Uri OriginalLocation
        {
            get { return originalLocation; }
            set
            {
                if (value != null && value != originalLocation)
                {
                    OnEntityChanged(() => originalLocation = value, "OriginalLocation");
                }
            }
        }

        public string Copyright
        {
            get { return copyright; }
            set
            {
                if (value != null && value != copyright)
                {
                    OnEntityChanged(() => copyright = value, "Copyright");
                }
            }
        }

        public DateTime PublishedDate
        {
            get { return publishedDate; }
            set
            {
                if (value != null && value != publishedDate)
                {
                    OnEntityChanged(() => publishedDate = value, "PublishedDate");
                }
            }
        }

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != null && value != updatedDate)
                {
                    OnEntityChanged(() => updatedDate = value, "UpdatedDate");
                }
            }
        }

        public string Generator
        {
            get { return generator; }
            set
            {
                if (value != null && value != generator)
                {
                    OnEntityChanged(() => generator = value, "Generator");
                }
            }
        }

        public Uri ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value != null && value != imagePath)
                {
                    OnEntityChanged(() => imagePath = value, "ImagePath");
                }
            }
        }

        public Uri IconPath
        {
            get { return iconPath; }
            set
            {
                if (value != null && value != iconPath)
                {
                    OnEntityChanged(() => iconPath = value, "IconPath");
                }
            }
        }

        public string FeedIdentifier
        {
            get { return feedIdentifier; }
            set
            {
                if (value != null && value != feedIdentifier)
                {
                    OnEntityChanged(() => feedIdentifier = value, "FeedIdentifier");
                }
            }
        }

        public IOrderedSet<IFeedCategory> Categories
        {
            get { return categories; }
        }

        public IOrderedSet<IFeedLink> Links
        {
            get { return links; }
        }

        public IOrderedSet<IFeedMetadata> Metadata
        {
            get { return metadata; }
        }

        public IOrderedSet<IFeedItem> Items
        {
            get { return items; }
        }
    }
}
