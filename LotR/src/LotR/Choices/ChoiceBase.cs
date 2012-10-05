using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Choices
{
    public class ChoiceBase
        : IChoice
    {
        protected ChoiceBase(string description, ICard source)
        {
            this.Description = description;
            this.Source = source;
        }

        public string Description
        {
            get;
            private set;
        }

        public ICard Source
        {
            get;
            private set;
        }
    }
}
