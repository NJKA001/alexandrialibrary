using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public class HealCharacterDamageStep
        : PhaseStepBase, IHealCharacterDamageStep
    {
        public HealCharacterDamageStep(IPhase phase, IPlayer player, ICharacterInPlay target, byte damageHealed)
            : base(phase, player)
        {
            this.Target = target;
            this.DamageHealed = damageHealed;
        }

        public ICharacterInPlay Target
        {
            get;
            private set;
        }

        public byte DamageHealed
        {
            get;
            private set;
        }
    }
}
