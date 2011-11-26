using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class TagType
    {
        static TagType()
        {
            all.Add(DefaultString);
            all.Add(DefaultPositiveInteger);
            all.Add(DefaultDate);
            all.Add(DefaultDuration);
            all.Add(DefaultByteArray);
            all.Add(AmericanizedString);
            all.Add(AmericanizedPositiveInteger);
            all.Add(AmericanizedDate);
            all.Add(AmericanizedDuration);
            all.Add(AmericanizedByteArray);
        }

        private static readonly IList<ITagType> all = new List<ITagType>();

        public static readonly ITagType DefaultString = new TagType<string>(1, "Default", TagSchema.Default, TagDomain.String);
        public static readonly ITagType DefaultPositiveInteger = new TagType<uint>(2, "DefaultPositiveInteger", TagSchema.Default, TagDomain.PositiveInteger);
        public static readonly ITagType DefaultDate = new TagType<DateTime>(3, "DefaultDate", TagSchema.Default, TagDomain.Date);
        public static readonly ITagType DefaultDuration = new TagType<TimeSpan>(4, "DefaultDuration", TagSchema.Default, TagDomain.Duration);
        public static readonly ITagType DefaultByteArray = new TagType<byte[]>(5, "DefaultByteArray", TagSchema.Default, TagDomain.ByteArray);

        public static readonly ITagType AmericanizedString = new TagType<string>(21, "Americanized", TagSchema.Americanized, TagDomain.String);
        public static readonly ITagType AmericanizedPositiveInteger = new TagType<uint>(22, "AmericanizedPositiveInteger", TagSchema.Americanized, TagDomain.PositiveInteger);
        public static readonly ITagType AmericanizedDate = new TagType<DateTime>(23, "AmericanizedDate", TagSchema.Americanized, TagDomain.Date);
        public static readonly ITagType AmericanizedDuration = new TagType<TimeSpan>(24, "AmericanizedDuration", TagSchema.Americanized, TagDomain.Duration);
        public static readonly ITagType AmericanizedByteArray = new TagType<byte[]>(25, "AmericanizedByteArray", TagSchema.Americanized, TagDomain.ByteArray);

        public static IEnumerable<ITagType> GetAllDefault()
        {
            return all;
        }
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
