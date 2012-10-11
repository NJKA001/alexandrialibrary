using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;

namespace LotR.States
{
    public interface IEnemyInPlay
        : ICardInPlay<IEnemyCard>
    {
    }
}
