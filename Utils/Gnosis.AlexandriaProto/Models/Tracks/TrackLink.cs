using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackLink
        : ValueBase<ITrackLink>, ITrackLink
    {
        public TrackLink()
        {
            AddInitializer(value => this.textEncoding = value.ToEnum<TextEncoding>(), link => link.TextEncoding);
            AddInitializer(value => this.relationship = value.ToString(), link => link.Relationship);
            AddInitializer(value => this.location = value.ToUri(), link => link.Location);
        }

        public TrackLink(Guid parent, TextEncoding textEncoding, string relationship, Uri location)
        {
            AddInitializer(value => this.textEncoding = textEncoding, link => link.TextEncoding);
            AddInitializer(value => this.relationship = relationship, link => link.Relationship);
            AddInitializer(value => this.location = location, link => link.Location);

            Initialize(parent);
        }

        private TextEncoding textEncoding;
        private string relationship;
        private Uri location;

        #region ITrackLink Members

        public TextEncoding TextEncoding
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
