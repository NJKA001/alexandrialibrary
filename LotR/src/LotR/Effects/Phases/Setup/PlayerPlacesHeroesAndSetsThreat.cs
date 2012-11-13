using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class PlayerPlacesHeroesAndSetsThreat
        : FrameworkEffectBase, IDuringSetup
    {
        public PlayerPlacesHeroesAndSetsThreat(IGame game, IPlayer player)
            : base("Place heroes and set starting threat", GetDescription(player), game)
        {
            this.Player = player;
        }

        private static string GetDescription(IPlayer player)
        {
            return string.Format("{0} places their heroes and sets their initial threat to {1}", player.Name, GetInitialThreat(player));
        }

        private static byte GetInitialThreat(IPlayer player)
        {
            return (byte)player.Deck.Heroes.Sum(x => x.ThreatCost);
        }

        public IPlayer Player
        {
            get;
            private set;
        }

        public void DuringSetup(IGame game)
        {
            foreach (var hero in Player.Deck.Heroes)
            {
                Player.AddCardInPlay(new HeroInPlay(game, hero));
            }

            Player.IncreaseThreat(GetInitialThreat(Player));
        }
    }
}
