using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Attachments
{
    public class UnexpectedCourage
        : AttachmentCardBase
    {
        public UnexpectedCourage()
            : base("Unexpected Courage", SetNames.Core, 57, Sphere.Spirit, 2, false, false)
        {
            Trait(Traits.Condition);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
