using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;

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

            private void ReadyCharacterInPlay(IGame game, IEffectHandle handle, IPlayer player, IExhaustableInPlay character)
            {
                character.Ready();
                handle.Resolve(string.Format("{0} chose to ready '{1}'", player.Name, character.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var exhaustedCharacters = game.FirstPlayer.CardsInPlay.OfType<ICharacterInPlay>().OfType<IExhaustableInPlay>().Where(x => x.IsExhausted).ToList();

                if (exhaustedCharacters.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("The first player may choose and ready 1 character he controls", game, game.FirstPlayer)
                        .Question(string.Format("{0}, which character would you like to ready?"))
                            .Answers(exhaustedCharacters, (item) => item.Title, (source, handle, character) => ReadyCharacterInPlay(source, handle, game.FirstPlayer, character));
                
                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
