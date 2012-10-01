using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Attachments
{
    public class DwarvenAxe
        : AttachmentCardBase
    {
        public DwarvenAxe()
            : base("Dwarven Axe", SetNames.Core, 41, Sphere.Tactics, 2, false, true)
        {
            Trait(Traits.Item);
            Trait(Traits.Weapon);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
