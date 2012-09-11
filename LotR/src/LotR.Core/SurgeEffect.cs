using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public class SurgeEffect
        : CardEffectBase, ISurgeEffect
    {
        public SurgeEffect(IEncounterCard source)
            : base(source, "Surge")
        {
        }
    }
}
