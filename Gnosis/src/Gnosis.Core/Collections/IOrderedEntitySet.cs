using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public interface IOrderedEntitySet<T>
        : IOrderedSet<T> where T : IEntity
    {
    }
}
