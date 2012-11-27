using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.Effects;

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

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                IAttachmentInPlay attachment = null;

                foreach (var player in game.Players)
                {
                    attachment = player.CardsInPlay.OfType<IAttachmentInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                    if (attachment != null)
                        continue;
                }

                if (attachment == null || attachment.AttachedTo == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    { handle.Cancel(GetCancelledString()); return; }

                resourceful.Resources += 1;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
