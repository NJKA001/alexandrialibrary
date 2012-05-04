using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ITextBuilder
    {
        ITextBuilder Passive(string text);
        ITextBuilder Passive(string text, LimitType limit);
        ITextBuilder Response(string text);
        ITextBuilder Response(string text, LimitType limit);
        ITextBuilder AnyPhase(string text);
        ITextBuilder AnyPhase(string text, LimitType limit);
        ITextBuilder Phase(PhaseType phase, string text);
        ITextBuilder Phase(PhaseType phase, string text, LimitType limit);

        ITextBuilder Trait(Trait trait);
        ITextBuilder Crest(Crest crest);
        ITextBuilder Keyword(Keyword keyword);
        ITextBuilder GoldBonus(sbyte goldBonus);
        ITextBuilder InfluenceBonus(sbyte influenceBonus);
        ITextBuilder InitiativeBonus(sbyte initiativeBonus);
        ITextBuilder FlavorText(string flavorText);

        IText ToText();
    }
}
