using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, IAlgorithm algorithm, ITagType type, object value)
            : this(target, algorithm, type, value, 0)
        {
        }

        public Tag(Uri target, IAlgorithm algorithm, ITagType type, object value, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");

            this.target = target;
            this.algorithm = algorithm;
            this.type = type;
            this.value = value;
            this.name = type.Domain.GetName(value);
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
        private readonly IAlgorithm algorithm;
        private readonly ITagType type;
        private readonly object value;
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

        public ITagType Type
        {
            get { return type; }
        }

        public object Value
        {
            get { return value; }
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
