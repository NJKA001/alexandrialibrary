using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedLink
        : ValueBase, IFeedLink
    {
        public FeedLink()
        {
            AddInitializer("Relationship", x => this.relationship = x.ToString());
            AddInitializer("Location", x => this.location = x.ToUri());
            AddInitializer("MediaType", x => this.mediaType = x.ToString());
            AddInitializer("Length", x => this.length = x.ToUInt32());
            AddInitializer("Language", x => this.language = x.ToString());
        }

        public FeedLink(Guid parent, string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddInitializer("Relationship", x => this.relationship = relationship);
            AddInitializer("Location", x => this.location = location);
            AddInitializer("MediaType", x => this.mediaType = mediaType);
            AddInitializer("Length", x => this.length = length);
            AddInitializer("Language", x => this.language = language);

            Initialize(parent);
        }

        private string relationship;
        private Uri location;
        private string mediaType;
        private uint length;
        private string language;

        #region IFeedLink Members

        public string Relationship
        {
            get { return relationship; }
        }

        public Uri Location
        {
            get { return location; }
        }

        public string MediaType
        {
            get { return mediaType; }
        }

        public uint Length
        {
            get { return length; }
        }

        public string Language
        {
            get { return language; }
        }

        #endregion
    }
}
