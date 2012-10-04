using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Attachments
{
    public class HornOfGondor
        : AttachmentCardBase
    {
        public HornOfGondor()
            : base("Horn of Gondor", SetNames.Core, 42, Sphere.Tactics, 1, true, true)
        {
            Trait(Traits.Item);
            Trait(Traits.Artifact);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
