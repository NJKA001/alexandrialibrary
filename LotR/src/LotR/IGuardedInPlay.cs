using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IGuardedInPlay
        : ICardInPlay
    {
        new IGuardedCard Card { get; }
        IEnumerable<ICardInPlay> Guards { get; }

        bool HasGuards { get; }
        void AddGuard(ICardInPlay guard);
        void RemoveGuard(ICardInPlay guard);
    }
}
