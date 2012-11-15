using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseFirstPlayer
        : ChoiceBase, IChooseFirstPlayer
    {
        public ChooseFirstPlayer(IGame game)
            : base("The players determine a first player based on a majority group decision. If this proves impossible, determine a first player at random.", game, game.Players)
        {
        }

        private IPlayer firstPlayer;

        public IPlayer FirstPlayer
        {
            get { return firstPlayer; }
            set
            {
                if (firstPlayer == value)
                    return;

                firstPlayer = value;
                OnPropertyChanged("FirstPlayer");
            }
        }

        public void ChooseRandomFirstPlayer()
        {
            var random = new Random();
            var choice = random.Next(0, Players.Count());
            FirstPlayer = Players.ToList()[choice];
        }

        public override bool IsValid(IGame game)
        {
            return (FirstPlayer != null && Players.Contains(FirstPlayer));
        }
    }
}
