using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class CitadelPlate
        : AttachmentCardBase
    {
        public CitadelPlate()
            : base("Citadel Plate", CardSet.Core, 40, Sphere.Tactics, 4, false, true)
        {
            AddTrait(Trait.Item);
            AddTrait(Trait.Armor);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }
    }
}
