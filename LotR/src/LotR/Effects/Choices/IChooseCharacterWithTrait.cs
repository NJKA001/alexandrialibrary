using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Choices
{
    public interface IChooseCharacterWithTrait
        : IChooseCharacter
    {
        Trait Trait { get; }
    }
}
