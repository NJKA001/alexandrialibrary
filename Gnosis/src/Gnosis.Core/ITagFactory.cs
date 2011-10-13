using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagFactory
    {
        ITag Create(Uri target, ITagType type, object value);
        ITag Create(Uri target, ITagType type, object value, long id);
        ITag Create<T>(Uri target, ITagType<T> type, T value);
        ITag Create<T>(Uri target, ITagType<T> type, T value, long id);
    }
}
