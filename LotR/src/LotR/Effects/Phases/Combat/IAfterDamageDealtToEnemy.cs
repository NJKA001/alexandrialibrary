using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Combat
{
    public interface IAfterDamageDealtToEnemy
    {
        void AfterDamageDealtToEnemy(IDamageDealt state);
    }
}
