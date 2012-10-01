using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Attachments
{
    public class PowerInTheEarth
        : AttachmentCardBase
    {
        public PowerInTheEarth()
            : base("Power in the Earth", SetNames.Core, 56, Sphere.Spirit, 1, false, false)
        {
            Trait(Traits.Condition);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is ILocationCard);
        }
    }
}
