using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public class Text
        : IText
    {
        public Text(IEnumerable<Keyword> keywords, IEnumerable<Crest> crests, IEnumerable<Trait> traits, IEnumerable<IAbility> abilities, sbyte goldBonus, sbyte influenceBonus, sbyte initiativeBonus, string flavorText)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");
            if (crests == null)
                throw new ArgumentNullException("crests");
            if (traits == null)
                throw new ArgumentNullException("traits");
            if (abilities == null)
                throw new ArgumentNullException("abilities");
            if (flavorText == null)
                throw new ArgumentNullException("flavorText");

            this.keywords = keywords;
            this.crests = crests;
            this.traits = traits;
            this.abilities = abilities;
            this.goldBonus = goldBonus;
            this.influenceBonus = influenceBonus;
            this.initiativeBonus = initiativeBonus;
            this.flavorText = flavorText;
        }

        private readonly IEnumerable<Keyword> keywords;
        private readonly IEnumerable<Crest> crests;
        private readonly IEnumerable<Trait> traits;
        private readonly IEnumerable<IAbility> abilities;
        private sbyte goldBonus;
        private sbyte influenceBonus;
        private sbyte initiativeBonus;
        private readonly string flavorText;

        public IEnumerable<Keyword> Keywords
        {
            get { return keywords; }
        }

        public IEnumerable<Crest> Crests
        {
            get { return crests; }
        }

        public IEnumerable<Trait> Traits
        {
            get { return traits; }
        }

        public IEnumerable<IAbility> Abilities
        {
            get { return abilities; }
        }

        public sbyte GoldBonus
        {
            get { return goldBonus; }
        }

        public sbyte InfluenceBonus
        {
            get { return influenceBonus; }
        }

        public sbyte InitiativeBonus
        {
            get { return initiativeBonus; }
        }

        public string FlavorText
        {
            get { return flavorText; }
        }
    }
}
