using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class CharacterSet
        : ICharacterSet
    {
        public CharacterSet(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        private readonly string name;
        private readonly string description;

        #region ICharacterSet Members

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        static CharacterSet()
        {
            InitializeCharacterSets();

            foreach (var characterSet in characterSets)
                byName.Add(characterSet.Name, characterSet);
        }

        private static void InitializeCharacterSets()
        {
            characterSets.Add(UsAscii);
            characterSets.Add(Utf8);
            characterSets.Add(Unknown);
        }

        private static readonly IList<ICharacterSet> characterSets = new List<ICharacterSet>();
        private static readonly IDictionary<string, ICharacterSet> byName = new Dictionary<string, ICharacterSet>();

        #region Public Static Methods

        public static ICharacterSet Parse(string name)
        {
            return byName.ContainsKey(name) ? byName[name] : Unknown;
        }

        public static IEnumerable<ICharacterSet> GetCharacterSets()
        {
            return characterSets;
        }

        #endregion

        #region Character Sets

        public static readonly ICharacterSet UsAscii = new CharacterSet("US-ASCII", "ANSI X3.4-1968");
        public static readonly ICharacterSet Utf8 = new CharacterSet("UTF-8", "Unicode UTF-8: RFC 3629");
        public static readonly ICharacterSet Unknown = new CharacterSet("UNKNOWN", "Unknown Character Set");

        #endregion
    }
}
