using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class ProtectorOfLorien
        : AttachmentCardBase
    {
        public ProtectorOfLorien()
            : base("Protector of Lorien", CardSet.Core, 70, Sphere.Lore, 1)
        {
            AddTrait(Trait.Title);
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }
    }
}
