using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public class CelebriansStone
        : AttachmentCardBase
    {
        public CelebriansStone()
            : base("Celebrian's Stone", CardSet.Core, 27, Sphere.Leadership, 2)
        {
            IsUnique = true;
            IsRestricted = true;

            AddTrait(Trait.Artifact);
            AddTrait(Trait.Item);

            AddEffect(new AddTwoWillpower(this));
            AddEffect(new AragornGetsASpiritResourceIcon(this));
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }

        public class AddTwoWillpower
            : PassiveEffect, IDuringDetermineWillpower
        {
            public AddTwoWillpower(CelebriansStone source)
                : base("Attached hero gets +2 Willpower", source)
            {
            }

            public void DuringDetermineWillpower(IDetermineWillpower state)
            {
                var attachment = state.GetState<IAttachmentInPlay>(Source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var willpowerful = attachment.AttachedTo.Card as IWillpowerfulCard;
                if (willpowerful == null)
                    return;

                if (state.Quester.Card.Id != willpowerful.Id)
                    return;

                state.Willpower += 2;
            }
        }

        public class AragornGetsASpiritResourceIcon
            : PassiveEffect, IDuringCheckForResourceIcon
        {
            public AragornGetsASpiritResourceIcon(CelebriansStone source)
                : base("If attached hero is Aragorn, he also gains a Spirit resource icon.", source)
            {
            }

            public void DuringCheckForResourceIcon(ICheckForResourceIcon state)
            {
                if (state.ResourceIcon != Sphere.Spirit)
                    return;

                var attachment = state.GetState<IAttachmentInPlay>(Source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var resourceful = attachment.AttachedTo.Card as IResourcefulCard;
                if (resourceful == null || resourceful.Title != "Aragorn")
                    return;

                if (state.Target.Card.Id != Source.Id)
                    return;

                state.HasResourceIcon = true;
            }
        }
    }
}
