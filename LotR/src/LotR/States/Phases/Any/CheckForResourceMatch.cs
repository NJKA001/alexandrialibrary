using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;

namespace LotR.States.Phases.Any
{
    public class CheckForResourceMatch
        : StateBase, ICheckForResourceMatch
    {
        public CheckForResourceMatch(IGame game, ICostlyCard costlyCard)
            : base(game)
        {
            this.costlyCard = costlyCard;
            this.cardEffect = null;
        }

        public CheckForResourceMatch(IGame game, ICardEffect cardEffect)
            : base(game)
        {
            this.costlyCard = null;
            this.cardEffect = cardEffect;
        }

        private readonly ICostlyCard costlyCard;
        private readonly ICardEffect cardEffect;
        private bool isResourceMatch;

        public ICostlyCard CostlyCard
        {
            get { return costlyCard; }
        }

        public ICardEffect CardEffect
        {
            get { return cardEffect; }
        }

        public bool IsResourceMatch
        {
            get { return isResourceMatch; }
            set
            {
                if (isResourceMatch == value)
                    return;

                isResourceMatch = value;
                OnPropertyChanged("IsResourceMatch");
            }
        }
    }
}
