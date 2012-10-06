using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Choices;
using LotR.Costs;
using LotR.Effects;
using LotR.Payments;

namespace LotR.Heroes
{
    public class Denethor
        : HeroCardBase
    {
        public Denethor()
            : base("Denethor", SetNames.Core, 10, Sphere.Lore, 8, 1, 1, 3, 3)
        {
            Trait(Traits.Gondor);
            Trait(Traits.Noble);
            Trait(Traits.Steward);

            Effect(new ExamineTopCardOfEncounterDeck(this));
        }

        public class ExamineTopCardOfEncounterDeck
            : ActionCharacterAbilityBase
        {
            public ExamineTopCardOfEncounterDeck(Denethor source)
                : base("Exhaust Denethor to look at the top card of the encounter deck. You may move that card to the bottom of the deck.", source)
            {
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var exhaustable = step.GetCardInPlay(Source.Id) as IExhaustableCard;
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public override bool PaymentAccepted(IPhaseStep step, IPayment payment)
            {
                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                var topCard = step.Phase.Round.Game.StagingArea.EncounterDeck.GetFromTop(1).FirstOrDefault();
                if (topCard == null)
                    return false;

                step.Phase.Round.Game.StagingArea.AddExaminedEncounterCards(new List<IEncounterCard> { topCard });
                return true;
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var topOfDeckChoice = choice as IChooseTopOrBottomOfDeck;
                if (topOfDeckChoice == null)
                    return;

                if (step.Phase.Round.Game.StagingArea.ExaminedEncounterCards.Count() != 1)
                    return;

                var topCard = step.Phase.Round.Game.StagingArea.ExaminedEncounterCards.FirstOrDefault() as IEncounterCard;
                if (topCard == null)
                    return;

                step.Phase.Round.Game.StagingArea.RemoveExaminedEncounterCards(new List<IEncounterCard> { topCard });

                if (topOfDeckChoice.TopOfDeck)
                {
                    step.Phase.Round.Game.StagingArea.EncounterDeck.PutOnTop(new List<IEncounterCard> { topCard });
                }
                else
                {
                    step.Phase.Round.Game.StagingArea.EncounterDeck.PutOnBottom(new List<IEncounterCard> { topCard });
                    
                }
            }
        }
    }
}
