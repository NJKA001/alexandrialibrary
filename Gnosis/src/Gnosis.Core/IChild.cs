using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IChild
        : IEntity
    {
        Guid Parent { get; }
        uint Sequence { get; }

        bool IsMoved();
        bool IsRemoved();

        void Move(uint sequence);
        void Remove();
    }
}
