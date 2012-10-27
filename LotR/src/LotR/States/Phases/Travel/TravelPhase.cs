using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Travel
{
    public class TravelPhase
        : PhaseBase, ITravelPhase
    {
        public TravelPhase(IGame game)
            : base(game, PhaseCode.Travel, PhaseStep.Travel_Start)
        {
        }

        private ILocationInPlay location;
        private bool isTraveledTo;

        public ILocationInPlay Location
        {
            get { return location; }
            set
            {
                if (location == value)
                    return;

                location = value;
                OnPropertyChanged("Location");
            }
        }

        public bool IsTraveledTo
        {
            get { return isTraveledTo; }
            set
            {
                if (isTraveledTo == value)
                    return;

                isTraveledTo = value;
                OnPropertyChanged("IsTraveledTo");
            }
        }
    }
}
