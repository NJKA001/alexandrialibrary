using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Travel;
using LotR.States;
using LotR.States.Phases.Travel;

namespace LotR.Cards.Encounter.Locations
{
    public class OldForestRoad
        : LocationCardBase
    {
        public OldForestRoad()
            : base("Old Forest Road", CardSet.Core, 99, EncounterSet.Passage_Through_Mirkwood, 2, 1, 3, 0)
        {
            AddTrait(Trait.Forest);

            AddEffect(new ResponseAfterTravelingHereFirstPlayMayReadyOne(this));
        }

        private class ResponseAfterTravelingHereFirstPlayMayReadyOne
            : ResponseEffectBase, IAfterTraveling
        {
            public ResponseAfterTravelingHereFirstPlayMayReadyOne(OldForestRoad source)
                : base("After you travel to Old Forest Road, the first player may choose and ready 1 character he controls.", source)
            {
            }

            public void AfterTraveling(ITravel state)
            {
                state.AddEffect(this);
            }

            public override IChoice GetChoice(IGame game)
            {
                var exhaustedCharacters = new Dictionary<Guid, IList<ICharacterCard>>() { { game.FirstPlayer.StateId, new List<ICharacterCard>() } };

                foreach (var character in game.FirstPlayer.GetStates<ICharacterInPlay>())
                {
                    var exhaustable = character as IExhaustableInPlay;
                    if (exhaustable == null || !exhaustable.IsExhausted)
                        continue;

                    exhaustedCharacters[game.FirstPlayer.StateId].Add(character.Card);
                }

                if (exhaustedCharacters[game.FirstPlayer.StateId].Count == 0)
                    return null;

                return new PlayersChooseCards<ICharacterCard>("The first player may choose and ready 1 character he controls", Source, new List<IPlayer> { game.FirstPlayer }, 1, exhaustedCharacters);
            }
        }
    }
}
