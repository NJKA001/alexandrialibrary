using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITagTypeFactory
    {
        ITagType Create(int id);

        void Add(ITagType type);
        void Remove(ITagType type);
    }
}
