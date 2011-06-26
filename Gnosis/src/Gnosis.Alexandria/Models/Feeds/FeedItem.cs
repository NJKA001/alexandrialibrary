using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedItem
        : ChildBase<IFeed, IFeedItem>, IFeedItem
    {
        public FeedItem()
        {
            AddInitializer(x => this.title = x.ToString(), x => x.Title);
            AddInitializer(x => this.titleMediaType = x.ToString(), x => x.TitleMediaType);
            AddInitializer(x => this.authors = x.ToString(), x => x.Authors);
            AddInitializer(x => this.contributors = x.ToString(), x => x.Contributors);
            AddInitializer(x => this.publishedDate = x.ToDateTime(), x => x.PublishedDate);
            AddInitializer(x => this.copyright = x.ToString(), x => x.Copyright);
            AddInitializer(x => this.summary = x.ToString(), x => x.Summary);
            AddInitializer(x => this.content = x.ToString(), x => x.Content);
            AddInitializer(x => this.contentMediaType = x.ToString(), x => x.ContentMediaType);
            AddInitializer(x => this.contentLocation = x.ToUri(), x => x.ContentLocation);
            AddInitializer(x => this.updatedDate = x.ToDateTime(), x => x.UpdatedDate);
            AddInitializer(x => this.feedItemIdentifier = x.ToString(), x => x.FeedItemIdentifier);
            
            AddValueInitializer(value => this.AddCategory(value as IFeedCategory), x => x.Categories);
            AddValueInitializer(value => this.AddLink(value as IFeedLink), x => x.Links);
            AddValueInitializer(value => this.AddMetadatum(value as IFeedMetadatum), x => x.Metadata);
            AddValueInitializer(value => AddTitleTag(value as ITag), x => x.TitleTags);
            AddValueInitializer(value => AddAuthorTag(value as ITag), x => x.AuthorTags);
            AddValueInitializer(value => AddContributorTag(value as ITag), x => x.ContributorTags);
            AddValueInitializer(value => AddSummaryTag(value as ITag), x => x.SummaryTags);

            AddHashFunction(Tag.SchemeDoubleMetaphone, token => Tag.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(Tag.SchemeAmericanizedGraph, token => Tag.CreateAmericanizedGraph(this.Id, token));

            AddHashInitializer(tag => AddTitleTag(tag), tag => RemoveTitleTag(tag), feedItem => feedItem.TitleTags);
            AddHashInitializer(tag => AddAuthorTag(tag), tag => RemoveAuthorTag(tag), feedItem => feedItem.AuthorTags);
            AddHashInitializer(tag => AddContributorTag(tag), tag => RemoveContributorTag(tag), feedItem => feedItem.ContributorTags);
            AddHashInitializer(tag => AddSummaryTag(tag), tag => RemoveSummaryTag(tag), feedItem => feedItem.SummaryTags);
        }

        private string title = string.Empty;
        private string titleMediaType = "text/plain";
        private string authors = string.Empty;
        private string contributors = string.Empty;
        private DateTime publishedDate = new DateTime(2000, 1, 1);
        private string copyright = string.Empty;
        private string summary = string.Empty;
        private string content = string.Empty;
        private string contentMediaType = "text/plain";
        private Uri contentLocation = UriExtensions.EmptyUri;
        private DateTime updatedDate = new DateTime(2000, 1, 1);
        private string feedItemIdentifier = string.Empty;

        private readonly IList<IFeedCategory> categories = new ObservableCollection<IFeedCategory>();
        private readonly IList<IFeedLink> links = new ObservableCollection<IFeedLink>();
        private readonly IList<IFeedMetadatum> metadata = new ObservableCollection<IFeedMetadatum>();

        private readonly IList<ITag> titleTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> authorTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> contributorTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> summaryTags = new ObservableCollection<ITag>();

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

        private void AddSummaryTag(ITag tag)
        {
            AddValue<ITag>(() => summaryTags.Add(tag), tag, x => x.SummaryTags);
        }

        private void RemoveSummaryTag(ITag tag)
        {
            RemoveValue<ITag>(() => summaryTags.Remove(tag), tag, x => x.SummaryTags);
        }

        #endregion

        #region IFeedItem Members

        public string Title
        {
            get { return title; }
            set
            {
                if (value != null && title != value)
                {
                    Change(() => title = value, x => x.Title);
                    RefreshTags(value, feed => feed.TitleTags);
                }
            }
        }

        public string TitleMediaType
        {
            get { return titleMediaType; }
            set
            {
                if (value != null && titleMediaType != value)
                {
                    Change(() => titleMediaType = value, x => x.TitleMediaType);
                }
            }
        }

        public string Authors
        {
            get { return authors; }
            set
            {
                if (value != null && authors != value)
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
                if (value != null && contributors != value)
                {
                    Change(() => contributors = value, x => x.Contributors);
                    RefreshTags(value, feedItem => feedItem.ContributorTags);
                }
            }
        }

        public DateTime PublishedDate
        {
            get { return publishedDate; }
            set
            {
                if (publishedDate != value)
                {
                    Change(() => publishedDate = value, x => x.PublishedDate);
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

        public string Summary
        {
            get { return summary; }
            set
            {
                if (value != null && value != summary)
                {
                    Change(() => summary = value, x => x.Summary);
                    RefreshTags(value, feedItem => feedItem.SummaryTags);
                }
            }
        }

        public string Content
        {
            get { return content; }
            set
            {

                if (value != null && value != content)
                {
                    Change(() => content = value, x => x.Content);
                }
            }
        }

        public string ContentMediaType
        {
            get { return contentMediaType; }
            set
            {
                if (value != null && value != contentMediaType)
                {
                    Change(() => contentMediaType = value, x => x.ContentMediaType);
                }
            }
        }

        public Uri ContentLocation
        {
            get { return contentLocation; }
            set
            {
                if (value != null && value != contentLocation)
                {
                    Change(() => contentLocation = value, x => x.ContentLocation);
                }
            }
        }

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != updatedDate)
                {
                    Change(() => updatedDate = value, x => x.UpdatedDate);
                }
            }
        }

        public string FeedItemIdentifier
        {
            get { return feedItemIdentifier; }
            set
            {
                if (value != null && value != feedItemIdentifier)
                {
                    Change(() => feedItemIdentifier = value, x => x.FeedItemIdentifier);
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

        public IEnumerable<ITag> SummaryTags
        {
            get { return summaryTags; }
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

        #endregion
    }
}
