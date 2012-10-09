using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Phases.Combat
{
    public interface IDealDamageToCharacterStep
        : IPhaseStep
    {
        IDamageableInPlay Target { get; }
        byte Damage { get; }
    }
}
