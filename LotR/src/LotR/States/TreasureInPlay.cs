using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Treasures;

namespace LotR.States
{
    public class TreasureInPlay
        : AttachableInPlay, ITreasureInPlay, IPlayerCardInPlay<ITreasureCard>
    {
        public TreasureInPlay(IGame game, ITreasureCard card, IAttachmentHostInPlay attachedTo)
            : base(game, card, attachedTo)
        {
        }

        ITreasureCard ICardInPlay<ITreasureCard>.Card
        {
            get { return Card as ITreasureCard; }
        }
    }
}
