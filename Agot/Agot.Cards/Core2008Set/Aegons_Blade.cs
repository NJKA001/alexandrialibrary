using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Agot;

namespace Agot.Core2008Set
{
    public class Aegons_Blade
        : PermanentBase
    {
        public Aegons_Blade()
            : base("Aegon's Blade", CardType.Attachment, CardSet.Core_2008, 1)
        {
            text.Trait(Trait.Weapon);
            text.Passive(@"While attached character has a military icon, it gains +2 STR.
                While attached character has an intrigue icon, it gains stealth.
                While attached character has a power icon, it gains renown.");
        }
    }
}
