using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEntityInitialState
    {
        Guid Id { get; }
        DateTime TimeStamp { get; }
        Guid Parent { get; }
        uint Sequence { get; }
        bool IsNew { get; }

        void Initialize(string name, Action<object> action);
        void Initialize(string name, Action<string, IDataRecord> action);
    }
}
