using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedMetadatum
        : ValueBase<IFeedMetadatum>, IFeedMetadatum
    {
        public FeedMetadatum()
        {
            AddInitializer(value => this.mediaType = value.ToString(), meta => meta.MediaType);
            AddInitializer(value => this.scheme = value.ToUri(), meta => meta.Scheme);
            AddInitializer(value => this.name = value.ToString(), meta => meta.Name);
            AddInitializer(value => this.content = value.ToString(), meta => meta.Content);
        }

        public FeedMetadatum(Guid parent, string mediaType, Uri scheme, string name, string content)
        {
            AddInitializer(value => this.mediaType = mediaType, meta => meta.MediaType);
            AddInitializer(value => this.scheme = scheme, meta => meta.Scheme);
            AddInitializer(value => this.name = name, meta => meta.Name);
            AddInitializer(value => this.content = content, meta => meta.Content);

            Initialize(parent);
        }

        private string mediaType;
        private Uri scheme;
        private string name;
        private string content;

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
