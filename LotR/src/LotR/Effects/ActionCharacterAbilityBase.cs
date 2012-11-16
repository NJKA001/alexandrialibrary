using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public abstract class ActionCharacterAbilityBase
        : CharacterAbilityBase, IActionEffect
    {
        protected ActionCharacterAbilityBase(string text, IPlayerCard source)
            : base(EffectType.Action, text, source)
        {
        }
    }
}
