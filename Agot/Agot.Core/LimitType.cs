using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public enum LimitType
    {
        None = 0,
        Limited,
        LimitedResponse,
        OncePerRound,
        OncePerPhase,
        OncePerChallenge
    }
}
