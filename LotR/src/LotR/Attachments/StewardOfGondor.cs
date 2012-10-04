using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Costs;
using LotR.Effects;
using LotR.Payments;
using LotR.Phases.Any;

namespace LotR.Attachments
{
    public class StewardOfGondor
        : AttachmentCardBase
    {
        public StewardOfGondor()
            : base("Steward of Gondor", SetNames.Core, 26, Sphere.Leadership, 2, true, false)
        {
            Trait(Traits.Gondor);
            Trait(Traits.Title);

            Effect(new AddGondorTrait(this));
            Effect(new ExhaustToAddTwoResources(this));
        }

        public override bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay.Card is IHeroCard);
        }

        public class AddGondorTrait
            : PassiveEffect, ICheckForTrait
        {
            public AddGondorTrait(StewardOfGondor source)
                : base("Attached hero gains the Gondor trait.", source)
            {
            }

            public void CheckForTrait(ICheckForTraitStep step)
            {
                if (step.CardInPlay == null || step.Trait != Traits.Gondor)
                    return;

                var attachment = step.GetCardInPlay(Source.Id) as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                if (step.CardInPlay.CardId == attachment.AttachedTo.CardId)
                {
                    step.HasTrait = true;
                }
            }
        }

        public class ExhaustToAddTwoResources
            : ActionEffectBase
        {
            public ExhaustToAddTwoResources(StewardOfGondor source)
                : base("Exhaust Steward of Gondor to add 2 resources to attched hero's resource pool.", source)
            {
                this.self = source;
            }

            private readonly StewardOfGondor self;

            public override ICost GetCost(IPhaseStep step)
            {
                var exhaustable = step.GetCardInPlay(self.Id) as IExhaustableCard;
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                    return;

                var attachment = exhaustPayment.Exhaustable as IAttachmentInPlay;
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var resourceful = attachment.AttachedTo.Card as IResourcefulCard;
                if (resourceful == null)
                    return;

                step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { resourceful.Id, 2 } }));
            }
        }
    }
}
