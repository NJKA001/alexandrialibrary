using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public class StewardOfGondor
        : AttachmentCardBase
    {
        public StewardOfGondor()
            : base("Steward of Gondor", CardSet.Core, 26, Sphere.Leadership, 2, true, false)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Title);

            AddEffect(new AddGondorTrait(this));
            AddEffect(new ExhaustToAddTwoResources(this));
        }

        public override bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }

        public class AddGondorTrait
            : PassiveEffect, IDuringCheckForTrait
        {
            public AddGondorTrait(StewardOfGondor source)
                : base("Attached hero gains the Gondor trait.", source)
            {
            }

            public void DuringCheckForTrait(ICheckForTrait state)
            {
                if (state.Trait != Trait.Gondor)
                    return;

                var attachment = state.GetState<IAttachmentInPlay>(Source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                if (state.Target.Card.Id != attachment.AttachedTo.Card.Id)
                    return;

                state.HasTrait = true;
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

            public override ICost GetCost(IGameState state)
            {
                //var exhaustable = step.GetCardInPlay(self.Id) as ICardInPlay<IExhaustableCard>;
                //if (exhaustable == null)
                //    return null;

                return null; //new ExhaustSelf(exhaustable);
            }

            public override bool PaymentAccepted(IGameState state, IPayment payment)
            {
                if (payment == null)
                    return false;

                //var exhaustPayment = payment as IExhaustCardPayment;
                //if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                //    return false;

                //exhaustPayment.Exhaustable.Exhaust();

                return true;
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                //var attachment = step.GetCardInPlay(Source.Id) as ICardInPlay<IAttachmentCard>;
                //if (attachment == null || attachment.AttachedTo == null)
                //    return;

                //var resourceful = attachment.AttachedTo.Card as IResourcefulCard;
                //if (resourceful == null)
                //    return;

                //step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { resourceful.Id, 2 } }));
            }
        }
    }
}
