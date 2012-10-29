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
            if (startingHand.Count() != 6)
                throw new ArgumentException("starting hand must consist of 6 cards");

            StartingHand = startingHand;
            KeepStartingHand = true;
        }

        public IEnumerable<IPlayerCard> StartingHand
        {
            get;
            private set;
        }

        public bool KeepStartingHand
        {
            get;
            set;
        }
    }
}
