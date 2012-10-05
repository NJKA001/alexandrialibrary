using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IEffect
    {
        string Description { get; }

        IChoice GetChoice(IPhaseStep step);
        ICost GetCost(IPhaseStep step);
        ILimit GetLimit(IPhaseStep step);

        void Setup(IPhaseStep step, IPayment payment);
        void Resolve(IPhaseStep step, IChoice choice);
    }
}
