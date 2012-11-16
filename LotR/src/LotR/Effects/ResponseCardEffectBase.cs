using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ResponseCardEffectBase
        : CardEffectBase, IResponseEffect
    {
        protected ResponseCardEffectBase(string description, ICard cardSource)
            : base(EffectType.Response, description, cardSource)
        {
        }
    }
}
