using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Combat
{
    public interface IAfterDamageDealtToCharacter
    {
        void AfterDamageDealtToCharacter(IDamageDealt state);
    }
}
