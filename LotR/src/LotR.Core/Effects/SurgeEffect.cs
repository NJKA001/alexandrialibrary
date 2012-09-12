using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public class SurgeEffect
        : CardEffectBase, ISurgeEffect
    {
        public SurgeEffect(IEncounterCard source)
            : base("Surge", source)
        {
        }
    }
}
