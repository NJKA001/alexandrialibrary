using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseHero
        : ChoiceBase, IChooseHero
    {
        public ChooseHero(ISource source, IPlayer player, IEnumerable<IHeroInPlay> heroesToChooseFrom)
            : base("Choose a hero", source, player)
        {
            if (heroesToChooseFrom == null)
                throw new ArgumentNullException("heroesToChooseFrom");

            this.HeroesToChooseFrom = heroesToChooseFrom;
        }

        private IHeroInPlay chosenHero;

        public IEnumerable<IHeroInPlay> HeroesToChooseFrom
        {
            get;
            private set;
        }

        public IHeroInPlay ChosenHero
        {
            get { return chosenHero; }
            set
            {
                if (chosenHero == value)
                    return;

                chosenHero = value;
                OnPropertyChanged("ChosenHero");
            }
        }

        public override bool IsValid(IGame game)
        {
            return chosenHero != null && HeroesToChooseFrom.Contains(chosenHero);
        }
    }
}
