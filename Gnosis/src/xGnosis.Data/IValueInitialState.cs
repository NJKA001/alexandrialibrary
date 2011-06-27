using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IValueInitialState
    {
        Guid Id { get; }
        Guid Parent { get; }
        uint Sequence { get; }
        bool IsNew { get; }

        void Initialize(string name, Action<object> action);
    }
}
