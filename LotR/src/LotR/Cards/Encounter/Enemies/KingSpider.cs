using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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

        private class WhenRevealedEachPlayerExhaustsOneReadyCharacter
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachPlayerExhaustsOneReadyCharacter(KingSpider source)
                : base("Each player must choose and exhaust 1 character he controls.", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var factory = new PlayersExhaustCharactersChoiceFactory();
                
                var choice = factory.GetChoice<ICharacterInPlay>(game, game.FirstPlayer, game.Players, "when King Spider is revealed", false, 1);

                return new EffectHandle(this, choice);
            }
        }

        private class ShadowDefendingPlayerExhaustsOneReadyCharacter
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerExhaustsOneReadyCharacter(KingSpider source)
                : base("Defending player must choose and exhaust 1 character he controls. (2 characters instead if this attack is undefended.)", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return base.GetHandle(game);

                uint minimumChosen = enemyAttack.IsUndefended ? (uint)2 : (uint)1;

                var factory = new PlayersExhaustCharactersChoiceFactory();

                var choice = factory.GetChoice<ICharacterInPlay>(game, enemyAttack.DefendingPlayer, new List<IPlayer> { enemyAttack.DefendingPlayer }, "when King Spider shadow effect resolves", false, minimumChosen);

                return new EffectHandle(this, choice);
            }
        }
    }
}
