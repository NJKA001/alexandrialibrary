using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedMetadatum
        : ValueBase, IFeedMetadatum
    {
        public FeedMetadatum()
        {
            AddInitializer("MediaType", x => this.mediaType = x.ToString());
            AddInitializer("Scheme", x => this.scheme = x.ToUri());
            AddInitializer("Name", x => this.name = x.ToString());
            AddInitializer("Content", x => this.content = x.ToString());
        }

        public FeedMetadatum(Guid parent, string mediaType, Uri scheme, string name, string content)
        {
            AddInitializer("MediaType", x => this.mediaType = mediaType);
            AddInitializer("Scheme", x => this.scheme = scheme);
            AddInitializer("Name", x => this.name = name);
            AddInitializer("Content", x => this.content = content);

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
