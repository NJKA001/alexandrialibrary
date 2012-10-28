using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Effects
{
    public class SurgeEffect
        : CardEffectBase, ISurgeEffect
    {
        public SurgeEffect(IEncounterCard source)
            : base("Surge", source)
        {
        }

        public override string ToString()
        {
            return "Surge";
        }
    }
}
