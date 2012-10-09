using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class ALightInTheDark
        : EventCardBase
    {
        public ALightInTheDark()
            : base("A Light in the Dark", CardSet.Core, 52, Sphere.Spirit, 2)
        {
        }
    }
}
