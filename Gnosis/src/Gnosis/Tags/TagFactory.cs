using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class TagFactory
        : ITagFactory
    {
        public ITag Create(Uri target, IAlgorithm algorithm, ITagType type, string value, byte[] data)
        {
            return new Tag(target, type, value, algorithm, data);
        }

        public ITag Create(Uri target, IAlgorithm algorithm, ITagType type, string value, byte[] data, long id)
        {
            return new Tag(target, type, value, algorithm, data, id);
        }
    }
}
