using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class PassiveEffect
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveEffect(string description, ISource source)
            : base(description, source)
        {
        }
    }
}
