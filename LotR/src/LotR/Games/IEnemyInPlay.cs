using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;

namespace LotR.Games
{
    public interface IEnemyInPlay
        : ICardInPlay, IDamageableInPlay
    {
        new IEnemyCard Card { get; }
    }
}
