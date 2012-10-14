using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChoosePlayer
        : ChoiceBase, IChoosePlayer
    {
        public ChoosePlayer(ISource source, IPlayer player)
            : base("Choose a player.", source, player)
        {
        }

        public IPlayer ChosenPlayer
        {
            get;
            set;
        }
    }
}
