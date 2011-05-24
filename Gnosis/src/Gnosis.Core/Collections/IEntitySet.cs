using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public interface IEntitySet<T>
        : ISet<T> where T : IEntity
    {
    }
}
