using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States.Areas
{
    public interface IPlayerArea
        : IArea, IInPlay
    {
        IPlayer Player { get; }
        IEnumerable<ICardInPlay<IAttachableCard>> PlayerDeckAttachments { get; }

        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IEnemyInPlay> EngagedEnemies { get; }

        void AddCard(ICardInPlay card);
        void RemoveCard(ICardInPlay card);

        void AddEngagedEnemy(IEnemyInPlay enemy);
        void RemoveEngagedEnemy(IEnemyInPlay enemy);
    }
}
