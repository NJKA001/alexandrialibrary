using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedItem
        : EntityBase, IFeedItem
    {
        public FeedItem(IContext context)
            : base(context)
        {
            this.categories = new List<IFeedCategory>();
            this.links = new List<IFeedLink>();
            this.metadata = new List<IFeedMetadata>();
        }

        public FeedItem(IContext context, Guid id, DateTime timeStamp, string title, string titleMediaType, string authors, string contributors, DateTime publishedDate, string copyright, string summary, string content, string contentMediaType, Uri contentLocation, DateTime updatedDate, string feedItemIdentifier)
            : base(context, id, timeStamp)
        {
            this.categories = new List<IFeedCategory>();
            this.links = new List<IFeedLink>();
            this.metadata = new List<IFeedMetadata>();
        }

        private string title;
        private string titleMediaType;
        private string authors;
        private string contributors;
        private DateTime publishedDate;
        private string copyright;
        private string summary;
        private string content;
        private string contentMediaType;
        private Uri contentLocation;
        private DateTime updatedDate;
        private string feedItemIdentifier;

        private readonly List<IFeedCategory> categories;
        private readonly List<IFeedLink> links;
        private readonly List<IFeedMetadata> metadata;

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

        #endregion
    }
}
