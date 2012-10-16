﻿using System;
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

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                if (state.CurrentPhase != Phase.Quest)
                    return;

                var committedCharacters = state.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (committedCharacters == null)
                    return;

                foreach (var willpowerful in committedCharacters.GetAllCharactersCommittedToQuest())
                {
                    state.AddEffect(new WillpowerModifier(state.CurrentPhase, Source, willpowerful, TimeScope.Phase, -1));
                }                
            }
        }

        private class ShadowRaiseDefendingPlayersThreat
            : ShadowEffectBase
        {
            public ShadowRaiseDefendingPlayersThreat(UngoliantsSpawn source)
                : base("Raise defending player's threat by 4. (Raise defending player's threat by 8 instead if this attack is undefended.)", source)
            {
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return;

                var threat = enemyAttack.IsUndefended ? (byte)8 : (byte)4;

                enemyAttack.DefendingPlayer.IncreaseThreat(threat);
            }
        }
    }
}