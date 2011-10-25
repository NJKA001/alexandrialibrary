using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagType
    {
        int Id { get; }
        string Name { get; }
        ITagSchema Schema { get; }
        ITagDomain Domain { get; }
    }

    public interface ITagType<T>
        : ITagType
    {
    }
}
