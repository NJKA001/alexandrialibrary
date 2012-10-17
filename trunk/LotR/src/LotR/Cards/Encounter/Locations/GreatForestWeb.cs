﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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
        }

        private class TravelEachPlayerMustExhaustOneHero
            : TravelEffectBase
        {
            public TravelEachPlayerMustExhaustOneHero(GreatForestWeb source)
                : base("Each player must exhaust 1 hero he controls to travel here.", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                return new EachPlayerChoosesReadyCharacters(Source, game, 1, true);
            }

            public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
            {
                var characterChoice = choice as IPlayersChooseCharacters;
                if (characterChoice == null)
                    return false;

                var players = game.GetStates<IPlayer>();
                if (players.Count() == 0)
                    return false;

                var charactersToExhaust = new List<IExhaustableInPlay>();

                foreach (var player in players)
                {
                    var exhaustable = characterChoice.GetChosenCharacters(player.StateId).FirstOrDefault() as IExhaustableInPlay;
                    if (exhaustable == null || exhaustable.IsExhausted)
                        return false;
                    else
                        charactersToExhaust.Add(exhaustable);
                }

                foreach (var exhaustable in charactersToExhaust)
                    exhaustable.Exhaust();

                return true;
            }
        }
    }
}
