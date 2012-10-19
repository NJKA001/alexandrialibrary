using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public class HornOfGondor
        : HeroAttachmentCardBase
    {
        public HornOfGondor()
            : base("Horn of Gondor", CardSet.Core, 42, Sphere.Tactics, 1)
        {
            IsUnique = true;
            IsRestricted = true;

            AddTrait(Trait.Item);
            AddTrait(Trait.Artifact);
        }

        public class AddOneResourceWhenACharacterLeavesPlay
            : ResponseCharacterAbilityBase, IAfterCardLeavesPlay
        {
            public AddOneResourceWhenACharacterLeavesPlay(HornOfGondor source)
                : base("After a character leaves play, add 1 resource to attached hero's pool.", source)
            {
            }

            public void AfterCardLeavesPlay(ICardLeavesPlay state)
            {
                if (!(state.LeavingPlay is ICharacterInPlay))
                    return;

                state.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var attachment = game.GetState<IAttachmentInPlay>(Source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return;

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    return;

                resourceful.Resources += 1;
            }
        }
    }
}
