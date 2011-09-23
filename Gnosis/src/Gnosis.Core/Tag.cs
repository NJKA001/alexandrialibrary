using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tag
        : ITag
    {
        public Tag(Uri target, ITagType type, string name)
            : this(target, type, name, 0)
        {
        }

        public Tag(Uri target, ITagType type, string name, long id)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            this.target = target;
            this.type = type;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri target;
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

        public ITagType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}
