using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, IAlgorithm algorithm, ITagSchema schema, ITagType type, string name)
            : this(target, algorithm, schema, type, name, 0)
        {
        }

        public Tag(Uri target, IAlgorithm algorithm, ITagSchema schema, ITagType type, string name, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            this.target = target;
            this.algorithm = algorithm;
            this.schema = schema;
            this.type = type;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
        private readonly IAlgorithm algorithm;
        private readonly ITagSchema schema;
        private readonly ITagType type;
        private readonly string name;
        
        #region ITag Members

        public long Id
        {
            get { return id; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IAlgorithm Algorithm
        {
            get { return algorithm; }
        }

        public ITagSchema Schema
        {
            get { return schema; }
        }

        public ITagType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("tag name='{0}' target='{1}'", name, target);
        }
    }
}
