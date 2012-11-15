using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChoosePlayer
        : ChoiceBase, IChoosePlayer
    {
        public ChoosePlayer(ISource source, IPlayer player, IEnumerable<IPlayer> playersToChooseFrom)
            : base("Choose a player", source, player)
        {
            if (playersToChooseFrom == null)
                throw new ArgumentNullException("playersToChooseFrom");

            this.PlayersToChooseFrom = playersToChooseFrom;
        }

        private IPlayer chosenPlayer;

        public IEnumerable<IPlayer> PlayersToChooseFrom
        {
            get;
            private set;
        }

        public IPlayer ChosenPlayer
        {
            get { return chosenPlayer; }
            set
            {
                if (chosenPlayer == value)
                    return;

                chosenPlayer = value;
                OnPropertyChanged("ChosenPlayer");
            }
        }

        public override bool IsValid(IGame game)
        {
            return chosenPlayer != null && PlayersToChooseFrom.Contains(chosenPlayer);
        }
    }
}
