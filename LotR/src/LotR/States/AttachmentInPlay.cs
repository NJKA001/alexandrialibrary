using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Attachments;

namespace LotR.States
{
    public class AttachmentInPlay
        : AttachableInPlay, IAttachmentInPlay, IPlayerCardInPlay<IAttachmentCard>
    {
        public AttachmentInPlay(IGame game, IAttachmentCard card, IAttachmentHostInPlay attachedTo)
            : base(game, card, attachedTo)
        {
        }

        public IPlayerCard PlayerCard
        {
            get { return Card as IPlayerCard; }
        }

        IAttachmentCard ICardInPlay<IAttachmentCard>.Card
        {
            get { return Card as IAttachmentCard; }
        }
    }
}
