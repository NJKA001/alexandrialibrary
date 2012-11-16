using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Modifiers;
using LotR.Effects.Payments;
using LotR.Effects.Choices;
using LotR.States;
using LotR.States.Phases.Combat;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Encounter.Enemies
{
    public class UngoliantsSpawn
        : EnemyCardBase
    {
        public UngoliantsSpawn()
            : base("Ungoliant's Spawn", CardSet.Core, 76, EncounterSet.Spiders_of_Mirkwood, 1, 3, 32, 5, 2, 9, 0)
        {
            AddTrait(Trait.Creature);
            AddTrait(Trait.Spider);

            AddEffect(new WhenRevealedEachCommittedCharacterLosesOneWillpower(this));
            AddEffect(new ShadowRaiseDefendingPlayersThreat(this));
        }

        private class WhenRevealedEachCommittedCharacterLosesOneWillpower
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachCommittedCharacterLosesOneWillpower(UngoliantsSpawn source)
                : base("Each character currently committed to a quest gets -1 Willpower until the end of the phase.", source)
            {
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return GetCancelledString();

                foreach (var willpowerful in questPhase.GetAllCharactersCommittedToQuest())
                {
                    game.AddEffect(new WillpowerModifier(game.CurrentPhase.Code, source, willpowerful, TimeScope.Phase, -1));
                }

                return ToString();
            }
        }

        private class ShadowRaiseDefendingPlayersThreat
            : ShadowEffectBase
        {
            public ShadowRaiseDefendingPlayersThreat(UngoliantsSpawn source)
                : base("Raise defending player's threat by 4. (Raise defending player's threat by 8 instead if this attack is undefended.)", source)
            {
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return GetCancelledString();

                var threat = enemyAttack.IsUndefended ? (byte)8 : (byte)4;

                enemyAttack.DefendingPlayer.IncreaseThreat(threat);

                return ToString();
            }
        }
    }
}
