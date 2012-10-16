using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Modifiers;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class ChieftanUfthak
        : EnemyCardBase
    {
        public ChieftanUfthak()
            : base("Chieftan Ufthak", CardSet.Core, 90, EncounterSet.Dol_Guldur_Orcs, 1, 2, 35, 3, 3, 6, 4)
        {
            AddTrait(Trait.Dol_Guldur);
            AddTrait(Trait.Orc);

            AddEffect(new PassiveChieftanUfthakGainsPlusTwoAttackForEachResource(this));
            AddEffect(new ForcedChieftanUfthakGainsResourceAfterAttacking(this));
        }

        private class PassiveChieftanUfthakGainsPlusTwoAttackForEachResource
            : PassiveCardEffectBase, IDuringDetermineAttack
        {
            public PassiveChieftanUfthakGainsPlusTwoAttackForEachResource(ChieftanUfthak source)
                : base("Cheiftan Ufthak gets +2 Attack for each resource token on him.", source)
            {
            }

            public void DuringDetermineAttack(IDetermineAttack state)
            {
                var enemy = state.GetState<IEnemyInPlay>(Source.Id);
                if (enemy == null || enemy.Resources == 0)
                    return;

                state.AddEffect(this);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemy = state.GetState<IEnemyInPlay>(Source.Id);
                if (enemy == null || enemy.Resources == 0)
                    return;

                var bonus = enemy.Resources * 2;

                state.AddEffect(new AttackModifier(state.CurrentPhase, Source, enemy, TimeScope.None, bonus));
            }
        }

        private class ForcedChieftanUfthakGainsResourceAfterAttacking
            : ForcedCardEffectBase, IAfterEnemyAttacks
        {
            public ForcedChieftanUfthakGainsResourceAfterAttacking(ChieftanUfthak source)
                : base("After Chieftan Ufthak attacks, place 1 resource token on him.", source)
            {
            }

            public void AfterEnemyAttacks(IEnemyAttack state)
            {
                if (!state.IsAttacking)
                    return;

                state.AddEffect(this);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemy = state.GetState<IEnemyInPlay>(Source.Id);
                if (enemy == null)
                    return;

                enemy.Resources += 1;
            }
        }
    }
}
