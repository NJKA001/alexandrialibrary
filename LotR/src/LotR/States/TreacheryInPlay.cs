using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

using LotR.Cards.Encounter.Treacheries;

namespace LotR.States
{
    public class TreacheryInPlay
        : CardInPlay<ITreacheryCard>, IEncounterInPlay
    {
        public TreacheryInPlay(IGame game, ITreacheryCard card)
            : base(game, card)
        {
        }

        IEncounterCard ICardInPlay<IEncounterCard>.Card
        {
            get { return Card as IEncounterCard; }
        }
    }
}
