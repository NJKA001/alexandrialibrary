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
        : IArea
    {
        IPlayer Player { get; }
        IEnumerable<IAttachableInPlay> PlayerDeckAttachments { get; }

        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IEnemyInPlay> EngagedEnemies { get; }

        void AddPlayerDeckAttachment(IAttachableInPlay attachment);
        void RemovePlayerDeckAttachment(IAttachableInPlay attachment);

        void AddCard(ICardInPlay card);
        void RemoveCard(ICardInPlay card);

        void AddEngagedEnemy(IEnemyInPlay enemy);
        void RemoveEngagedEnemy(IEnemyInPlay enemy);

        bool IsControlledByPlayer(ICardInPlay card);
    }
}
