using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;

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
            : base("Steward of Gondor", CardSet.Core, 26, Sphere.Leadership, 2)
        {
            IsUnique = true;

            AddTrait(Trait.Gondor);
            AddTrait(Trait.Title);

            AddEffect(new AddGondorTrait(this));
            AddEffect(new ExhaustToAddTwoResources(this));
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }

        public class AddGondorTrait
            : PassiveCardEffectBase, IDuringCheckForTrait
        {
            public AddGondorTrait(StewardOfGondor source)
                : base("Attached hero gains the Gondor trait.", source)
            {
            }

            public void DuringCheckForTrait(ICheckForTrait check)
            {
                if (check.Trait != Trait.Gondor)
                    return;

                var attachment = check.Game.GetCardInPlay<IAttachmentInPlay>(source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                if (check.Target.StateId != attachment.AttachedTo.Card.Id)
                    return;

                check.HasTrait = true;
            }
        }

        public class ExhaustToAddTwoResources
            : ActionCardEffectBase
        {
            public ExhaustToAddTwoResources(StewardOfGondor source)
                : base("Exhaust Steward of Gondor to add 2 resources to attched hero's resource pool.", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(source.Id);
                if (exhaustable == null)
                    return base.GetHandle(game);

                var cost = new ExhaustSelf(exhaustable);
                return new EffectHandle(this, cost);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var exhaustPayment = handle.Payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                {
                    handle.Reject();
                    return;
                }

                exhaustPayment.Exhaustable.Exhaust();

                handle.Accept();
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var attachment = game.GetCardInPlay<IAttachmentInPlay>(source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                resourceful.Resources += 2;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
