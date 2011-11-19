using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisUser
        : IUser
    {
        public GnosisUser(string name, Uri thumbnail)
            : this(name, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisUser(string name, Uri thumbnail, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly string name;
        private readonly Uri thumbnail;
        private readonly Uri location;

        public string Name
        {
            get { return name; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
        }

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisUser; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public static readonly IUser Administrator = new GnosisUser("Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}
