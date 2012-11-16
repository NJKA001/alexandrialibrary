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
        protected ResponseCardEffectBase(string text, ICard cardSource)
            : base("Response", text, cardSource)
        {
        }
    }
}
