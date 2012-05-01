using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPhase
    {
        PhaseType Type { get; }
        IEnumerable<IPhaseEffect> Effects { get; }
        IEnumerable<IChallenge> Challenges { get; }
        IPlayer ActivePlayer { get; }

        void AddEffect(IPhaseEffect effect);
        void RemoveEffect(IPhaseEffect effect);

        void AddChallenge(IChallenge challenge);

        void ChangeActivePlayer(IPlayer player);
    }
}
