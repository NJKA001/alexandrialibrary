using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Travel;

namespace LotR.Effects.Phases.Travel
{
    public interface IAfterTraveling
    {
        void AfterTraveling(ITravel state);
    }
}
