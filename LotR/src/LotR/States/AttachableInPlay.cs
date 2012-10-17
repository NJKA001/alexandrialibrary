using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public class AttachableInPlay
        : CardInPlay<IAttachableCard>, IAttachableInPlay
    {
        public AttachableInPlay(IGame gameState, IAttachableCard card, IAttachmentHostInPlay attachedTo)
            : base(gameState, card)
        {
            this.AttachedTo = attachedTo;
        }

        public IAttachmentHostInPlay AttachedTo
        {
            get;
            private set;
        }
    }
}
