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
        protected ChoiceBase(string description, ISource source)
        {
            this.Description = description;
            this.Source = source;
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
    }
}
