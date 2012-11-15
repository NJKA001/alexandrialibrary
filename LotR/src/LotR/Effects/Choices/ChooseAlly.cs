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
        : ChoiceBase
    {
        public ChooseAlly(ISource source, IPlayer player)
            : base("Choose an ally.", source, player)
        {
        }

        private IAllyInPlay ally;

        public IAllyInPlay Ally
        {
            get { return ally; }
            set
            {
                if (ally == value)
                    return;

                ally = value;
                OnPropertyChanged("Ally");
            }
        }
    }
}
