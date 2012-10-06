using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ResponseCharacterAbilityBase
        : CharacterAbilityBase, IResponse
    {
        public ResponseCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
