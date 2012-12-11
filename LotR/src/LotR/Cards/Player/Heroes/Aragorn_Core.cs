using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
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

            private void PayOneResourceToReadyAragorn(IGame game, IEffectHandle handle, IPlayer controller)
            {
                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(source.Id);
                if (exhaustable == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                if (!exhaustable.IsExhausted)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                exhaustable.Ready();

                handle.Resolve(string.Format("{0} chose to pay 1 resource from '{0}' to ready him after committing to him to the quest", controller.Name, CardSource.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var character = game.GetCardInPlay<ICharacterInPlay>(CardSource.Id);
                if (character == null || character.Resources == 0)
                    return base.GetHandle(game);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder(string.Format("Pay 1 resource from his resource pool to ready '{0}' after commiting him to the quest", CardSource.Title), game, controller)
                        .Question(string.Format("{0}, do you want to pay 1 resource from his resource pool to ready '{0}'?", CardSource.Title))
                            .Answer("Yes, I want to ready him", controller, (source, handle, item) => PayOneResourceToReadyAragorn(source, handle, item))
                            .LastAnswer("No, I do not want to ready him", false, (source, handle, item) => handle.Cancel(string.Format("", controller.Name)));

                return new EffectHandle(this, builder.ToChoice());
            }

            //public override void Validate(IGame game, IEffectHandle handle)
            //{
            //    var resourcePayment = handle.Payment as IResourcePayment;
            //    if (resourcePayment == null)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    if (resourcePayment.Characters.Count() != 1)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    var character = resourcePayment.Characters.First();
            //    if (character.Card.Id != source.Id)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    if (resourcePayment.GetPaymentBy(character.Card.Id) != 1)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    if (character.Resources < 1)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    character.Resources -= 1;

            //    handle.Accept();
            //}
        }
    }
}
