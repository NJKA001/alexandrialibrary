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
            : base(EffectType.Surge, "Reveal 1 additional card from the encounter deck", source)
        {
        }
    }
}
