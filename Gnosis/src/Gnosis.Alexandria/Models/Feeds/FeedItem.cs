using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedItem
        : ChildBase, IFeedItem
    {
        public FeedItem()
        {
            AddInitializer("Title", x => this.title = x.ToString());
            AddInitializer("TitleMediaType", x => this.titleMediaType = x.ToString());
            AddInitializer("Authors", x => this.authors = x.ToString());
            AddInitializer("Contributors", x => this.contributors = x.ToString());
            AddInitializer("PublishedDate", x => this.publishedDate = x.ToDateTime());
            AddInitializer("Copyright", x => this.copyright = x.ToString());
            AddInitializer("Summary", x => this.summary = x.ToString());
            AddInitializer("Content", x => this.content = x.ToString());
            AddInitializer("ContentMediaType", x => this.contentMediaType = x.ToString());
            AddInitializer("ContentLocation", x => this.contentLocation = x.ToUri());
            AddInitializer("UpdatedDate", x => this.updatedDate = x.ToDateTime());
            AddInitializer("FeedItemIdentifier", x => this.feedItemIdentifier = x.ToString());
            AddValueInitializer("FeedItem_Categories", value => this.AddCategory(value as IFeedCategory));
            AddValueInitializer("FeedItem_Links", value => this.AddLink(value as IFeedLink));
            AddValueInitializer("FeedItem_Metadata", value => this.AddMetadatum(value as IFeedMetadatum));
            AddValueInitializer("FeedItem_TitleHashCodes", value => AddTitleHashCode(value as IHashCode));
            AddValueInitializer("FeedItem_AuthorHashCodes", value => AddAuthorHashCode(value as IHashCode));
            AddValueInitializer("FeedItem_ContributorHashCodes", value => AddContributorHashCode(value as IHashCode));
            AddValueInitializer("FeedItem_SummaryHashCodes", value => AddSummaryHashCode(value as IHashCode));
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
            AddValue<IFeedItem, IFeedCategory>(() => categories.Add(category), category, x => x.Categories);
        }

        private void AddLink(IFeedLink link)
        {
            AddValue<IFeedItem, IFeedLink>(() => links.Add(link), link, x => x.Links);
        }

        private void AddMetadatum(IFeedMetadatum metadatum)
        {
            AddValue<IFeedItem, IFeedMetadatum>(() => metadata.Add(metadatum), metadatum, x => x.Metadata);
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue<IFeedItem, IHashCode>(() => titleHashCodes.Add(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeedItem, IHashCode>(() => titleHashCodes.Remove(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void AddAuthorHashCode(IHashCode hashCode)
        {
            AddValue<IFeedItem, IHashCode>(() => authorHashCodes.Add(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void RemoveAuthorHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeedItem, IHashCode>(() => authorHashCodes.Remove(hashCode), hashCode, x => x.AuthorHashCodes);
        }

        private void AddContributorHashCode(IHashCode hashCode)
        {
            AddValue<IFeedItem, IHashCode>(() => contributorHashCodes.Add(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void RemoveContributorHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeedItem, IHashCode>(() => contributorHashCodes.Remove(hashCode), hashCode, x => x.ContributorHashCodes);
        }

        private void AddSummaryHashCode(IHashCode hashCode)
        {
            AddValue<IFeedItem, IHashCode>(() => summaryHashCodes.Add(hashCode), hashCode, x => x.SummaryHashCodes);
        }

        private void RemoveSummaryHashCode(IHashCode hashCode)
        {
            RemoveValue<IFeedItem, IHashCode>(() => summaryHashCodes.Remove(hashCode), hashCode, x => x.SummaryHashCodes);
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
                    Change(() => title = value, "Title");
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
                    Change(() => titleMediaType = value, "TitleMediaType");
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
                    Change(() => authors = value, "Authors");
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
                    Change(() => contributors = value, "Contributors");
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
                    Change(() => publishedDate = value, "PublishedDate");
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

        public string Summary
        {
            get { return summary; }
            set
            {
                if (value != null && value != summary)
                {
                    Change(() => summary = value, "Summary");
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
                    Change(() => content = value, "Content");
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
                    Change(() => contentMediaType = value, "ContentMediaType");
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
                    Change(() => contentLocation = value, "ContentLocation");
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
                    Change(() => updatedDate = value, "UpdatedDate");
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
                    Change(() => feedItemIdentifier = value, "FeedItemIdentifier");
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
            RemoveValue<IFeedItem, IFeedCategory>(() => categories.Remove(category), category, x => x.Categories);
        }

        public void AddLink(string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddLink(new FeedLink(this.Id, relationship, location, mediaType, length, language));
        }

        public void RemoveLink(IFeedLink link)
        {
            RemoveValue<IFeedItem, IFeedLink>(() => links.Remove(link), link, x => x.Links);
        }

        public void AddMetadatum(string mediaType, Uri scheme, string name, string content)
        {
            AddMetadatum(new FeedMetadatum(this.Id, mediaType, scheme, name, content));
        }

        public void RemoveMetadatum(IFeedMetadatum metadatum)
        {
            RemoveValue<IFeedItem, IFeedMetadatum>(() => metadata.Remove(metadatum), metadatum, x => x.Metadata);
        }

        #endregion
    }
}
