using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Culture;

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
            AddInitializer(value => this.language = value.ToLanguageTag(), feed => feed.Language);
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
            AddValueInitializer(value => AddTitleTag(value as ITag), x => x.TitleTags);
            AddValueInitializer(value => AddAuthorTag(value as ITag), x => x.AuthorTags);
            AddValueInitializer(value => AddContributorTag(value as ITag), x => x.ContributorTags);
            AddValueInitializer(value => AddDescriptionTag(value as ITag), x => x.DescriptionTags);

            AddHashFunction(Tag.SchemeDoubleMetaphone, token => Tag.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(Tag.SchemeAmericanizedGraph, token => Tag.CreateAmericanizedGraph(this.Id, token));

            AddHashInitializer(tag => AddTitleTag(tag), tag => RemoveTitleTag(tag), feed => feed.TitleTags);
            AddHashInitializer(tag => AddAuthorTag(tag), tag => RemoveAuthorTag(tag), feed => feed.AuthorTags);
            AddHashInitializer(tag => AddContributorTag(tag), tag => RemoveContributorTag(tag), feed => feed.ContributorTags);
            AddHashInitializer(tag => AddDescriptionTag(tag), tag => RemoveDescriptionTag(tag), feed => feed.DescriptionTags);
        }

        private Uri location;
        private string mediaType = "application/rss+xml";
        private string title = string.Empty;
        private string authors = string.Empty;
        private string contributors = string.Empty;
        private string description = string.Empty;
        private ILanguageTag language = LanguageTag.Empty;
        private Uri originalLocation = Guid.Empty.ToUrn();
        private string copyright = string.Empty;
        private DateTime publishedDate = DateTime.MinValue;
        private DateTime updatedDate = DateTime.MinValue;
        private string generator = string.Empty;
        private Uri imagePath = Guid.Empty.ToUrn();
        private Uri iconPath = Guid.Empty.ToUrn();
        private string feedIdentifier = string.Empty;

        private readonly IList<IFeedCategory> categories = new ObservableCollection<IFeedCategory>();
        private readonly IList<IFeedLink> links = new ObservableCollection<IFeedLink>();
        private readonly IList<IFeedMetadatum> metadata = new ObservableCollection<IFeedMetadatum>();
        private readonly IList<IFeedItem> items = new ObservableCollection<IFeedItem>();

        private readonly IList<ITag> titleTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> authorTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> contributorTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> descriptionTags = new ObservableCollection<ITag>();

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

        private void AddTitleTag(ITag tag)
        {
            AddValue<ITag>(() => titleTags.Add(tag), tag, x => x.TitleTags);
        }

        private void RemoveTitleTag(ITag tag)
        {
            RemoveValue<ITag>(() => titleTags.Remove(tag), tag, x => x.TitleTags);
        }

        private void AddAuthorTag(ITag tag)
        {
            AddValue<ITag>(() => authorTags.Add(tag), tag, x => x.AuthorTags);
        }

        private void RemoveAuthorTag(ITag tag)
        {
            RemoveValue<ITag>(() => authorTags.Remove(tag), tag, x => x.AuthorTags);
        }

        private void AddContributorTag(ITag tag)
        {
            AddValue<ITag>(() => contributorTags.Add(tag), tag, x => x.ContributorTags);
        }

        private void RemoveContributorTag(ITag tag)
        {
            RemoveValue<ITag>(() => contributorTags.Remove(tag), tag, x => x.ContributorTags);
        }

        private void AddDescriptionTag(ITag tag)
        {
            AddValue<ITag>(() => descriptionTags.Add(tag), tag, x => x.DescriptionTags);
        }

        private void RemoveDescriptionTag(ITag tag)
        {
            RemoveValue<ITag>(() => descriptionTags.Remove(tag), tag, x => x.DescriptionTags);
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
                    RefreshTags(value, x => x.TitleTags);
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
                    RefreshTags(value, x => x.AuthorTags);
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
                    RefreshTags(value, x => x.ContributorTags);
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
                    RefreshTags(value, x => x.DescriptionTags);
                }
            }
        }

        public ILanguageTag Language
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


        public IEnumerable<ITag> TitleTags
        {
            get { return titleTags; }
        }

        public IEnumerable<ITag> AuthorTags
        {
            get { return authorTags; }
        }

        public IEnumerable<ITag> ContributorTags
        {
            get { return contributorTags; }
        }

        public IEnumerable<ITag> DescriptionTags
        {
            get { return descriptionTags; }
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
