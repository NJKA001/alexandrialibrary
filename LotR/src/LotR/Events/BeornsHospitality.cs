using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class BeornsHospitality
        : EventCardBase
    {
        public BeornsHospitality()
            : base("Beorn's Hospitality", CardSet.Core, 68, Sphere.Lore, 5)
        {
        }
    }
}
