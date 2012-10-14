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
            var playerArea = player.GetStates<IPlayerArea>().FirstOrDefault();
            if (playerArea == null)
                return;

            foreach (var character in choice.GetChosenCharacters(player.StateId))
            {
                var exhaustable = playerArea.GetState<IExhaustableInPlay>(character.Card.Id);
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

            public override IChoice GetChoice(IGameState state)
            {
                return new EachPlayerChoosesReadyCharacters(Source, state, 1);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var characterChoice = choice as IPlayersChooseCharacters;
                if (characterChoice == null)
                    return;

                foreach (var player in characterChoice.Players)
                {
                    ExhaustReadyCharacters(player, characterChoice);
                }
            }
        }

        private class ShadowDefendingPlayerExhaustsOneReadyCharacter
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerExhaustsOneReadyCharacter(KingSpider source)
                : base("Defending player must choose and exhaust 1 character he controls. (2 characters instead if this attack is undefended.)", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return null;

                byte numberOfCharacters = enemyAttack.IsUndefended ? (byte)2 : (byte)1;

                return new EachPlayerChoosesReadyCharacters(Source, enemyAttack.DefendingPlayer, numberOfCharacters);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var characterChoice = choice as IPlayersChooseCharacters;
                if (characterChoice == null)
                    return;

                var player = characterChoice.Players.FirstOrDefault();
                if (player == null)
                    return;

                ExhaustReadyCharacters(player, characterChoice);
            }
        }
    }
}
