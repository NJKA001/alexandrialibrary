using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class Feed
        : EntityBase, IFeed
    {

        public Feed(IContext context)
            : base(context)
        {
        }

        public Feed(IContext context, Guid id, DateTime timeStamp, Uri location, string mediaType, string title, string authors, string contributors, string description, string language, Uri originalLocation, string copyright, DateTime publishedDate, DateTime updatedDate, string generator, Uri imagePath, Uri iconPath, string feedIdentifier)
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
        }

        private Uri location;
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

        private readonly IList<IFeedCategory> categories = new ObservableCollection<IFeedCategory>();
        private readonly IList<IFeedLink> links = new ObservableCollection<IFeedLink>();
        private readonly IList<IFeedMetadata> metadata = new ObservableCollection<IFeedMetadata>();
        private readonly IList<IFeedItem> items = new ObservableCollection<IFeedItem>();

        public Uri Location
        {
            get { return location; }
            set
            {
                if (value != null && value != location)
                {
                    OnEntityChanged(() => location = value, "Location");
                }
            }
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

        public IEnumerable<IFeedCategory> Categories
        {
            get { return categories; }
        }

        public IEnumerable<IFeedLink> Links
        {
            get { return links; }
        }

        public IEnumerable<IFeedMetadata> Metadata
        {
            get { return metadata; }
        }

        public IEnumerable<IFeedItem> Items
        {
            get { return items; }
        }

        public void AddCategory(IFeedCategory category)
        {
            AddValue(() => categories.Add(category), category, "Categories");
        }

        public void RemoveCategory(IFeedCategory category)
        {
            RemoveValue(() => categories.Remove(category), category, "Categories");
        }

        public void AddLink(IFeedLink link)
        {
            AddValue(() => links.Add(link), link, "Links");
        }

        public void RemoveLink(IFeedLink link)
        {
            RemoveValue(() => links.Remove(link), link, "Links");
        }

        public void AddMetadatum(IFeedMetadata metadatum)
        {
            AddValue(() => metadata.Add(metadatum), metadatum, "Metadata");
        }

        public void RemoveMetadatum(IFeedMetadata metadatum)
        {
            RemoveValue(() => metadata.Remove(metadatum), metadatum, "Metadata");
        }

        public void AddItem(IFeedItem item)
        {
            AddChild(() => items.Add(item), item, "Items");
        }

        public void RemoveItem(IFeedItem item)
        {
            RemoveChild(() => items.Remove(item), item, "Items");
        }
    }
}
