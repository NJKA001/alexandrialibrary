using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IChallenge
    {
        ChallengeIcon Type { get; }
        
        ushort AttackingStrength { get; }
        ushort DefendingStrength { get; }
        ChallengeResult Result { get; }
        bool IsUnopposed { get; }
        bool IsDeadly { get; }

        IPlayer Winner { get; }

        IEnumerable<ICharacter> Attackers { get; }
        IEnumerable<ICharacter> Defenders { get; }
    }
}
