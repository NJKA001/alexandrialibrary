using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagTypeFactory
    {
        ITagType Create(long id);

        void Add(ITagType type);
        void Remove(ITagType type);
    }
}
