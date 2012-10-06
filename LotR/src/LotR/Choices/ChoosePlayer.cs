using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Choices
{
    public class ChoosePlayer
        : ChoiceBase, IChoosePlayer
    {
        public ChoosePlayer(ICard source)
            : base("Choose a player.", source)
        {
        }

        public IPlayer Player
        {
            get;
            set;
        }
    }
}
