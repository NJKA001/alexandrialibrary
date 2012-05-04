using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public class Ability
        : IAbility
    {
        public Ability(AbilityType type, PhaseType phase, LimitType limit, string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            this.type = type;
            this.phase = phase;
            this.limit = limit;
            this.text = text;
        }

        private readonly AbilityType type;
        private readonly PhaseType phase;
        private readonly LimitType limit;
        private readonly string text;

        public AbilityType Type
        {
            get { return type; }
        }

        public PhaseType Phase
        {
            get { return phase; }
        }

        public LimitType Limit
        {
            get { return limit; }
        }

        public string Text
        {
            get { return text; }
        }
    }
}
