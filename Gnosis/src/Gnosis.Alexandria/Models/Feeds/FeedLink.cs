using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedLink
        : ValueBase, IFeedLink
    {
        public FeedLink(Guid id, Guid parent, uint sequence, string relationship, Uri location, string mediaType, uint length, string language)
            : base(id, parent, sequence)
        {
            this.relationship = relationship;
            this.location = location;
            this.mediaType = mediaType;
            this.length = length;
            this.language = language;
        }

        private readonly string relationship;
        private readonly Uri location;
        private readonly string mediaType;
        private readonly uint length;
        private readonly string language;

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
