using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Quest;
using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Player.Heroes
{
    public sealed class Aragorn
        : HeroCardBase
    {
        public Aragorn()
            : base("Aragorn", CardSet.Core, 1, Sphere.Leadership, 12, 2, 3, 2, 5)
        {
            AddTrait(Trait.Dunedain);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Ranger);

            AddEffect(new SentinelAbility(this));
        }

        public class ReadyAfterCommitingToQuest
            : ResponseCharacterAbilityBase, IAfterCommittingToQuest
        {
            public ReadyAfterCommitingToQuest(Aragorn source)
                : base("After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.", source)
            {
            }

            public void AfterCommittingToQuest(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return;

                var self = questPhase.GetAllCharactersCommittedToQuest().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (self == null)
                    return;

                game.AddEffect(this);
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var character = game.GetCardInPlay<ICharacterInPlay>(CardSource.Id);
                if (character == null)
                    return base.GetHandle(game);

                var cost = new PayResourcesFrom(source, character, 1, false);
                return new EffectHandle(this, cost);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var resourcePayment = handle.Payment as IResourcePayment;
                if (resourcePayment == null)
                {
                    handle.Reject();
                    return;
                }

                if (resourcePayment.Characters.Count() != 1)
                {
                    handle.Reject();
                    return;
                }

                var character = resourcePayment.Characters.First();
                if (character.Card.Id != source.Id)
                {
                    handle.Reject();
                    return;
                }

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 1)
                {
                    handle.Reject();
                    return;
                }

                if (character.Resources < 1)
                {
                    handle.Reject();
                    return;
                }

                character.Resources -= 1;

                handle.Accept();
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(source.Id);
                if (exhaustable == null)
                    { handle.Cancel(GetCancelledString()); return; }

                if (!exhaustable.IsExhausted)
                    { handle.Cancel(GetCancelledString()); return; }

                exhaustable.Ready();

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
