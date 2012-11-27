using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Cards.Encounter.Locations
{
    public class GreatForestWeb
        : LocationCardBase
    {
        public GreatForestWeb()
            : base("Great Forest Web", CardSet.Core, 77, EncounterSet.Spiders_of_Mirkwood, 2, 2, 2, 0)
        {
            AddTrait(Trait.Forest);

            AddEffect(new TravelEachPlayerMustExhaustOneHero(this));
        }

        private class TravelEachPlayerMustExhaustOneHero
            : TravelEffectBase
        {
            public TravelEachPlayerMustExhaustOneHero(GreatForestWeb source)
                : base("Each player must exhaust 1 hero he controls to travel here.", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var factory = new PlayersExhaustCharactersChoiceFactory();

                var choice = factory.GetChoice<IHeroInPlay>(game, game.FirstPlayer, game.Players, "travel to Great Forest Web", true, 1);
                
                return new EffectHandle(this, choice);
            }
        }
    }
}
