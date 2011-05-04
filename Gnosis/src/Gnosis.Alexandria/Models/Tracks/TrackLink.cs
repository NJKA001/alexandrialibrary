using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackLink
        : ValueBase, ITrackLink
    {
        public TrackLink(string textEncoding, string relationship, Uri location)
        {
            this.textEncoding = textEncoding;
            this.relationship = relationship;
            this.location = location;
        }

        private readonly string textEncoding;
        private readonly string relationship;
        private readonly Uri location;

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
    }
}
