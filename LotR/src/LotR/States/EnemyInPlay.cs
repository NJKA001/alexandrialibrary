using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;

namespace LotR.States
{
    public class EnemyInPlay
        : ThreateningInPlay, IEnemyInPlay, IThreateningInPlay
    {
        public EnemyInPlay(IGame game, IEnemyCard card)
            : base(game, card)
        {
        }

        private readonly IList<IShadowInPlay> shadowCards = new List<IShadowInPlay>();

        IEnemyCard ICardInPlay<IEnemyCard>.Card
        {
            get { return Card as IEnemyCard; }
        }

        public IEnumerable<IShadowInPlay> ShadowCards
        {
            get { return shadowCards; }
        }

        public void AddShadowCard(IShadowInPlay shadow)
        {
            if (shadow == null)
                throw new ArgumentNullException("shadow");

            if (shadowCards.Contains(shadow))
                return;

            shadowCards.Add(shadow);
        }

        public void RemoveShadowCard(IShadowInPlay shadow)
        {
            if (shadow == null)
                throw new ArgumentNullException("shadow");

            if (!shadowCards.Contains(shadow))
                return;

            shadowCards.Remove(shadow);
        }
    }
}
