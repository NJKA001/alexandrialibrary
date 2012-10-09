using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Games;
using LotR.Games.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class ProtectorOfLorien
        : AttachmentCardBase
    {
        public ProtectorOfLorien()
            : base("Protector of Lorien", CardSet.Core, 70, Sphere.Lore, 1, false, false)
        {
            AddTrait(Trait.Title);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
