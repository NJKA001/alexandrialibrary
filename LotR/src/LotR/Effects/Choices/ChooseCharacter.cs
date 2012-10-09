using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games;

namespace LotR.Effects.Choices
{
    public class ChooseCharacter
        : ChoiceBase, IChooseCharacter
    {
        public ChooseCharacter(ISource source)
            : base("Choose a character", source)
        {
        }

        public ICharacterInPlay Character
        {
            get;
            set;
        }
    }
}
