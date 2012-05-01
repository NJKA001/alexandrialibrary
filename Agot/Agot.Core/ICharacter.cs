using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICharacter
        : IPermanent
    {
        IEnumerable<ChallengeIcon> Icons { get; }
        IEnumerable<Crest> Crests { get; }

        byte Strength { get; }
    }
}
