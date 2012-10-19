using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public class CheckForResourceIcon
        : StateBase, ICheckForResourceIcon
    {
        public CheckForResourceIcon(IGame game, ICostlyCard costlyCard, IResourcefulInPlay target, Sphere resourceIcon)
            : base(game)
        {
            this.CostlyCard = costlyCard;
            this.Target = target;
            this.ResourceIcon = resourceIcon;
        }

        private bool hasResourceIcon;

        public ICostlyCard CostlyCard
        {
            get;
            private set;
        }

        public IResourcefulInPlay Target
        {
            get;
            private set;
        }

        public Sphere ResourceIcon
        {
            get;
            private set;
        }

        public bool HasResourceIcon
        {
            get { return hasResourceIcon; }
            set
            {
                if (hasResourceIcon != value)
                {
                    hasResourceIcon = value;
                    OnPropertyChanged("HasResourceIcon");
                }
            }
        }
    }
}
