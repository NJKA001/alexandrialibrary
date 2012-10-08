using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Attachments
{
    public class SelfPreservation
        : AttachmentCardBase
    {
        public SelfPreservation()
            : base("Self Preservation", CardSet.Core, 72, Sphere.Lore, 3, false, false)
        {
            AddTrait(Trait.Skill);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
