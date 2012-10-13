using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player.Allies;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseAlly
        : PlayerChoiceBase
    {
        public ChooseAlly(ISource source, IPlayer player)
            : base("Choose an ally.", source, player)
        {
        }

        public ICardInPlay<IAllyCard> Ally
        {
            get;
            set;
        }
    }
}
