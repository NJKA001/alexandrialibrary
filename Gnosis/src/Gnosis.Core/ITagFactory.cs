using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagFactory
    {
        ITag Create(Uri target, IAlgorithm algorithm, ITagType type, object value);
        ITag Create(Uri target, IAlgorithm algorithm, ITagType type, object value, long id);
    }
}
