using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.States
{
    public abstract class ThreateningInPlay
        : CardInPlay<IThreateningCard>, IEncounterInPlay, IThreateningInPlay
    {
        protected ThreateningInPlay(IGame game, IThreateningCard card)
            : base(game, card)
        {
        }

        private byte threat;

        IEncounterCard ICardInPlay<IEncounterCard>.Card
        {
            get { return Card as IEncounterCard; }
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
    }
}
