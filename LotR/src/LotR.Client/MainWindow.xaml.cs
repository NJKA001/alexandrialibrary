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
using System.Windows.Threading;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Controllers;

using LotR.Client.ViewModels;

namespace LotR.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                System.Threading.Thread.CurrentThread.Name = "UI Thread";

                statusViewModel = new StatusViewModel(this.Dispatcher);

                currentStatusLabel.DataContext = statusViewModel;
                statusHistoryContainer.ItemsSource = statusViewModel.History;
                playersContainer.ItemsSource = playerViewModels;

                var player1 = new PlayerInfo("Dan", "TheThreeHunters.txt");
                var player2 = new PlayerInfo("Irma", "SpiritLeadership.txt");
                var playersInfo = new List<PlayerInfo> 
                //{ player1 }; 
                //, 
                { player2 };

                controller = GetController();
                game = new Game(controller);

                var players = GetPlayers(game, playersInfo);
                foreach (var player in players)
                {
                    playerViewModels.Add(new PlayerViewModel(this.Dispatcher, game, player));
                }

                choiceControl.Initialize(game);
                //System.Threading.Thread.Sleep(1000);

                Action setupGame = () => game.Setup(players, ScenarioCode.Passage_Through_Mirkwood);

                gameThread = new System.Threading.Thread(new System.Threading.ThreadStart(setupGame));
                gameThread.Name = "Game Thread";
                gameThread.Start();

                while (game.StagingArea == null)
                {
                    System.Threading.Thread.Sleep(150);
                }

                stagingAreaViewModel = new StagingAreaViewModel(this.Dispatcher, game);
                stagingAreaContainer.ItemsSource = stagingAreaViewModel.CardsInPlay;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, "An Error Occurred");
            }
        }

        private readonly IGame game;
        private readonly System.Threading.Thread gameThread;
        private readonly IGameController controller;
        private readonly ObservableCollection<PlayerViewModel> playerViewModels = new ObservableCollection<PlayerViewModel>();
        private readonly StagingAreaViewModel stagingAreaViewModel;
        private readonly StatusViewModel statusViewModel;

        private IGameController GetController()
        {
            var controller = new GameController();
            controller.RegisterEffectAddedCallback((effect) => EffectAddedCallback(effect));
            controller.RegisterEffectCancelledCallback((effect, handle) => EffectCancelledCallback(effect, handle));
            controller.RegisterEffectResolvedCallback((effect, handle) => EffectResolvedCallback(effect, handle));
            controller.RegisterOfferChoiceCallback((effect, choice) => OfferChoiceCallback(effect, choice));
            //controller.RegisterPaymentAcceptedCallback((effect, handle) => PaymentAcceptedCallback(effect, handle));
            //controller.RegisterPaymentRejectedCallback((effect, handle) => PaymentRejectedCallback(effect, handle));

            return controller;
        }

        private IEnumerable<IPlayer> GetPlayers(IGame game, IEnumerable<PlayerInfo> playersInfo)
        {
            var players = new List<IPlayer>();

            var playerDeckLoader = new PlayerDeckLoader();

            foreach (var info in playersInfo)
            {
                var playerDeck = playerDeckLoader.Load(info.DeckPath);
                if (playerDeck != null)
                {
                    players.Add(new Player(game, info.Name, playerDeck));
                }
            }

            return players;
        }

        private void Dispatch(Action action)
        {
            this.Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }

        private void BlockUntil(Func<bool> predicate)
        {
            while (!predicate())
            {
                System.Threading.Thread.Sleep(150);
            }
        }

        private void SetCurrentStatus(string status)
        {
            Action action = () => statusViewModel.SetCurrentStatus(status);
            Dispatch(action);
        }

        private void EffectAddedCallback(IEffect effect)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("\r\nError in effect added callback: {0}\r\n{1}", ex.Message, ex.StackTrace), "ERROR: EffectAddedCallback");
            }
        }

        private void EffectResolvedCallback(IEffect effect, IEffectHandle handle)
        {
            SetCurrentStatus(handle.Status);
        }

        private void EffectCancelledCallback(IEffect effect, IEffectHandle handle)
        {
            SetCurrentStatus("Cancelled: " + effect.ToString());
        }

        private void OfferChoiceCallback(IEffect effect, IChoice choice)
        {
            try
            {
                //var x = System.Threading.Thread.CurrentThread.Name;
                Dispatch(() => choiceControl.Load(choice));

                BlockUntil(() => choiceControl.IsValid || choiceControl.IsCancelled);
            }
            catch (Exception ex)
            {
                MessageBox.Show(choice.Text + Environment.NewLine + Environment.NewLine + ex.Message, "Error Offering Choice");
            }
            //statusViewModel.SetCurrentStatus("Choice: " + choice.Text);
            //MessageBox.Show(choice.Text, "OfferChoiceCallback");
        }

        private void PaymentAcceptedCallback(IEffect effect, IEffectHandle handle)
        {
            statusViewModel.SetCurrentStatus("Payment Accepted: " + effect.ToString());
            //MessageBox.Show(effect.ToString(), "PaymentAcceptedCallback");
        }

        private void PaymentRejectedCallback(IEffect effect, IEffectHandle handle)
        {
            statusViewModel.SetCurrentStatus("Payment Rejected: " + effect.ToString());
            //MessageBox.Show(effect.ToString(), "PaymentRejectedCallback");
        }

        public IEnumerable<PlayerViewModel> Players
        {
            get { return playerViewModels; }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                game.Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Pausing/Resuming Game");
            }
        }
    }
}
