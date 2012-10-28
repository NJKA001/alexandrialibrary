using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ResponseEffectBase
        : CardEffectBase, IResponseEffect
    {
        protected ResponseEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public override string ToString()
        {
            return string.Format("Response: {0}", Description);
        }
    }
}
