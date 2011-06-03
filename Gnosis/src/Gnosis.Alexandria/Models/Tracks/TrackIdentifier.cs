using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackIdentifier
        : ValueBase, ITrackIdentifier
    {
        public TrackIdentifier(Guid id, Uri scheme, string identifier)
            : base(id)
        {
            this.scheme = scheme;
            this.identifier = identifier;
        }

        private readonly Uri scheme;
        private readonly string identifier;

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Identifier
        {
            get { return identifier; }
        }
    }
}
