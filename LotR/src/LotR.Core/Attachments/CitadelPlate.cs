using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Attachments
{
    public class CitadelPlate
        : AttachmentCardBase
    {
        public CitadelPlate()
            : base("Citadel Plate", SetNames.Core, 40, Sphere.Tactics, 4, false, true)
        {
            Trait(Traits.Item);
            Trait(Traits.Armor);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
