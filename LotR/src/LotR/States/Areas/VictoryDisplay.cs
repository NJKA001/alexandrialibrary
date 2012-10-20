using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public class VictoryDisplay
        : AreaBase, IVictoryDisplay
    {
        public VictoryDisplay(IGame game)
            : base(game)
        {
        }

        private readonly IList<ICard> cards = new List<ICard>();
        private readonly IList<ICard> outOfPlayCards = new List<ICard>();

        public IEnumerable<ICard> Cards
        {
            get { return cards; }
        }

        public IEnumerable<ICard> OutOfPlayCards
        {
            get { return outOfPlayCards; }
        }

        public void AddCard(ICard card)
        {
            
        }

        public void RemoveCard(ICard card)
        {
            
        }

        public void AddToCardToOutOfPlay(ICard card)
        {
        }

        public void RemoveCardFromOutOfPlay(ICard card)
        {
        }

        public byte GetTotalVictoryPoints()
        {
            var sum = cards.OfType<IVictoryCard>().Sum(x => x.VictoryPoints);
            return (sum > 255) ? (byte)255 : (byte)sum;
        }
    }
}
