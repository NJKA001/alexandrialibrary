using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public abstract class PlayerChoiceBase
        : ChoiceBase, IPlayerChoice
    {
        protected PlayerChoiceBase(string description, ISource source, IPlayer player)
            : base(description, source)
        {
            this.Player = player;
        }

        public IPlayer Player
        {
            get;
            private set;
        }
    }
}
