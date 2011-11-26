using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, ITagType type, string name, uint number, object value)
            : this(target, type, name, number, value, 0)
        {
        }

        public Tag(Uri target, ITagType type, string name, uint number, object value, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");

            this.target = target;
            this.type = type;
            this.name = name;
            this.number = number;
            this.value = value;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
        private readonly ITagType type;
        private readonly string name;
        private readonly uint number;
        private readonly object value;

        public long Id
        {
            get { return id; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public ITagType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }

        public uint Number
        {
            get { return number; }
        }

        public object Value
        {
            get { return value; }
        }
    }
}
