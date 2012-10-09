using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Games;
using LotR.Games.Phases;

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

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
