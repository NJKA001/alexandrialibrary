using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games;

namespace LotR.Effects.Choices
{
    public class ChoosePlayer
        : ChoiceBase, IChoosePlayer
    {
        public ChoosePlayer(ISource source)
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
