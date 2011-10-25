using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILinkTypeFactory
    {
        ILinkType Create(int id);

        void Add(ILinkType type);
        void Remove(ILinkType type);
    }
}
