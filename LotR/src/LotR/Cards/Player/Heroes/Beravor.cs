using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Heroes
{
    public class Beravor
        : HeroCardBase
    {
        public Beravor()
            : base("Beravor", CardSet.Core, 12, Sphere.Lore, 10, 2, 2, 2, 4)
        {
            AddTrait(Trait.Dunedain);
            AddTrait(Trait.Ranger);

            AddEffect(new ExhaustBeravorToDrawTwoCards(this));
        }

        private class ExhaustBeravorToDrawTwoCards
            : ActionCharacterAbilityBase
        {
            public ExhaustBeravorToDrawTwoCards(Beravor source)
                : base("Exhaust Beravor to choose a player. That player draws 2 cards.", source)
            {
            }

            public override IChoice GetChoice(IPhaseStep step)
            {
                return new ChoosePlayer(Source);
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var exhaustable = step.GetCardInPlay(Source.Id) as ICardInPlay<IExhaustableCard>;
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public override bool PaymentAccepted(IPhaseStep step, IPayment payment)
            {
                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                //exhaustPayment.Exhaustable.Exhaust();
                
                return true;
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var playerChoice = choice as IChoosePlayer;
                if (playerChoice == null)
                    return;

                var cards = playerChoice.Player.Deck.GetFromTop(2);
                playerChoice.Player.Hand.AddCards(cards);
            }
        }
    }
}
