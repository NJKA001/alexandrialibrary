using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class UnexpectedCourage
        : AttachmentCardBase
    {
        public UnexpectedCourage()
            : base("Unexpected Courage", CardSet.Core, 57, Sphere.Spirit, 2, false, false)
        {
            AddTrait(Trait.Condition);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }
    }
}
