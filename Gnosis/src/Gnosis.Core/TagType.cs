using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
        : ITagType
    {
        public TagType(long id, string name, ITagSchema schema)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (schema == null)
                throw new ArgumentNullException("schema");

            this.id = id;
            this.name = name;
            this.schema = schema;
        }

        private readonly long id;
        private readonly string name;
        private readonly ITagSchema schema;

        public long Id
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

        public static readonly ITagType Default = new TagType(1, "Default", TagSchema.Default);
    }
}
