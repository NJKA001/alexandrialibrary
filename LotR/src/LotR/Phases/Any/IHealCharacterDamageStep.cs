using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface IHealCharacterDamageStep
        : IPhaseStep
    {
        ICharacterInPlay Target { get; }
        byte DamageHealed { get; }
    }
}
