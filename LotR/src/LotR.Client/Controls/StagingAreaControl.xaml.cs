using System;
using System.Collections.Generic;
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

using LotR.Client.ViewModels;
using LotR.States;

namespace LotR.Client.Controls
{
    /// <summary>
    /// Interaction logic for StagingAreaControl.xaml
    /// </summary>
    public partial class StagingAreaControl : UserControl
    {
        public StagingAreaControl()
        {
            InitializeComponent();
        }

        StagingAreaViewModel viewModel;

        public void Initialize(IGame game)
        {
            viewModel = new StagingAreaViewModel(this.Dispatcher, game);

            stagingAreaContainer.ItemsSource = viewModel.CardsInPlay;
            revealedCardContainer.DataContext = viewModel.RevealedEncounterCard;
        }
    }
}
