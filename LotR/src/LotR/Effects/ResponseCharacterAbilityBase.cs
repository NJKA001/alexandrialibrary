using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public abstract class ResponseCharacterAbilityBase
        : CharacterAbilityBase, IResponseEffect
    {
        public ResponseCharacterAbilityBase(string text, IPlayerCard source)
            : base(EffectType.Response, text, source)
        {
        }
    }
}
