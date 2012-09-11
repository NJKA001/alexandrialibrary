using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class ResponseCharacterAbilityBase
        : CharacterAbilityBase, IResponse
    {
        public ResponseCharacterAbilityBase(IPlayerCard source, string description)
            : base(source, description)
        {
        }
    }
}
