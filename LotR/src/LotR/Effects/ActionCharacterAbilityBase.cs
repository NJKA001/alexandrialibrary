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
        protected ActionCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }

        public override string ToString()
        {
            return string.Format("Action: {0}", Description);
        }
    }
}
