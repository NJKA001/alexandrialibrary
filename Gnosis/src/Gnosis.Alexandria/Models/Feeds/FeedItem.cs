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
        public FeedItem(IContext context, Guid parent)
            : base(context, parent)
        {
        }

        public FeedItem(IContext context, Guid id, Guid parent, DateTime timeStamp, string title, string titleMediaType, string authors, string contributors, DateTime publishedDate, string copyright, string summary, string content, string contentMediaType, Uri contentLocation, DateTime updatedDate, string feedItemIdentifier)
            : base(context, id, timeStamp, parent)
        {
            this.title = title;
            this.titleMediaType = titleMediaType;
            this.authors = authors;
            this.contributors = contributors;
            this.publishedDate = publishedDate;
            this.copyright = copyright;
            this.summary = summary;
            this.content = content;
            this.contentMediaType = contentMediaType;
            this.contentLocation = contentLocation;
            this.updatedDate = updatedDate;
            this.feedItemIdentifier = feedItemIdentifier;
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
        private readonly IList<IFeedMetadata> metadata = new ObservableCollection<IFeedMetadata>();

        #region IFeedItem Members

        public string Title
        {
            get { return title; }
            set
            {
                if (value != null && title != value)
                {
                    OnEntityChanged(() => title = value, "Title");
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
                    OnEntityChanged(() => titleMediaType = value, "TitleMediaType");
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
                    OnEntityChanged(() => authors = value, "Authors");
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
                    OnEntityChanged(() => contributors = value, "Contributors");
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
                    OnEntityChanged(() => publishedDate = value, "PublishedDate");
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

        public string Summary
        {
            get { return summary; }
            set
            {
                if (value != null && value != summary)
                {
                    OnEntityChanged(() => summary = value, "Summary");
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
                    OnEntityChanged(() => content = value, "Content");
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
                    OnEntityChanged(() => contentMediaType = value, "ContentMediaType");
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
                    OnEntityChanged(() => contentLocation = value, "ContentLocation");
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
                    OnEntityChanged(() => updatedDate = value, "UpdatedDate");
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
                    OnEntityChanged(() => feedItemIdentifier = value, "FeedItemIdentifier");
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

        #endregion
    }
}
