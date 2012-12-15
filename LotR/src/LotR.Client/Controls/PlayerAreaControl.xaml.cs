using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LotR.Cards.Player;
using LotR.Client.ViewModels;
using LotR.States;

namespace LotR.Client.Controls
{
    /// <summary>
    /// Interaction logic for PlayerAreaControl.xaml
    /// </summary>
    public partial class PlayerAreaControl : UserControl
    {
        public PlayerAreaControl()
        {
            InitializeComponent();
        }

        private IGame game;
        private readonly ObservableCollection<PlayerViewModel> playerViewModels = new ObservableCollection<PlayerViewModel>();

        public void Initialize(IGame game, IEnumerable<IPlayer> players)
        {
            this.game = game;
            
            foreach (var player in players)
            {
                playerViewModels.Add(new PlayerViewModel(this.Dispatcher, game, player));
            }

            playersContainer.ItemsSource = playerViewModels;
        }
    }
}
