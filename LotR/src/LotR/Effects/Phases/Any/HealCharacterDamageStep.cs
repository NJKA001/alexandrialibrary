using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class HealCharacterDamageStep
        : PhaseStepBase, IHealCharacterDamageStep
    {
        public HealCharacterDamageStep(IPhase phase, IPlayer player, ICardInPlay<ICharacterCard> target, byte damageHealed)
            : base(phase, player)
        {
            this.Target = target;
            this.DamageHealed = damageHealed;
        }

        public ICardInPlay<ICharacterCard> Target
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
