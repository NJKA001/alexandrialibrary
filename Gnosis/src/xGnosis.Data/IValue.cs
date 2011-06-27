using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Gnosis.Data
{
    public interface IValue
    {
        Guid Id { get; }
        Guid Parent { get; }
        uint Sequence { get; }

        bool IsInitialized();
        bool IsNew();
        bool IsMoved();
        bool IsRemoved();

        void Initialize(IValueInitialState state);
        void Move(uint sequence);
        void Remove();
        void Save();
    }
}
