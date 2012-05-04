using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public class TextBuilder
        : ITextBuilder
    {
        private readonly IList<Trait> traits = new List<Trait>();
        private readonly IList<Keyword> keywords = new List<Keyword>();
        private readonly IList<Crest> crests = new List<Crest>();
        private readonly IList<IAbility> abilities = new List<IAbility>();
        private sbyte goldBonus;
        private sbyte influenceBonus;
        private sbyte initiativeBonus;
        private string flavorText;

        public ITextBuilder Passive(string text)
        {
            return Passive(text, LimitType.None);
        }

        public ITextBuilder Passive(string text, LimitType limit)
        {
            this.abilities.Add(new Ability(AbilityType.Passive, PhaseType.None, limit, text));
            
            return this;
        }

        public ITextBuilder Response(string text)
        {
            return Response(text, LimitType.None);
        }

        public ITextBuilder Response(string text, LimitType limit)
        {
            this.abilities.Add(new Ability(AbilityType.Response, PhaseType.None, limit, text));

            return this;
        }

        public ITextBuilder AnyPhase(string text)
        {
            return Phase(PhaseType.None, text, LimitType.None);
        }

        public ITextBuilder AnyPhase(string text, LimitType limit)
        {
            return Phase(PhaseType.None, text, limit);
        }

        public ITextBuilder Phase(PhaseType phase, string text)
        {
            return Phase(phase, text, LimitType.None);
        }

        public ITextBuilder Phase(PhaseType phase, string text, LimitType limit)
        {
            this.abilities.Add(new Ability(AbilityType.Passive, phase, limit, text));

            return this;
        }

        public ITextBuilder Trait(Trait trait)
        {
            this.traits.Add(trait);

            return this;
        }

        public ITextBuilder Crest(Crest crest)
        {
            this.crests.Add(crest);

            return this;
        }

        public ITextBuilder Keyword(Keyword keyword)
        {
            this.keywords.Add(keyword);

            return this;
        }

        public ITextBuilder GoldBonus(sbyte goldBonus)
        {
            this.goldBonus = goldBonus;

            return this;
        }

        public ITextBuilder InfluenceBonus(sbyte influenceBonus)
        {
            this.influenceBonus = influenceBonus;

            return this;
        }

        public ITextBuilder InitiativeBonus(sbyte initiativeBonus)
        {
            this.initiativeBonus = initiativeBonus;

            return this;
        }

        public ITextBuilder FlavorText(string flavorText)
        {
            if (flavorText == null)
                throw new ArgumentNullException("falvorText");

            this.flavorText = flavorText;

            return this;
        }

        public IText ToText()
        {
            return new Text(keywords, crests, traits, abilities, goldBonus, influenceBonus, initiativeBonus, flavorText);
        }
    }
}
