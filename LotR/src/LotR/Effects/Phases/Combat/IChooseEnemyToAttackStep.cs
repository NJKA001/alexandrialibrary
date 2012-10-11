using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter.Enemies;
using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface IChooseEnemyToAttackStep
    {
        IEnumerable<IAttackingCard> Attackers { get; }
        IEnumerable<ICardInPlay<IEnemyCard>> Enemies { get; }
        ICardInPlay<IEnemyCard> Choice { get; set; }

        void AddEnemy(ICardInPlay<IEnemyCard> enemy);
        void RemoveEnemy(ICardInPlay<IEnemyCard> enemy);
    }
}
