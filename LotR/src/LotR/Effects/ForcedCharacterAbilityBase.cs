using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public abstract class ForcedCharacterAbilityBase
        : PassiveCharacterAbilityBase, IForcedEffect
    {
        protected ForcedCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
