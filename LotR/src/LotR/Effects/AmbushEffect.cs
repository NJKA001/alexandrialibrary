using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public class AmbushEffect
        : CardEffectBase, IAmbushEffect
    {
        public AmbushEffect(ISource source)
            : base("Ambush", source)
        {
        }

        public override string ToString()
        {
            return "Ambush (after this enemy enters play, each player makes an engagement check against it.)";
        }
    }
}
