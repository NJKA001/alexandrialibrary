using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface IDuringEncounterCardRevealed
    {
        void DuringEncounterCardRevealed(IEncounterCardRevealedStep step);
    }
}
