using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class User
        : IUser
    {
        public User(IContentType type, string name, Uri thumbnail)
            : this(type, name, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public User(IContentType type, string name, Uri thumbnail, Uri location)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");
            if (location == null)
                throw new ArgumentNullException("location");

            this.type = type;
            this.name = name;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly IContentType type;
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

        public IContentType Type
        {
            get { return type; }
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

        public static IUser GetAdministrator(IContentTypeFactory contentTypeFactory)
        {
            return new User(contentTypeFactory.GetByCode("vnd.gnosis.user"), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
        }
    }
}
