using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseGaladhrimsGreetingEffect
        : ChoiceBase, IChooseGaladhrimsGreetingEffect
    {
        public ChooseGaladhrimsGreetingEffect(IGame game, ICard cardSource, IPlayer player)
            : base("Reduce 1 player's threat by 6, or reduce each player's threat by 2.", cardSource, player)
        {
        }

        private IPlayer reduceOnePlayersThreatBySix;
        private bool reduceEachPlayersThreatByTwo;


        public IPlayer ReduceOnePlayersThreatBySix
        {
            get { return reduceOnePlayersThreatBySix; }
            set
            {
                reduceOnePlayersThreatBySix = value;

                if (reduceOnePlayersThreatBySix != null)
                {
                    reduceEachPlayersThreatByTwo = false;
                }
            }
        }

        public bool ReduceEachPlayersThreatByTwo
        {
            get { return reduceEachPlayersThreatByTwo; }
            set
            {
                reduceEachPlayersThreatByTwo = value;

                if (reduceEachPlayersThreatByTwo)
                {
                    reduceOnePlayersThreatBySix = null;
                }
            }
        }

        public override bool IsValid(IGame game)
        {
            return reduceOnePlayersThreatBySix != null || reduceEachPlayersThreatByTwo;
        }
    }
}
