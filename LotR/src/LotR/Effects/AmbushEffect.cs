using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public class AmbushEffect
        : CardEffectBase, IAmbushEffect
    {
        public AmbushEffect(ICard source)
            : base("Ambush", source)
        {
        }
    }
}
