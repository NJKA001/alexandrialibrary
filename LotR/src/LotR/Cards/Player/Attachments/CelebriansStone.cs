using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Games;
using LotR.Games.Phases;
using LotR.Games.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public class CelebriansStone
        : AttachmentCardBase
    {
        public CelebriansStone()
            : base("Celebrian's Stone", CardSet.Core, 27, Sphere.Leadership, 2, true, true)
        {
            AddTrait(Trait.Artifact);
            AddTrait(Trait.Item);

            AddEffect(new AddTwoWillpower(this));
            AddEffect(new AragornGetsASpiritResourceIcon(this));
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }

        public class AddTwoWillpower
            : PassiveEffect, IDetermineWillpower
        {
            public AddTwoWillpower(CelebriansStone source)
                : base("Attached hero gets +2 Willpower", source)
            {
            }

            public void DetermineWillpower(IDetermineWillpowerStep step)
            {
                var attachment = step.GetCardInPlay(Source.Id) as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var willpowerful = attachment.AttachedTo.Card as IWillpowerfulCard;
                if (willpowerful == null)
                    return;

                if (step.Source != null && step.Source.Id == willpowerful.Id)
                {
                    step.Willpower += 2;
                }
            }
        }

        public class AragornGetsASpiritResourceIcon
            : PassiveEffect, ICheckForResourceIcon
        {
            public AragornGetsASpiritResourceIcon(CelebriansStone source)
                : base("If attached hero is Aragorn, he also gains a Spirit resource icon.", source)
            {
            }

            public void CheckForResourceIcon(ICheckForResourceIconStep step)
            {
                var attachment = step.GetCardInPlay(Source.Id) as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var resourceful = attachment.AttachedTo.Card as IResourcefulCard;
                if (resourceful == null)
                    return;

                if (step.Source != null && step.Source.Id == resourceful.Id)
                {
                    if (step.ResourceIcon == Sphere.Spirit && resourceful.Title == "Aragorn")
                    {
                        step.HasResourceIcon = true;
                    }
                }
            }
        }
    }
}
