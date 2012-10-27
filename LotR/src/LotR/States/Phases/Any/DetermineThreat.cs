using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DetermineThreat
        : StateBase, IDetermineThreat
    {
        public DetermineThreat(IGame game, IThreateningInPlay threatening)
            : base(game)
        {
            this.Threatening = threatening;
            this.threat = threatening.Card.PrintedThreat;
        }

        private byte threat;
        private bool isThreatCounted = true;

        public IThreateningInPlay Threatening
        {
            get;
            private set;
        }

        public byte Threat
        {
            get { return threat; }
            set
            {
                if (threat == value)
                    return;

                threat = value;
                OnPropertyChanged("Threat");
            }
        }

        public bool IsThreatCounted
        {
            get { return isThreatCounted; }
            set
            {
                if (isThreatCounted == value)
                    return;

                isThreatCounted = value;
                OnPropertyChanged("IsThreatCounted");
            }
        }
    }
}
