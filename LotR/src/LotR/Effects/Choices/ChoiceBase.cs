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
            : this(description, source, new List<IPlayer> { player })
        {
        }

        protected ChoiceBase(string description, ISource source, IEnumerable<IPlayer> players)
        {
            this.Description = description;
            this.Source = source;
            this.Players = players;
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

        public IEnumerable<IPlayer> Players
        {
            get;
            private set;
        }

        public virtual bool IsValid(IGame game)
        {
            return true;
        }
    }
}
