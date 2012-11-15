using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class KingSpider
        : EnemyCardBase
    {
        public KingSpider()
            : base("King Spider", CardSet.Core, 74, EncounterSet.Spiders_of_Mirkwood, 2, 2, 20, 3, 1, 3, 0)
        {
            AddTrait(Trait.Creature);
            AddTrait(Trait.Spider);

            AddEffect(new WhenRevealedEachPlayerExhaustsOneReadyCharacter(this));
            AddEffect(new ShadowDefendingPlayerExhaustsOneReadyCharacter(this));
        }

        private static void ExhaustReadyCharacters(IPlayer player, IPlayersChooseCharacters choice)
        {
            foreach (var character in choice.GetChosenCharacters(player.StateId))
            {
                var exhaustable = player.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == character.Card.Id).FirstOrDefault();
                if (exhaustable == null || exhaustable.IsExhausted)
                    continue;

                exhaustable.Exhaust();
            }
        }

        private class WhenRevealedEachPlayerExhaustsOneReadyCharacter
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachPlayerExhaustsOneReadyCharacter(KingSpider source)
                : base("Each player must choose and exhaust 1 character he controls.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var choice = new EachPlayerChoosesReadyCharacters(Source, game, 1);
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var characterChoice = options.Choice as IPlayersChooseCharacters;
                if (characterChoice == null)
                    return GetCancelledString();

                foreach (var player in characterChoice.Players)
                {
                    ExhaustReadyCharacters(player, characterChoice);
                }

                return ToString();
            }
        }

        private class ShadowDefendingPlayerExhaustsOneReadyCharacter
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerExhaustsOneReadyCharacter(KingSpider source)
                : base("Defending player must choose and exhaust 1 character he controls. (2 characters instead if this attack is undefended.)", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return base.GetOptions(game);

                byte numberOfCharacters = enemyAttack.IsUndefended ? (byte)2 : (byte)1;

                var choice = new EachPlayerChoosesReadyCharacters(Source, enemyAttack.DefendingPlayer, numberOfCharacters);
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var characterChoice = options.Choice as IPlayersChooseCharacters;
                if (characterChoice == null)
                    return GetCancelledString();

                var player = characterChoice.Players.FirstOrDefault();
                if (player == null)
                    return GetCancelledString();

                ExhaustReadyCharacters(player, characterChoice);

                return ToString();
            }
        }
    }
}
