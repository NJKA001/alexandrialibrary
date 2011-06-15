using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackLink
        : ValueBase, ITrackLink
    {
        public TrackLink()
        {
            AddInitializer("TextEncoding", value => this.textEncoding = value.ToString());
            AddInitializer("Relationship", value => this.relationship = value.ToString());
            AddInitializer("Location", value => this.location = value.ToUri());
        }

        public TrackLink(Guid parent, string textEncoding, string relationship, Uri location)
        {
            AddInitializer("TextEncoding", value => this.textEncoding = textEncoding);
            AddInitializer("Relationship", value => this.relationship = relationship);
            AddInitializer("Location", value => this.location = location);

            Initialize(parent);
        }

        private string textEncoding;
        private string relationship;
        private Uri location;

        #region ITrackLink Members

        public string TextEncoding
        {
            get { return textEncoding; }
        }

        public string Relationship
        {
            get { return relationship; }
        }

        public Uri Location
        {
            get { return location; }
        }

        #endregion
    }
}
