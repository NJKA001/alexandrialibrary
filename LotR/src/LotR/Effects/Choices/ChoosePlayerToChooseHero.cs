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
            this.PlayersToChooseFrom = playersToChooseFrom;
        }

        public IEnumerable<IPlayer> PlayersToChooseFrom
        {
            get;
            private set;
        }

        public IPlayer ChosenPlayer
        {
            get;
            set;
        }

        public IHeroInPlay ChosenHero
        {
            get;
            set;
        }

        public override bool IsValid(IGameState state)
        {
            if (this.ChosenPlayer == null)
                return false;

            if (!PlayersToChooseFrom.Contains(ChosenPlayer))
                return false;

            if (ChosenHero == null)
                return false;

            if (state.GetController(ChosenHero.Card.Id) != ChosenPlayer)
                return false;

            return true;
        }
    }
}
