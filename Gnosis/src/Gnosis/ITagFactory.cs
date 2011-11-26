using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagFactory
    {
        ITag Create(Uri target, IAlgorithm algorithm, ITagType type, string value, byte[] data);
        ITag Create(Uri target, IAlgorithm algorithm, ITagType type, string value, byte[] data, long id);
    }
}
