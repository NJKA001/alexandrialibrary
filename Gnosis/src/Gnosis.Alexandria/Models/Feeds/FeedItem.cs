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
            AddValueInitializer(value => AddTitleHashCode(value as IHashCode), x => x.TitleHashCodes);
            AddValueInitializer(value => AddAuthorHashCode(value as IHashCode), x => x.AuthorHashCodes);
            AddValueInitializer(value => AddContributorHashCode(value as IHashCode), x => x.ContributorHashCodes);
            AddValueInitializer(value => AddSummaryHashCode(value as IHashCode), x => x.SummaryHashCodes);

            AddHashFunction(HashCode.SchemeDoubleMetaphone, token => HashCode.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(HashCode.SchemeNameHash, token => HashCode.CreateNameHash(this.Id, token));

            AddHashInitializer(hashCode => AddTitleHashCode(hashCode), hashCode => RemoveTitleHashCode(hashCode), feedItem => feedItem.TitleHashCodes);
            AddHashInitializer(hashCode => AddAuthorHashCode(hashCode), hashCode => RemoveAuthorHashCode(hashCode), feedItem => feedItem.AuthorHashCodes);
            AddHashInitializer(hashCode => AddContributorHashCode(hashCode), hashCode => RemoveContributorHashCode(hashCode), feedItem => feedItem.ContributorHashCodes);
            AddHashInitializer(hashCode => AddSummaryHashCode(hashCode), hashCode => RemoveSummaryHashCode(hashCode), feedItem => feedItem.SummaryHashCodes);
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

        private readonly IList<IHashCode> titleHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> authorHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> contributorHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> summaryHashCodes = new ObservableCollection<IHashCode>();

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

        private void AddSummaryHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => summaryHashCodes.Add(hashCode), hashCode, x => x.SummaryHashCodes);
        }

        private void RemoveSummaryHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => summaryHashCodes.Remove(hashCode), hashCode, x => x.SummaryHashCodes);
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
                    RefreshHashCodes(value, feed => feed.TitleHashCodes);
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
                    RefreshHashCodes(value, x => x.AuthorHashCodes);
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

        public IEnumerable<IHashCode> SummaryHashCodes
        {
            get { return summaryHashCodes; }
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
