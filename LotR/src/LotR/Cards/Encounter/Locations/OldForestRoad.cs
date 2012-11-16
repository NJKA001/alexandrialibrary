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
            : ResponseCardEffectBase, IAfterTraveling
        {
            public ResponseAfterTravelingHereFirstPlayMayReadyOne(OldForestRoad source)
                : base("After you travel to Old Forest Road, the first player may choose and ready 1 character he controls.", source)
            {
            }

            public void AfterTraveling(ITravelPhase state)
            {
                state.Game.AddEffect(this);
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var exhaustedCharacters = new Dictionary<Guid, IList<ICharacterCard>>() { { game.FirstPlayer.StateId, new List<ICharacterCard>() } };

                foreach (var character in game.FirstPlayer.CardsInPlay.OfType<ICharacterInPlay>())
                {
                    var exhaustable = character as IExhaustableInPlay;
                    if (exhaustable == null || !exhaustable.IsExhausted)
                        continue;

                    exhaustedCharacters[game.FirstPlayer.StateId].Add(character.Card);
                }

                if (exhaustedCharacters[game.FirstPlayer.StateId].Count == 0)
                    return new EffectOptions();

                var choice = new PlayersChooseCards<ICharacterCard>("The first player may choose and ready 1 character he controls", source, new List<IPlayer> { game.FirstPlayer }, 1, exhaustedCharacters);
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var chooseCard = options.Choice as IPlayersChooseCards<ICharacterCard>;
                if (chooseCard == null)
                    return GetCancelledString();

                var character = chooseCard.GetChosenCards(game.FirstPlayer.StateId).FirstOrDefault();
                if (character == null)
                    return GetCancelledString();

                var exhaustable = game.GetCardInPlay<IExhaustableInPlay>(character.Id);
                if (exhaustable == null)
                    return GetCancelledString();

                exhaustable.Ready();

                return ToString();
            }
        }
    }
}
