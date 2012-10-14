using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class SelfPreservation
        : AttachmentCardBase
    {
        public SelfPreservation()
            : base("Self Preservation", CardSet.Core, 72, Sphere.Lore, 3)
        {
            AddTrait(Trait.Skill);
        }

        public override bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }
    }
}
