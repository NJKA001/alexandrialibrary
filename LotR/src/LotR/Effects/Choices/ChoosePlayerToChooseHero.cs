using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChoosePlayerToChooseHero
        : ChoiceBase, IChoosePlayerToChooseHero
    {
        public ChoosePlayerToChooseHero(ISource source, IPlayer player, IEnumerable<IPlayer> playersToChooseFrom)
            : base("Choose a player to choose a hero that they control.", source, player)
        {
            if (playersToChooseFrom == null)
                throw new ArgumentNullException("playersToChooseFrom");

            this.playersToChooseFrom = playersToChooseFrom;
        }

        private readonly IEnumerable<IPlayer> playersToChooseFrom;
        private IPlayer chosenPlayer;
        private IHeroInPlay chosenHero;

        public IEnumerable<IPlayer> PlayersToChooseFrom
        {
            get { return playersToChooseFrom; }
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

        public IHeroInPlay ChosenHero
        {
            get { return chosenHero; }
            set
            {
                if (chosenHero == value)
                    return;

                chosenHero = value;
                OnPropertyChanged("ChosenHero");
            }
        }

        public override bool IsValid(IGame game)
        {
            if (this.ChosenPlayer == null)
                return false;

            if (!playersToChooseFrom.Contains(ChosenPlayer))
                return false;

            if (ChosenHero == null)
                return false;

            var controller = ChosenHero.GetController(game);

            if (controller == null || controller.StateId != ChosenPlayer.StateId)
                return false;

            return true;
        }
    }
}
