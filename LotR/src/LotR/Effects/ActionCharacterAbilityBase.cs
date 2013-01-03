using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects
{
    public abstract class ActionCharacterAbilityBase
        : CharacterAbilityBase, IActionEffect, IPlayerActionEffect
    {
        protected ActionCharacterAbilityBase(string text, IPlayerCard source)
            : base("Action", text, source)
        {
        }

        public override bool CanBeTriggered(IGame game)
        {
            return IsPlayerActionWindow(game);
        }
    }
}
