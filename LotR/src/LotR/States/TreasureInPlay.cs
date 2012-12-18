using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
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

        public IPlayerCard PlayerCard
        {
            get { return Card as IPlayerCard; }
        }

        ITreasureCard ICardInPlay<ITreasureCard>.Card
        {
            get { return Card as ITreasureCard; }
        }
    }
}
