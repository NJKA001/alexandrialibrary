using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagFactory
        : ITagFactory
    {
        #region ITagFactory Members

        public ITag Create(Uri target, IAlgorithm algorithm, ITagType type, object value)
        {
            return new Tag(target, algorithm, type, value);
        }

        public ITag Create(Uri target, IAlgorithm algorithm, ITagType type, object value, long id)
        {
            return new Tag(target, algorithm, type, value, id);
        }

        #endregion
    }
}
