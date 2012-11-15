using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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

                var self = questPhase.GetAllCharactersCommittedToQuest().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (self == null)
                    return;

                game.AddEffect(this);
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var character = game.GetCardInPlay<ICharacterInPlay>(CardSource.Id);
                if (character == null)
                    return base.GetOptions(game);

                var cost = new PayResourcesFrom(Source, character, 1, false);
                return new EffectOptions(cost);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var resourcePayment = options.Payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Characters.Count() != 1)
                    return false;

                var character = resourcePayment.Characters.First();
                if (character.Card.Id != Source.Id)
                    return false;

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 1)
                    return false;

                if (character.Resources < 1)
                    return false;

                character.Resources -= 1;

                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(Source.Id);
                if (exhaustable == null)
                    return GetCancelledString();

                if (!exhaustable.IsExhausted)
                    return GetCancelledString();

                exhaustable.Ready();

                return ToString();
            }
        }
    }
}
