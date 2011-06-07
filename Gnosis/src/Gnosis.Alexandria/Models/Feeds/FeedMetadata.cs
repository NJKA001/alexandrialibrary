using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedMetadata
        : ValueBase, IFeedMetadata
    {
        public FeedMetadata(Guid parent, uint sequence, string mediaType, Uri scheme, string name, string content)
            : base(parent, sequence)
        {
            this.mediaType = mediaType;
            this.scheme = scheme;
            this.name = name;
            this.content = content;
        }

        public FeedMetadata(Guid id, Guid parent, uint sequence, string mediaType, Uri scheme, string name, string content)
            : base(id, parent, sequence)
        {
            this.mediaType = mediaType;
            this.scheme = scheme;
            this.name = name;
            this.content = content;
        }

        private readonly string mediaType;
        private readonly Uri scheme;
        private readonly string name;
        private readonly string content;

        #region IFeedMetadata Members

        public string MediaType
        {
            get { return mediaType; }
        }

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Content
        {
            get { return content; }
        }

        #endregion
    }
}
