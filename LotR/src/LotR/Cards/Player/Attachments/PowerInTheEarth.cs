using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class PowerInTheEarth
        : AttachmentCardBase
    {
        public PowerInTheEarth()
            : base("Power in the Earth", CardSet.Core, 56, Sphere.Spirit, 1)
        {
            AddTrait(Trait.Condition);
        }

        public override bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is ILocationCard);
        }
    }
}
