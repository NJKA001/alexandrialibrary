using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Payments;
using LotR.Games;

namespace LotR.Effects.Choices
{
    public class ChooseAlly
        : ChoiceBase
    {
        public ChooseAlly(ISource source)
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
