using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Travel;
using LotR.States;
using LotR.States.Phases.Travel;

namespace LotR.Cards.Encounter.Locations
{
    public class ForestGate
        : LocationCardBase
    {
        public ForestGate()
            : base("Forest Gate", CardSet.Core, 100, EncounterSet.Passage_Through_Mirkwood, 2, 2, 4, 0)
        {
            AddTrait(Trait.Forest);

            AddEffect(new ResponseAfterTravelingHereFirstPlayerDrawsTwoCards(this));
        }

        private class ResponseAfterTravelingHereFirstPlayerDrawsTwoCards
            : ResponseEffectBase, IAfterTraveling
        {
            public ResponseAfterTravelingHereFirstPlayerDrawsTwoCards(ForestGate source)
                : base("After you travel to Forest Gate, the first player may draw 2 cards.", source)
            {
            }

            public void AfterTraveling(ITravel state)
            {
                state.AddEffect(this);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var topTwo = state.FirstPlayer.Deck.GetFromTop(2);
                state.FirstPlayer.Hand.AddCards(topTwo);
            }
        }
    }
}
