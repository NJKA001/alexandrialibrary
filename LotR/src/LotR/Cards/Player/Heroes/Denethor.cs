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

            public override ICost GetCost(IGameState state)
            {
                var exhaustable = state.GetState<IExhaustableInPlay>(Source.Id);
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public override bool PaymentAccepted(IGameState state, IPayment payment)
            {
                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return false;

                var topCard = stagingArea.EncounterDeck.GetFromTop(1).FirstOrDefault();
                if (topCard == null)
                    return false;

                stagingArea.AddExaminedEncounterCards(new List<IEncounterCard> { topCard });

                return true;
            }

            public override void Resolve(IGameState state, IChoice choice)
            {
                var topOfDeckChoice = choice as IChooseTopOrBottomOfDeck;
                if (topOfDeckChoice == null)
                    return;

                var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                if (stagingArea.ExaminedEncounterCards.Count() != 1)
                    return;

                var topCard = stagingArea.ExaminedEncounterCards.FirstOrDefault() as IEncounterCard;
                if (topCard == null)
                    return;

                stagingArea.RemoveExaminedEncounterCards(new List<IEncounterCard> { topCard });

                if (topOfDeckChoice.TopOfDeck)
                {
                    stagingArea.EncounterDeck.PutOnTop(new List<IEncounterCard> { topCard });
                }
                else
                {
                    stagingArea.EncounterDeck.PutOnBottom(new List<IEncounterCard> { topCard });

                }
            }
        }
    }
}
