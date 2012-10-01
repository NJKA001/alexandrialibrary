using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Attachments
{
    public class TheFavorOfTheLady
        : AttachmentCardBase
    {
        public TheFavorOfTheLady()
            : base("The Favor of the Lady", SetNames.Core, 55, Sphere.Spirit, 2, false, false)
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
