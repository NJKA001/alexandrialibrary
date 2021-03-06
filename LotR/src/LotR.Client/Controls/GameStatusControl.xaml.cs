﻿using System;
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

using LotR.Client.ViewModels;
using LotR.States;

namespace LotR.Client.Controls
{
    /// <summary>
    /// Interaction logic for GameStatusControl.xaml
    /// </summary>
    public partial class GameStatusControl : UserControl
    {
        public GameStatusControl()
        {
            InitializeComponent();
        }

        private IGame game;
        private StatusViewModel statusViewModel;

        public void Initialize(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            
            this.game = game;
            
            this.statusViewModel = new StatusViewModel(this.Dispatcher, game);
            gameStatusContainer.DataContext = statusViewModel;
        }

        public void SetCurrentStatus(string status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            if (statusViewModel == null)
                return;

            statusViewModel.SetCurrentStatus(status);
        }
    }
}
