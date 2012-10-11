using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards
{
    public interface IAttackingCard
        : ICard
    {
        void DetermineAttack(IDetermineAttack state);
    }
}
