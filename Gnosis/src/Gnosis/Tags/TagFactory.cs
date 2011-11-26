using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags
{
    public class TagFactory
        : ITagFactory
    {
        public ITag Create(Uri target, ITagType type, string name, uint number, object value)
        {
            return new Tag(target, type, name, number, value);
        }

        public ITag Create(Uri target, ITagType type, string name, uint number, object value, long id)
        {
            return new Tag(target, type, name, number, value, id);
        }

        //public ITag Create<T>(Uri target, ITagType<T> type, T value)
        //{
        //    return new Tag(target, type, value);
        //}

        //public ITag Create<T>(Uri target, ITagType<T> type, T value, long id)
        //{
        //    return new Tag(target, type, value, id);
        //}
    }
}
