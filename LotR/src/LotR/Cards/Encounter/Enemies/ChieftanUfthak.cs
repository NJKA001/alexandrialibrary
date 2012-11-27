using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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
                var enemy = state.Game.GetCardInPlay<IEnemyInPlay>(source.Id);
                if (enemy == null || enemy.Resources == 0)
                    return;

                state.Game.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemy = game.GetCardInPlay<IEnemyInPlay>(source.Id);
                if (enemy == null || enemy.Resources == 0)
                    { handle.Cancel(GetCancelledString()); return; }

                var bonus = enemy.Resources * 2;

                game.AddEffect(new AttackModifier(game.CurrentPhase.Code, source, enemy, TimeScope.None, bonus));

                handle.Resolve(GetCompletedStatus());
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

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemy = game.GetCardInPlay<IEnemyInPlay>(source.Id);
                if (enemy == null)
                    { handle.Cancel(GetCancelledString()); return; }

                enemy.Resources += 1;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
