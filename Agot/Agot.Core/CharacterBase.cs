using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public abstract class CharacterBase
        : PermanentBase, ICharacter
    {
        protected CharacterBase(string title, CardSet set, byte cost, byte strength)
            : base(title, CardType.Character, set, cost)
        {
            this.strength = strength;
        }

        private readonly IList<ChallengeIcon> icons = new List<ChallengeIcon>();
        private readonly IList<Crest> crests = new List<Crest>();
        private readonly byte strength;

        protected void AddIcon(ChallengeIcon icon)
        {
            icons.Add(icon);
        }

        protected void AddCrest(Crest crest)
        {
            crests.Add(crest);
        }

        public IEnumerable<ChallengeIcon> Icons
        {
            get { return icons; }
        }

        public IEnumerable<Crest> Crests
        {
            get { return crests; }
        }

        public byte Strength
        {
            get { return strength; }
        }
    }
}
