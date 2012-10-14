using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChoiceBase
        : IChoice
    {
        protected ChoiceBase(string description, ISource source, IPlayer player)
        {
            this.Description = description;
            this.Source = source;
            this.Player = player;
        }

        public string Description
        {
            get;
            private set;
        }

        public ISource Source
        {
            get;
            private set;
        }

        public IPlayer Player
        {
            get;
            private set;
        }

        public virtual bool IsValid(IGameState state)
        {
            return true;
        }
    }
}
