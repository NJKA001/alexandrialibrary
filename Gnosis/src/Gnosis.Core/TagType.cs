using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
    {
        public static readonly ITagType Default = new TagType<string>(1, "Default", TagSchema.Default, TagDomain.String);
    }

    public class TagType<T>
        : ITagType<T>
    {
        public TagType(int id, string name, ITagSchema schema, ITagDomain domain)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (domain == null)
                throw new ArgumentNullException("domain");

            this.id = id;
            this.name = name;
            this.schema = schema;
            this.domain = domain;
        }

        private readonly int id;
        private readonly string name;
        private readonly ITagSchema schema;
        private readonly ITagDomain domain;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public ITagSchema Schema
        {
            get { return schema; }
        }

        public ITagDomain Domain
        {
            get { return domain; }
        }
    }
}
