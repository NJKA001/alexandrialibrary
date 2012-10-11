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

        IEnumerable<ICardInPlay<ICard>> CardsInPlay { get; }
        IEnumerable<ICardInPlay<IEnemyCard>> EngagedEnemies { get; }

        void AddCard(ICardInPlay<ICard> card);
        void RemoveCard(ICardInHand<ICard> card);
        void AddEnemy(ICardInPlay<IEnemyCard> enemy);
        void RemoveEnemy(ICardInPlay<IEnemyCard> enemy);
    }
}
