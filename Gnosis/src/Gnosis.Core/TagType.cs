using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
    {
        public static readonly ITagType DefaultString = new TagType<string>(1, "Default", TagSchema.Default, TagDomain.String);
        public static readonly ITagType DefaultStringArray = new TagType<string[]>(2, "DefaultStringArray", TagSchema.Default, TagDomain.StringArray);
        public static readonly ITagType DefaultPositiveInteger = new TagType<uint>(3, "DefaultPositiveInteger", TagSchema.Default, TagDomain.PositiveInteger);
        public static readonly ITagType DefaultDate = new TagType<DateTime>(4, "DefaultDate", TagSchema.Default, TagDomain.Date);
        public static readonly ITagType DefaultDuration = new TagType<TimeSpan>(5, "DefaultDuration", TagSchema.Default, TagDomain.Duration);
        public static readonly ITagType DefaultByteArray = new TagType<byte[]>(6, "DefaultByteArray", TagSchema.Default, TagDomain.ByteArray);
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
