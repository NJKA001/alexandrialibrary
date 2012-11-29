using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class EncounterCardInPlayViewModel
        : EncounterCardViewModel
    {
        public EncounterCardInPlayViewModel(IEncounterInPlay encounterInPlay)
            : base(encounterInPlay.Card)
        {
            if (encounterInPlay == null)
                throw new ArgumentNullException("encounterInPlay");

            this.encounterInPlay = encounterInPlay;
        }

        private readonly IEncounterInPlay encounterInPlay;

        public byte Damage
        {
            get { return encounterInPlay.Damage; }
        }

        public byte Progress
        {
            get
            {
                return (encounterInPlay is ILocationInPlay) ?
                    ((ILocationInPlay)encounterInPlay).Progress
                    : (byte)0;
            }
        }
    }
}
