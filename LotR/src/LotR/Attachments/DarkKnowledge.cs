using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Attachments
{
    public class DarkKnowledge
        : AttachmentCardBase
    {
        public DarkKnowledge()
            : base("Dark Knowledge", CardSet.Core, 71, Sphere.Lore, 1, false, false)
        {
            AddTrait(Trait.Condition);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
