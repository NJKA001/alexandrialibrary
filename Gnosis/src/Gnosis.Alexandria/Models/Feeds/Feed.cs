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
            AddValueInitializer("Feed_TitleHashCodes", value => ReplaceTitleHashCode(value as IHashCode));
            AddValueInitializer("Feed_AuthorHashCodes", value => ReplaceAuthorHashCode(value as IHashCode));
            AddValueInitializer("Feed_ContributorHashCodes", value => ReplaceContributorHashCode(value as IHashCode));
            AddValueInitializer("Feed_DescriptionHashCodes", value => ReplaceDescriptionHashCode(value as IHashCode));
        }

        private Uri location;
        private string mediaType = "application/xml+rss";
        private string title = string.Empty;
        private string authors = string.Empty;
        private string contributors = string.Empty;
        private string description = string.Empty;
        private string language = "en-us";
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
            AddValue<IFeed, IFeedCategory>(() => categories.Add(category), category, x => x.Categories);
        }

        private void AddLink(IFeedLink link)
        {
            AddValue<IFeed, IFeedLink>(() => links.Add(link), link, x => x.Links);
        }

        private void AddMetadatum(IFeedMetadatum metadatum)
        {
            AddValue<IFeed, IFeedMetadatum>(() => metadata.Add(metadatum), metadatum, x => x.Metadata);
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue<IFeed, IHashCode>(() => titleHashCodes.Add(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeed, IHashCode>(() => titleHashCodes.Remove(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void ReplaceTitleHashCode(IHashCode hashCode)
        {
            ReplaceTitleHashCode(hashCode.Scheme, hashCode.Value);
        }

        private void ReplaceTitleHashCode(Uri scheme, string value)
        {
            var existing = titleHashCodes.Where(hashCode => hashCode.Scheme == scheme).FirstOrDefault();
            if (existing != null)
            {
                if (existing.Value == value)
                    return;

                RemoveTitleHashCode(existing);
            }

            AddTitleHashCode(new HashCode(this.Id, scheme, value));
        }

        private void AddAuthorHashCode(IHashCode hashCode)
        {
            AddValue<IFeed, IHashCode>(() => authorHashCodes.Add(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void RemoveAuthorHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeed, IHashCode>(() => authorHashCodes.Remove(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void ReplaceAuthorHashCode(IHashCode hashCode)
        {
            ReplaceAuthorHashCode(hashCode.Scheme, hashCode.Value);
        }

        private void ReplaceAuthorHashCode(Uri scheme, string value)
        {
            var existing = authorHashCodes.Where(hashCode => hashCode.Scheme == scheme).FirstOrDefault();
            if (existing != null)
            {
                if (existing.Value == value)
                    return;

                RemoveAuthorHashCode(existing);
            }

            AddAuthorHashCode(new HashCode(this.Id, scheme, value));
        }

        private void AddContributorHashCode(IHashCode hashCode)
        {
            AddValue<IFeed, IHashCode>(() => contributorHashCodes.Add(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void RemoveContributorHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeed, IHashCode>(() => contributorHashCodes.Remove(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void ReplaceContributorHashCode(IHashCode hashCode)
        {
            ReplaceContributorHashCode(hashCode.Scheme, hashCode.Value);
        }

        private void ReplaceContributorHashCode(Uri scheme, string value)
        {
            var existing = contributorHashCodes.Where(hashCode => hashCode.Scheme == scheme).FirstOrDefault();
            if (existing != null)
            {
                if (existing.Value == value)
                    return;

                RemoveContributorHashCode(existing);
            }

            AddContributorHashCode(new HashCode(this.Id, scheme, value));
        }

        private void AddDescriptionHashCode(IHashCode hashCode)
        {
            AddValue<IFeed, IHashCode>(() => descriptionHashCodes.Add(hashCode), hashCode, x => x.DescriptionHashCodes);
        }

        private void RemoveDescriptionHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeed, IHashCode>(() => descriptionHashCodes.Remove(hashCode), hashCode, x => x.DescriptionHashCodes);
        }

        private void ReplaceDescriptionHashCode(IHashCode hashCode)
        {
            ReplaceDescriptionHashCode(hashCode.Scheme, hashCode.Value);
        }

        private void ReplaceDescriptionHashCode(Uri scheme, string value)
        {
            var existing = descriptionHashCodes.Where(hashCode => hashCode.Scheme == scheme).FirstOrDefault();
            if (existing != null)
            {
                if (existing.Value == value)
                    return;

                RemoveDescriptionHashCode(existing);
            }

            AddDescriptionHashCode(new HashCode(this.Id, scheme, value));
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
                    ReplaceTitleHashCode(HashCode.SchemeDoubleMetaphone, value.AsDoubleMetaphone());
                    ReplaceTitleHashCode(HashCode.SchemeNameHash, value.AsNameHash());
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
                    ReplaceAuthorHashCode(HashCode.SchemeDoubleMetaphone, value.AsDoubleMetaphone());
                    ReplaceAuthorHashCode(HashCode.SchemeNameHash, value.AsNameHash());
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
                    ReplaceContributorHashCode(HashCode.SchemeDoubleMetaphone, value.AsDoubleMetaphone());
                    ReplaceContributorHashCode(HashCode.SchemeNameHash, value.AsNameHash());
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
                    ReplaceDescriptionHashCode(HashCode.SchemeDoubleMetaphone, value.AsDoubleMetaphone());
                    ReplaceDescriptionHashCode(HashCode.SchemeNameHash, value.AsNameHash());
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
            RemoveValue<IFeed, IFeedCategory>(() => categories.Remove(category), category, x => x.Categories);
        }

        public void AddLink(string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddLink(new FeedLink(this.Id, relationship, location, mediaType, length, language));
        }

        public void RemoveLink(IFeedLink link)
        {
            RemoveValue<IFeed, IFeedLink>(() => links.Remove(link), link, x => x.Links);
        }

        public void AddMetadatum(string mediaType, Uri scheme, string name, string content)
        {
            AddMetadatum(new FeedMetadatum(this.Id, mediaType, scheme, name, content));
        }

        public void RemoveMetadatum(IFeedMetadatum metadatum)
        {
            RemoveValue<IFeed, IFeedMetadatum>(() => metadata.Remove(metadatum), metadatum, x => x.Metadata);
        }

        public void AddItem(IFeedItem item)
        {
            AddChild<IFeed, IFeedItem>(() => items.Add(item), item, x => x.Items);
        }

        public void RemoveItem(IFeedItem item)
        {
            RemoveChild<IFeed, IFeedItem>(() => items.Remove(item), item, x => x.Items);
        }

        #endregion
    }
}
