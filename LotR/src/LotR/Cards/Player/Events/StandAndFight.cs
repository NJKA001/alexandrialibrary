using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class StandAndFight
        : EventCardBase
    {
        public StandAndFight()
            : base("Stand and Fight", CardSet.Core, 51, Sphere.Spirit, 0)
        {
            HasVariableCost = true;
        }
    }
}
