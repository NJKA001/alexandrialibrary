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

            private void ExhaustAndAddTwoResources(IGame game, IEffectHandle handle, IPlayer player, IExhaustableInPlay exhaustable, ICharacterInPlay resourceful)
            {
                exhaustable.Exhaust();
                resourceful.Resources += 2;

                handle.Resolve(string.Format("{0} chose to exhaust '{1}' to add 2 resources to {2}'s resource pool", player.Name, CardSource.Title, resourceful.Title));
            }


            public override IEffectHandle GetHandle(IGame game)
            {
                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(CardSource.Id);
                if (exhaustable == null || exhaustable.IsExhausted)
                    return base.GetHandle(game);

                var controller = exhaustable.GetController(game);

                var attachment = game.GetCardInPlay<IAttachableInPlay>(CardSource.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return base.GetHandle(game);

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder(string.Format("You may exhaust '{0}' to add 2 resources to {1}'s resource pool", CardSource.Title, resourceful.Title), game, controller)
                        .Question(string.Format("{0}, do you want to exhaust '{0}'?", controller, CardSource.Title))
                            .Answer(string.Format("Yes, I want to exhaust '{0}' to add 2 resources to {1}'s resource pool", CardSource.Title, resourceful.Title), true, (source, handle, item) => ExhaustAndAddTwoResources(source, handle, controller, exhaustable, resourceful))
                            .LastAnswer(string.Format("No, I do not to exhaust '{0}' to add 2 resources to {1}'s resource pool", CardSource.Title, resourceful.Title), false, (source, handle, item) => handle.Cancel(string.Format("{0} chose not to exhaust '{1}' to add 2 resources to {2}'s resource pool", controller.Name, CardSource.Title, resourceful.Title)));
                
                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
