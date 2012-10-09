using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Games;
using LotR.Games.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class DwarvenAxe
        : AttachmentCardBase
    {
        public DwarvenAxe()
            : base("Dwarven Axe", CardSet.Core, 41, Sphere.Tactics, 2, false, true)
        {
            AddTrait(Trait.Item);
            AddTrait(Trait.Weapon);
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }
    }
}
