using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class DolGuldurBeastmaster
        : EnemyCardBase
    {
        public DolGuldurBeastmaster()
            : base("Dol Guldur Beastmaster", CardSet.Core, 91, EncounterSet.Dol_Guldur_Orcs, 2, 2, 35, 3, 1, 5, 0)
        {
            AddTrait(Trait.Dol_Guldur);
            AddTrait(Trait.Orc);

            AddEffect(new ForcedAfterAttackingDealOneAdditionalShadowCard(this));
        }

        private class ForcedAfterAttackingDealOneAdditionalShadowCard
            : ForcedCardEffectBase, IDuringEnemyAttacks
        {
            public ForcedAfterAttackingDealOneAdditionalShadowCard(DolGuldurBeastmaster source)
                : base("When Dol Guldor Beastmaster attacks, deal it 1 additional shadow card.", source)
            {
            }

            public void DuringEnemyAttacks(IEnemyAttack state)
            {
                state.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    { handle.Cancel(GetCancelledString()); return; }

                enemyAttack.NumberOfShadowCardsToDeal += 1;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
