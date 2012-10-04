using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IDealDamageToCharacterStep
        : IPhaseStep
    {
        IDamageableInPlay Target { get; }
        byte Damage { get; }
    }
}
