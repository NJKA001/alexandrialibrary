using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackLink
        : ValueBase, ITrackLink
    {
        public TrackLink()
        {
        }

        public TrackLink(Guid parent, string textEncoding, string relationship, Uri location)
        {
            AddInitializer("TextEncoding", x => this.textEncoding = textEncoding);
            AddInitializer("Relationship", x => this.relationship = relationship);
            AddInitializer("Location", x => this.location = location);

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
