using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, IAlgorithm algorithm, ISchema schema, string name)
            : this(target, algorithm, schema, name, 0)
        {
        }

        public Tag(Uri target, IAlgorithm algorithm, ISchema schema, string name, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (name == null)
                throw new ArgumentNullException("name");

            this.target = target;
            this.algorithm = algorithm;
            this.schema = schema;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
        private readonly IAlgorithm algorithm;
        private readonly ISchema schema;
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

        public ISchema Schema
        {
            get { return schema; }
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
