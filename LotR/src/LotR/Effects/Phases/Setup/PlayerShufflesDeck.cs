using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class PlayerShufflesDeck
        : FrameworkEffectBase, IDuringSetup
    {
        public PlayerShufflesDeck(IGame game, IPlayer player)
            : base("Shuffle Deck", GetDescription(player), game)
        {
            this.Player = player;
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} shuffles their player card deck", player.Name);
        }

        public IPlayer Player
        {
            get;
            private set;
        }
        
        public void DuringSetup(IGame game)
        {
            Player.Deck.Shuffle();
        }
    }
}
