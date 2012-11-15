using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseToKeepStartingHand
        : ChoiceBase, IChooseToKeepStartingHand
    {
        public ChooseToKeepStartingHand(string description, ISource source, IPlayer player, IEnumerable<IPlayerCard> startingHand)
            : base(description, source, player)
        {
            if (startingHand == null)
                throw new ArgumentNullException("startingHand");
            if (startingHand.Count() == 0)
                throw new ArgumentException("starting hand does not contain any cards");

            this.startingHand = startingHand;
        }

        private readonly IEnumerable<IPlayerCard> startingHand;
        private bool keepStartingHand = true;

        public IEnumerable<IPlayerCard> StartingHand
        {
            get { return startingHand; }
        }

        public bool KeepStartingHand
        {
            get { return keepStartingHand; }
            set
            {
                if (keepStartingHand == value)
                    return;

                keepStartingHand = value;
                OnPropertyChanged("KeepStartingHand");
            }
        }
    }
}
