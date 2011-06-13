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
            AddInitializer("Location", value => this.location = value.ToUri());
            AddInitializer("MediaType", value => this.mediaType = value.ToString());
            AddInitializer("Title", value => this.title = value.ToString());
            AddInitializer("Authors", value => this.authors = value.ToString());
            AddInitializer("Contributors", value => this.contributors = value.ToString());
            AddInitializer("Description", value => this.description = value.ToString());
            AddInitializer("Language", value => this.language = value.ToString());
            AddInitializer("OriginalLocation", value => this.originalLocation = value.ToUri());
            AddInitializer("Copyright", value => this.copyright = value.ToString());
            AddInitializer("PublishedDate", value => this.publishedDate = value.ToDateTime());
            AddInitializer("UpdatedDate", value => this.updatedDate = value.ToDateTime());
            AddInitializer("Generator", value => this.generator = value.ToString());
            AddInitializer("ImagePath", value => this.imagePath = value.ToUri());
            AddInitializer("IconPath", value => this.iconPath = value.ToUri());
            AddInitializer("FeedIdentifier", value => this.feedIdentifier = value.ToString());
            AddChildInitializer("FeedItem", child => AddItem(child as IFeedItem));
            AddValueInitializer("Feed_Categories", value => AddCategory(value as IFeedCategory));
            AddValueInitializer("Feed_Links", value => AddLink(value as IFeedLink));
            AddValueInitializer("Feed_Metadata", value => AddMetadatum(value as IFeedMetadatum));
            AddValueInitializer("Feed_TitleHashCodes", value => AddTitleHashCode(value as IHashCode));
            AddValueInitializer("Feed_AuthorHashCodes", value => AddAuthorHashCode(value as IHashCode));
            AddValueInitializer("Feed_ContributorHashCodes", value => AddContributorHashCode(value as IHashCode));
            AddValueInitializer("Feed_DescriptionHashCodes", value => AddDescriptionHashCode(value as IHashCode));
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
        private readonly IList<IFeedMetadatum> metadata = new ObservableCollection<IFeedMetadatum>();
        private readonly IList<IFeedItem> items = new ObservableCollection<IFeedItem>();

        private readonly IList<IHashCode> titleHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> authorHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> contributorHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> descriptionHashCodes = new ObservableCollection<IHashCode>();

        #region Private Methods

        private void AddCategory(IFeedCategory category)
        {
            AddValue(() => categories.Add(category), category, "Categories");
        }

        private void AddLink(IFeedLink link)
        {
            AddValue(() => links.Add(link), link, "Links");
        }

        private void AddMetadatum(IFeedMetadatum metadatum)
        {
            AddValue(() => metadata.Add(metadatum), metadatum, "Metadata");
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue(() => titleHashCodes.Add(hashCode), hashCode, "TitleHashCodes");
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue(() => titleHashCodes.Remove(hashCode), hashCode.Id, "TitleHashCodes");
        }

        private void AddAuthorHashCode(IHashCode hashCode)
        {
            AddValue(() => authorHashCodes.Add(hashCode), hashCode, "AuthorHashCodes");
        }

        private void RemoveAuthorHashCode(IHashCode hashCode)
        {
            RemoveValue(() => authorHashCodes.Remove(hashCode), hashCode.Id, "AuthorHashCodes");
        }

        private void AddContributorHashCode(IHashCode hashCode)
        {
            AddValue(() => contributorHashCodes.Add(hashCode), hashCode, "ContributorHashCodes");
        }

        private void RemoveContributorHashCode(IHashCode hashCode)
        {
            RemoveValue(() => contributorHashCodes.Remove(hashCode), hashCode.Id, "ContributorHashCodes");
        }

        private void AddDescriptionHashCode(IHashCode hashCode)
        {
            AddValue(() => descriptionHashCodes.Add(hashCode), hashCode, "DescriptionHashCodes");
        }

        private void RemoveDescriptionHashCode(IHashCode hashCode)
        {
            RemoveValue(() => descriptionHashCodes.Remove(hashCode), hashCode.Id, "DescriptionHashCodes");
        }

        #endregion

        #region IFeed Members

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

        public IEnumerable<IFeedMetadatum> Metadata
        {
            get { return metadata; }
        }

        public IEnumerable<IFeedItem> Items
        {
            get { return items; }
        }


        public IEnumerable<IHashCode> TitleHashCodes
        {
            get { return titleHashCodes; }
        }

        public IEnumerable<IHashCode> AuthorHashCodes
        {
            get { return authorHashCodes; }
        }

        public IEnumerable<IHashCode> ContributorHashCodes
        {
            get { return contributorHashCodes; }
        }

        public IEnumerable<IHashCode> DescriptionHashCodes
        {
            get { return descriptionHashCodes; }
        }


        public void AddCategory(Uri scheme, string name, string label)
        {
            AddCategory(new FeedCategory(this.Id, scheme, name, label));
        }

        public void RemoveCategory(IFeedCategory category)
        {
            RemoveValue(() => categories.Remove(category), category.Id, "Categories");
        }

        public void AddLink(string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddLink(new FeedLink(this.Id, relationship, location, mediaType, length, language));
        }

        public void RemoveLink(IFeedLink link)
        {
            RemoveValue(() => links.Remove(link), link.Id, "Links");
        }

        public void AddMetadatum(string mediaType, Uri scheme, string name, string content)
        {
            AddMetadatum(new FeedMetadatum(this.Id, mediaType, scheme, name, content));
        }

        public void RemoveMetadatum(IFeedMetadatum metadatum)
        {
            RemoveValue(() => metadata.Remove(metadatum), metadatum.Id, "Metadata");
        }

        public void AddItem(IFeedItem item)
        {
            AddChild(() => items.Add(item), item, "Items");
        }

        public void RemoveItem(IFeedItem item)
        {
            RemoveChild(() => items.Remove(item), item.Id, "Items");
        }

        #endregion
    }
}
