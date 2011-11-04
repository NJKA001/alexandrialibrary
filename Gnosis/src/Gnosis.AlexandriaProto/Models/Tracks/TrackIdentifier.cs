using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackIdentifier
        : ValueBase<ITrackIdentifier>, ITrackIdentifier
    {
        public TrackIdentifier()
        {
            AddInitializer(value => this.scheme = value.ToUri(), id => id.Scheme);
            AddInitializer(value => this.identifier = value.ToString(), id => id.Identifier);
        }

        public TrackIdentifier(Guid parent, Uri scheme, string identifier)
        {
            AddInitializer(value => this.scheme = scheme, id => id.Scheme);
            AddInitializer(value => this.identifier = identifier, id => id.Identifier);
            
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
