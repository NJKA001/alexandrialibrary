using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
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
