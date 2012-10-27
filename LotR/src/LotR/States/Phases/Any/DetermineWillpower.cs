using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DetermineWillpower
        : StateBase, IDetermineWillpower
    {
        public DetermineWillpower(IGame game, IWillpowerfulInPlay quester)
            : base(game)
        {
            this.Quester = quester;
            this.willpower = quester.Card.PrintedWillpower;
        }

        private byte willpower;
        private bool isWillpowerCounted = true;

        public IWillpowerfulInPlay Quester
        {
            get;
            private set;
        }

        public byte Willpower
        {
            get { return willpower; }
            set
            {
                if (willpower == value)
                    return;

                willpower = value;
                OnPropertyChanged("Willpower");
            }
        }

        public bool IsWillpowerCounted
        {
            get { return isWillpowerCounted; }
            set
            {
                if (isWillpowerCounted == value)
                    return;

                isWillpowerCounted = value;
                OnPropertyChanged("IsWillpowerCounted");
            }
        }
    }
}
