using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Player.Heroes
{
    public class Denethor
        : HeroCardBase
    {
        public Denethor()
            : base("Denethor", CardSet.Core, 10, Sphere.Lore, 8, 1, 1, 3, 3)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Steward);

            AddEffect(new ExamineTopCardOfEncounterDeck(this));
        }

        public class ExamineTopCardOfEncounterDeck
            : ActionCharacterAbilityBase
        {
            public ExamineTopCardOfEncounterDeck(Denethor source)
                : base("Exhaust Denethor to look at the top card of the encounter deck. You may move that card to the bottom of the deck.", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return base.GetHandle(game);

                var cost = new ExhaustSelf(exhaustable);
                return new EffectHandle(this, null, cost);
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

                var topCard = game.StagingArea.EncounterDeck.GetFromTop(1).FirstOrDefault();
                if (topCard == null)
                {
                    handle.Reject();
                    return;
                }

                game.StagingArea.AddExaminedEncounterCards(new List<IEncounterCard> { topCard });

                handle.Accept();
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var topOfDeckChoice = handle.Choice as IChooseTopOrBottomOfDeck;
                if (topOfDeckChoice == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                if (game.StagingArea.ExaminedEncounterCards.Count() != 1)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var topCard = game.StagingArea.ExaminedEncounterCards.FirstOrDefault() as IEncounterCard;
                if (topCard == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                game.StagingArea.RemoveExaminedEncounterCards(new List<IEncounterCard> { topCard });

                if (topOfDeckChoice.TopOfDeck)
                {
                    game.StagingArea.EncounterDeck.PutOnTop(new List<IEncounterCard> { topCard });
                }
                else
                {
                    game.StagingArea.EncounterDeck.PutOnBottom(new List<IEncounterCard> { topCard });

                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
