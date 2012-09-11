using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class ForcedCharacterAbilityBase
        : PassiveCharacterAbilityBase, IForced
    {
        protected ForcedCharacterAbilityBase(IPlayerCard source, string description)
            : base(source, description)
        {
        }
    }
}
