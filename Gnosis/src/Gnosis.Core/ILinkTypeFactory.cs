using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILinkTypeFactory
    {
        ILinkType Create(long id);

        void Add(ILinkType type);
        void Remove(ILinkType type);
    }
}
