using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IOutline<T>
        where T : IEntity
    {
        Guid Id { get; }

        void Initialize(IDataRecord record);
    }
}
