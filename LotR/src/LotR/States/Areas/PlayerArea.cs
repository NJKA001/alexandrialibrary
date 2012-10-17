using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public class PlayerArea
        : AreaBase, IPlayerArea
    {
        public PlayerArea(IGame game, IPlayer player)
            : base(game)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            this.Player = player;

            foreach (var hero in Player.Deck.Heroes)
            {
                AddCard(new HeroInPlay(game, hero, player));
            }
        }

        private readonly IDictionary<Guid, IAttachableInPlay> playerDeckAttachments = new Dictionary<Guid, IAttachableInPlay>();
        private readonly IDictionary<Guid, IEnemyInPlay> engagedEnemies = new Dictionary<Guid, IEnemyInPlay>();

        public IPlayer Player
        {
            get;
            private set;
        }

        public IEnumerable<IAttachableInPlay> PlayerDeckAttachments
        {
            get { return playerDeckAttachments.Values; }
        }

        public IEnumerable<ICardInPlay> CardsInPlay
        {
            get { return GetStates<ICardInPlay>(); }
        }

        public IEnumerable<IEnemyInPlay> EngagedEnemies
        {
            get { return engagedEnemies.Values; }
        }

        public void AddPlayerDeckAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (playerDeckAttachments.ContainsKey(attachment.StateId))
                return;

            playerDeckAttachments.Add(attachment.StateId, attachment);
        }

        public void RemovePlayerDeckAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!playerDeckAttachments.ContainsKey(attachment.StateId))
                return;

            playerDeckAttachments.Remove(attachment.StateId);
        }

        public void AddCard(ICardInPlay card)
        {
            AddState(card);
        }

        public void RemoveCard(ICardInPlay card)
        {
            RemoveState(card);
        }

        public void AddEngagedEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (engagedEnemies.ContainsKey(enemy.StateId))
                return;

            engagedEnemies.Add(enemy.StateId, enemy);
        }

        public void RemoveEngagedEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (!engagedEnemies.ContainsKey(enemy.StateId))
                return;

            engagedEnemies.Remove(enemy.StateId);
        }

        public bool IsControlledByPlayer(ICardInPlay card)
        {
            return (GetState<ICardInPlay>(card.StateId) != null);
        }
    }
}
