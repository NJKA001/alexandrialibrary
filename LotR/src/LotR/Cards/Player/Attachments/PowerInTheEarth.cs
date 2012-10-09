using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;
using LotR.Games;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class PowerInTheEarth
        : AttachmentCardBase
    {
        public PowerInTheEarth()
            : base("Power in the Earth", CardSet.Core, 56, Sphere.Spirit, 1, false, false)
        {
            AddTrait(Trait.Condition);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is ILocationCard);
        }
    }
}
