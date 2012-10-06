using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Choices
{
    public class ChooseCharacter
        : ChoiceBase, IChooseCharacter
    {
        public ChooseCharacter(ICard source)
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
