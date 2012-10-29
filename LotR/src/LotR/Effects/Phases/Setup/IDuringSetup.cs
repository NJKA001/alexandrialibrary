using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public interface IDuringSetup
    {
        void DuringSetup(IGame game);
    }
}
