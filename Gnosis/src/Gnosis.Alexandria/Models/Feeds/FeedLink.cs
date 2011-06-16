using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedLink
        : ValueBase<IFeedLink>, IFeedLink
    {
        public FeedLink()
        {
            AddInitializer(value => this.relationship = value.ToString(), link => link.Relationship);
            AddInitializer(value => this.location = value.ToUri(), link => link.Location);
            AddInitializer(value => this.mediaType = value.ToString(), link => link.MediaType);
            AddInitializer(value => this.length = value.ToUInt32(), link => link.Length);
            AddInitializer(value => this.language = value.ToString(), link => link.Language);
        }

        public FeedLink(Guid parent, string relationship, Uri location, string mediaType, uint length, string language)
        {
            AddInitializer(value => this.relationship = relationship, link => link.Relationship);
            AddInitializer(value => this.location = location, link => link.Location);
            AddInitializer(value => this.mediaType = mediaType, link => link.MediaType);
            AddInitializer(value => this.length = length, link => link.Length);
            AddInitializer(value => this.language = language, link => link.Language);

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
