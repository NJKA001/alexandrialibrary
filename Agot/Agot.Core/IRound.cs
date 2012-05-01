using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IRound
    {
        IEnumerable<IRoundEffect> Effects { get; }
        IEnumerable<IPhase> Phases { get; }
        IEnumerable<IPlayer> Players { get; }

        IPhase CurrentPhase { get; }

        void AddEffect(IRoundEffect effect);
        void RemoveEffect(IRoundEffect effect);

        void AddPhase(IPhase phase);
        void RemovePhase(IPhase phase);
    }
}
