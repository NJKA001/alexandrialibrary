using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
    {
        public static readonly ITagType Default = new TagType<string>(1, "Default", Algorithm.Default, TagSchema.Default, TagDomain.String);
    }

    public class TagType<T>
        : ITagType<T>
    {
        public TagType(int id, string name, IAlgorithm algorithm, ITagSchema schema, ITagDomain domain)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (domain == null)
                throw new ArgumentNullException("domain");

            this.id = id;
            this.name = name;
            this.algorithm = algorithm;
            this.schema = schema;
            this.domain = domain;
        }

        private readonly int id;
        private readonly string name;
        private readonly IAlgorithm algorithm;
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

        public IAlgorithm Algorithm
        {
            get { return algorithm; }
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
