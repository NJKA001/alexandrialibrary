using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IAfterDamageDealt
    {
        void AfterDamageDealtSetup(IDealDamageStep step);
        void AfterDamageDealtResolve(IDealDamageStep step);
    }
}
