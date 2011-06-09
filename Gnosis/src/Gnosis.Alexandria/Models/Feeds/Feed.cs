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
        public Feed()
        {
            AddInitializer("Location", x => this.location = x.ToUri());
            AddInitializer("MediaType", x => this.mediaType = x.ToString());
            AddInitializer("Title", x => this.title = x.ToString());
            AddInitializer("Authors", x => this.authors = x.ToString());
            AddInitializer("Contributors", x => this.contributors = x.ToString());
            AddInitializer("Description", x => this.description = x.ToString());
            AddInitializer("Language", x => this.language = x.ToString());
            AddInitializer("OriginalLocation", x => this.originalLocation = x.ToUri());
            AddInitializer("Copyright", x => this.copyright = x.ToString());
            AddInitializer("PublishedDate", x => this.publishedDate = x.ToDateTime());
            AddInitializer("UpdatedDate", x => this.updatedDate = x.ToDateTime());
            AddInitializer("Generator", x => this.generator = x.ToString());
            AddInitializer("ImagePath", x => this.imagePath = x.ToUri());
            AddInitializer("IconPath", x => this.iconPath = x.ToUri());
            AddInitializer("FeedIdentifier", x => this.feedIdentifier = x.ToString());
        }

        private Uri location;
        private string mediaType;
        private string title;
        private string authors;
        private string contributors;
        private string description;
        private string language;
        private Uri originalLocation = UriExtensions.EmptyUri;
        private string copyright = string.Empty;
        private DateTime publishedDate;
        private DateTime updatedDate;
        private string generator = string.Empty;
        private Uri imagePath;
        private Uri iconPath;
        private string feedIdentifier = string.Empty;

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
                    Change(() => location = value, "Location");
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
                    Change(() => mediaType = value, "MediaType");
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
                    Change(() => title = value, "Title");
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
                    Change(() => authors = value, "Authors");
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
                    Change(() => contributors = value, "Contributors");
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
                    Change(() => description = value, "Description");
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
                    Change(() => language = value, "Language");
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
                    Change(() => originalLocation = value, "OriginalLocation");
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
                    Change(() => copyright = value, "Copyright");
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
                    Change(() => publishedDate = value, "PublishedDate");
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
                    Change(() => updatedDate = value, "UpdatedDate");
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
                    Change(() => generator = value, "Generator");
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
                    Change(() => imagePath = value, "ImagePath");
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
                    Change(() => iconPath = value, "IconPath");
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
                    Change(() => feedIdentifier = value, "FeedIdentifier");
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
