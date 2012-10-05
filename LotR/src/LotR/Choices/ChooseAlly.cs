using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Payments;

namespace LotR.Choices
{
    public class ChooseAlly
        : ChoiceBase
    {
        public ChooseAlly(ICard source)
            : base("Choose an ally.", source)
        {
        }

        public IAllyInPlay Ally
        {
            get;
            set;
        }
    }
}
