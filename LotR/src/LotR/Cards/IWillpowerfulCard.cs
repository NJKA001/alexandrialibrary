using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards
{
    public interface IWillpowerfulCard
        : ICard
    {
        byte PrintedWillpower { get; }

        void DetermineWillpower(IDetermineWillpower state);
    }
}
