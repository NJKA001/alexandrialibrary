using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ResponseEffectBase
        : CardEffectBase, IResponse
    {
        protected ResponseEffectBase(string description, ICard source)
            : base(description, source)
        {
        }
    }
}
