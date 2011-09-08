using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITag
    {
        long Id { get; }
        string Name { get; }
        string NameSoundsLike { get; }
        string NameAmericanized { get; }
        ITagType Type { get; }
        Uri Target { get; }
    }
}
