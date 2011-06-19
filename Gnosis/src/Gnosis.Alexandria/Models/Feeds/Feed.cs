using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Iso;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class Feed
        : EntityBase<IFeed>, IFeed
    {
        public Feed()
        {
            AddInitializer(value => this.location = value.ToUri(), x => x.Location);
            AddInitializer(value => this.mediaType = value.ToString(), x => x.MediaType);
            AddInitializer(value => this.title = value.ToString(), x => x.Title);
            AddInitializer(value => this.authors = value.ToString(), x => x.Authors);
            AddInitializer(value => this.contributors = value.ToString(), x => x.Contributors);
            AddInitializer(value => this.description = value.ToString(), x => x.Description);
            AddInitializer(value => this.language = value.ToLanguage(), feed => feed.Language);
            AddInitializer(value => this.originalLocation = value.ToUri(), x => x.OriginalLocation);
            AddInitializer(value => this.copyright = value.ToString(), x => x.Copyright);
            AddInitializer(value => this.publishedDate = value.ToDateTime(), x => x.PublishedDate);
            AddInitializer(value => this.updatedDate = value.ToDateTime(), x => x.UpdatedDate);
            AddInitializer(value => this.generator = value.ToString(), x => x.Generator);
            AddInitializer(value => this.imagePath = value.ToUri(), x => x.ImagePath);
            AddInitializer(value => this.iconPath = value.ToUri(), x => x.IconPath);
            AddInitializer(value => this.feedIdentifier = value.ToString(), x => x.FeedIdentifier);

            AddChildInitializer<IFeedItem>(child => AddItem(child as IFeedItem));
            
            AddValueInitializer(value => AddCategory(value as IFeedCategory), x => x.Categories);
            AddValueInitializer(value => AddLink(value as IFeedLink), x => x.Links);
            AddValueInitializer(value => AddMetadatum(value as IFeedMetadatum), x => x.Metadata);
            AddValueInitializer(value => AddTitleHashCode(value as IHashCode), x => x.TitleHashCodes);
            AddValueInitializer(value => AddAuthorHashCode(value as IHashCode), x => x.AuthorHashCodes);
            AddValueInitializer(value => AddContributorHashCode(value as IHashCode), x => x.ContributorHashCodes);
            AddValueInitializer(value => AddDescriptionHashCode(value as IHashCode), x => x.DescriptionHashCodes);

            AddHashFunction(HashCode.SchemeDoubleMetaphone, token => HashCode.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(HashCode.SchemeNameHash, token => HashCode.CreateNameHash(this.Id, token));

            AddHashInitializer(hashCode => AddTitleHashCode(hashCode), hashCode => RemoveTitleHashCode(hashCode), feed => feed.TitleHashCodes);
            AddHashInitializer(hashCode => AddAuthorHashCode(hashCode), hashCode => RemoveAuthorHashCode(hashCode), feed => feed.AuthorHashCodes);
            AddHashInitializer(hashCode => AddContributorHashCode(hashCode), hashCode => RemoveContributorHashCode(hashCode), feed => feed.ContributorHashCodes);
            AddHashInitializer(hashCode => AddDescriptionHashCode(hashCode), hashCode => RemoveDescriptionHashCode(hashCode), feed => feed.DescriptionHashCodes);
        }

        private Uri location;
        private string mediaType = "application/xml+rss";
        private string title = string.Empty;
        private string authors = string.Empty;
        private string contributors = string.Empty;
        private string description = string.Empty;
        private ILanguage language = Gnosis.Core.Iso.Language.Undetermined;
        private Uri originalLocation = UriExtensions.EmptyUri;
        private string copyright = string.Empty;
        private DateTime publishedDate = DateTime.MinValue;
        private DateTime updatedDate = DateTime.MinValue;
        private string generator = string.Empty;
        private Uri imagePath = UriExtensions.EmptyUri;
        private Uri iconPath = UriExtensions.EmptyUri;
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
            AddValue<IFeedCategory>(() => categories.Add(category), category, x => x.Categories);
        }

        private void AddLink(IFeedLink link)
        {
            AddValue<IFeedLink>(() => links.Add(link), link, x => x.Links);
        }

        private void AddMetadatum(IFeedMetadatum metadatum)
        {
            AddValue<IFeedMetadatum>(() => metadata.Add(metadatum), metadatum, x => x.Metadata);
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => titleHashCodes.Add(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => titleHashCodes.Remove(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void AddAuthorHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => authorHashCodes.Add(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void RemoveAuthorHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => authorHashCodes.Remove(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void AddContributorHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => contributorHashCodes.Add(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void RemoveContributorHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => contributorHashCodes.Remove(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void AddDescriptionHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => descriptionHashCodes.Add(hashCode), hashCode, x => x.DescriptionHashCodes);
        }

        private void RemoveDescriptionHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => descriptionHashCodes.Remove(hashCode), hashCode, x => x.DescriptionHashCodes);
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
                    Change(() => location = value, x => x.Location);
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
                    Change(() => mediaType = value, x => x.MediaType);
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
                    Change(() => title = value, x => x.Title);
                    RefreshHashCodes(value, x => x.TitleHashCodes);
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
                    Change(() => authors = value, x => x.Authors);
                    RefreshHashCodes(value, x => x.AuthorHashCodes);
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
                    Change(() => contributors = value, x => x.Contributors);
                    RefreshHashCodes(value, x => x.ContributorHashCodes);
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
                    Change(() => description = value, x => x.Description);
                    RefreshHashCodes(value, x => x.DescriptionHashCodes);
                }
            }
        }

        public ILanguage Language
        {
            get { return language; }
            set
            {
                if (value != null && value != language)
                {
                    Change(() => language = value, feed => feed.Language);
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
                    Change(() => originalLocation = value, x => x.OriginalLocation);
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
                    Change(() => copyright = value, x => x.Copyright);
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
                    Change(() => publishedDate = value, x => x.PublishedDate);
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
                    Change(() => updatedDate = value, x => x.UpdatedDate);
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
                    Change(() => generator = value, x => x.Generator);
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
                    Change(() => imagePath = value, x => x.ImagePath);
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
                    Change(() => iconPath = value, x => x.IconPath);
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
                    Change(() => feedIdentifier = value, x => x.FeedIdentifier);
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
            RemoveValue<IFeedCategory>(() => categories.Remove(category), category, x => x.Categories);
        }

        public void AddLink(string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddLink(new FeedLink(this.Id, relationship, location, mediaType, length, language));
        }

        public void RemoveLink(IFeedLink link)
        {
            RemoveValue<IFeedLink>(() => links.Remove(link), link, x => x.Links);
        }

        public void AddMetadatum(string mediaType, Uri scheme, string name, string content)
        {
            AddMetadatum(new FeedMetadatum(this.Id, mediaType, scheme, name, content));
        }

        public void RemoveMetadatum(IFeedMetadatum metadatum)
        {
            RemoveValue<IFeedMetadatum>(() => metadata.Remove(metadatum), metadatum, x => x.Metadata);
        }

        public void AddItem(IFeedItem item)
        {
            AddChild<IFeedItem>(() => items.Add(item), item, x => x.Items);
        }

        public void RemoveItem(IFeedItem item)
        {
            RemoveChild<IFeedItem>(() => items.Remove(item), item, x => x.Items);
        }

        #endregion
    }
}
