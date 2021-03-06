﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class GameStatus
        : StateBase, IGameStatus
    {
        public GameStatus(IGame game)
            : base(game)
        {
        }

        private bool isPlayerDefeat;
        private bool isPlayerVictory;
        private bool isPaused = true;

        public bool IsGameRunning
        {
            get { return !IsPlayerDefeat && !IsPlayerVictory; }
        }

        public bool IsPaused
        {
            get { return isPaused; }
            set
            {
                if (isPaused == value)
                    return;

                isPaused = value;
                OnPropertyChanged("IsPaused");
            }
        }

        public bool IsPlayerDefeat
        {
            get { return isPlayerDefeat; }
            set
            {
                if (isPlayerDefeat == value)
                    return;

                isPlayerDefeat = value;
                OnPropertyChanged("IsGameRunning");
                OnPropertyChanged("IsPlayerDefeat");
            }
        }        

        public bool IsPlayerVictory
        {
            get { return isPlayerVictory; }
            set
            {
                if (isPlayerVictory == value)
                    return;

                isPlayerVictory = value;
                OnPropertyChanged("IsGameRunning");
                OnPropertyChanged("IsPlayerVictory");
            }
        }
    }
}
