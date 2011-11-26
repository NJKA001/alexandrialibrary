using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITag
    {
        long Id { get; }
        Uri Target { get; }
        IAlgorithm Algorithm { get; }
        ITagType Type { get; }
        string Value { get; }
        byte[] Data { get; }
    }
}
