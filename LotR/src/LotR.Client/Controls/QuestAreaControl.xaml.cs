using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using LotR.Client.ViewModels;
using LotR.States;

namespace LotR.Client.Controls
{
    /// <summary>
    /// Interaction logic for QuestAreaControl.xaml
    /// </summary>
    public partial class QuestAreaControl : UserControl
    {
        public QuestAreaControl()
        {
            InitializeComponent();
        }

        private IGame game;
        private QuestAreaViewModel viewModel;

        public void Initialize(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;
            this.viewModel = new QuestAreaViewModel(this.Dispatcher, game);

            Action action = () =>
            {
            this.questAreaContainer.DataContext = viewModel;
            this.activeLocationContainer.DataContext = viewModel.ActiveLocation;
            this.activeQuestContainer.DataContext = viewModel.ActiveQuest;
            };

            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }
    }
}
