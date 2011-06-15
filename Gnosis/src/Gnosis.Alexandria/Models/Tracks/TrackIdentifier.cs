using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackIdentifier
        : ValueBase, ITrackIdentifier
    {
        public TrackIdentifier()
        {
            AddInitializer("Scheme", value => this.scheme = value.ToUri());
            AddInitializer("Identifier", value => this.identifier = value.ToString());
        }

        public TrackIdentifier(Guid parent, Uri scheme, string identifier)
        {
            AddInitializer("Scheme", x => this.scheme = scheme);
            AddInitializer("Identifier", x => this.identifier = identifier);
            
            Initialize(parent);
        }

        private Uri scheme;
        private string identifier;

        #region ITrackIdentifier Members

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        #endregion
    }
}
