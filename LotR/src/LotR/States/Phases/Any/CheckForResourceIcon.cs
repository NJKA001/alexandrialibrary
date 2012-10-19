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
        public CheckForResourceIcon(IGame game, ICharacterInPlay target, Sphere resourceIcon)
            : base(game)
        {
            this.Target = target;
            this.ResourceIcon = resourceIcon;
        }

        private bool hasResourceIcon;

        public ICharacterInPlay Target
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
