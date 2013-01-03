using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects
{
    public abstract class ResponseCharacterAbilityBase
        : CharacterAbilityBase, IResponseEffect
    {
        public ResponseCharacterAbilityBase(string text, IPlayerCard source)
            : base("Response", text, source)
        {
        }

        public override bool CanBeTriggered(IGame game)
        {
            return true;
        }
    }
}
