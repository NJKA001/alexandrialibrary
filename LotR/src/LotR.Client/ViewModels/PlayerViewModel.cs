using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class PlayerViewModel
        : ViewModelBase
    {
        public PlayerViewModel(IGame game, IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            this.player = player;

            foreach (var hero in player.Deck.Heroes)
            {
                var heroInPlay = new HeroInPlay(game, hero);
                heroes.Add(new PlayerCardInPlayViewModel<IHeroCard>(heroInPlay));
            }
        }

        private readonly IPlayer player;
        private readonly ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>> heroes = new ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>>();

        public string PlayerName
        {
            get { return player.Name; }
        }

        public byte CurrentThreat
        {
            get { return player.CurrentThreat; }
        }

        public IEnumerable<PlayerCardInPlayViewModel<IHeroCard>> Heroes
        {
            get { return heroes; }
        }
    }
}
