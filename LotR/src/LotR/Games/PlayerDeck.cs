using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.Games
{
    public class PlayerDeck
        : Deck<IPlayerCard>, IPlayerDeck
    {
        public PlayerDeck(string name, IEnumerable<IHeroCard> heroes, IEnumerable<IPlayerCard> cards)
            : base(cards)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (heroes == null)
                throw new ArgumentNullException("heroes");

            this.Name = name;
            this.Heroes = heroes;
        }

        public string Name
        {
            get;
            private set;
        }

        public IEnumerable<IHeroCard> Heroes
        {
            get;
            private set;
        }
    }
}
