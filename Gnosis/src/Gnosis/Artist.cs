using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class Artist
        : IArtist
    {
        public Artist(string name, DateTime activeFrom, DateTime activeTo, Uri thumbnail)
            : this(name, activeFrom, activeTo, thumbnail, Guid.NewGuid())
        {
        }

        public Artist(string name, DateTime activeFrom, DateTime activeTo, Uri thumbnail, Guid id)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.activeFrom = activeFrom;
            this.activeTo = activeTo;
            this.thumbnail = thumbnail;
            this.id = id;
        }

        private readonly Guid id;
        private readonly string name;
        private readonly DateTime activeFrom;
        private readonly DateTime activeTo;
        private readonly Uri thumbnail;

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime ActiveFrom
        {
            get { return activeFrom; }
        }

        public DateTime ActiveTo
        {
            get { return activeTo; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
        }
    }
}
