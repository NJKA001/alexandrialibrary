using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseHero
        : PlayerChoiceBase, IChooseHero
    {
        public ChooseHero(ISource source, IPlayer player)
            : base("Choose a hero", source, player)
        {
        }

        public IHeroInPlay Hero
        {
            get;
            set;
        }
    }
}
